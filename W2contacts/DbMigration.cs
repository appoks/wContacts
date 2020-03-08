using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using W2contacts.Models;

namespace W2contacts
{
    internal class DbMigration
    {
        private static IList<DadosAnteriores> todosClientes = new List<DadosAnteriores>();
        private static readonly HttpClient http = new HttpClient();

        internal static async Task<bool> Initialize(ContactsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clientes.Any())
            {
                return false;   // DB has been seeded
            }

            var response = await http.GetStringAsync("https://eur04.safelinks.protection.outlook.com/?url=https%3A%2F%2Fd1c2avle47.execute-api.sa-east-1.amazonaws.com%2Fapi%2Fcadastros&data=02%7C01%7C%7C1cf2fbd35a4a41132bb608d79eb62b39%7C84df9e7fe9f640afb435aaaaaaaaaaaa%7C1%7C0%7C637152377552613589&sdata=rsMmPwsaZ8EKp2RmZCuaCUawKcCvF6B0Tsbpgqi18DU%3D&reserved=0");

            //Console.WriteLine("Convertendo dados da API...\n");
            var body = JObject.Parse(response);

            var clients = JArray.Parse(body["body"].ToString());

            //Console.WriteLine("Populando dados no banco...\n");
            foreach (var client in clients)
            {
                //Console.WriteLine(client);
                DadosAnteriores searchResult = client.ToObject<DadosAnteriores>();
                todosClientes.Add(searchResult);

                // TODO: Inicialização por construtor...
                Cliente newCli = new Cliente();
                newCli.NomeEmpresa = searchResult.empresa;
                newCli.RamoAtividade = searchResult.ramoatividade;
                newCli.Estado = searchResult.estado;
                newCli.Cidade = searchResult.cidade;
                newCli.NomeResponsavel = searchResult.contato;
                newCli.Telefone = searchResult.telefonecontato;
                newCli.Email = searchResult.emailcontato;

                Concessionaria newConc = new Concessionaria();
  
                var concess = context.Concessionarias.Where(c => c.ContatoTecnico == searchResult.contatoconcessionaria).ToList();

                if (concess.Count >= 1)
                {
                    newCli.ConcessionariaID = concess.First().ID;
                }
                else 
                {
                    newConc.Nome = searchResult.concessionaria;
                    newConc.ContatoTecnico = searchResult.contatoconcessionaria;
                    newConc.Telefone = searchResult.telefonecontatoconcessionaria;
                    newConc.Email = searchResult.emailcontatoconcessionaria;
                    newCli.Concessionaria = newConc;
                }
                
                await context.Clientes.AddAsync(newCli);
                await context.SaveChangesAsync();
            }
            return true;
        }
    }
}