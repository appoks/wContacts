using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using W2contacts.Models;

namespace W2contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegacyController : ControllerBase
    {
        private static IList<DadosAnteriores> todosClientes = new List<DadosAnteriores>();
        private static readonly HttpClient http = new HttpClient();

        private readonly ContactsContext _context;

        public LegacyController(ContactsContext context)
        {
            _context = context;
        }

        // GET: api/Legacy
        [HttpGet]
        public async Task<IList<DadosAnteriores>> Get()
        {
            try
            {
                var response = await http.GetStringAsync("https://eur04.safelinks.protection.outlook.com/?url=https%3A%2F%2Fd1c2avle47.execute-api.sa-east-1.amazonaws.com%2Fapi%2Fcadastros&data=02%7C01%7C%7C1cf2fbd35a4a41132bb608d79eb62b39%7C84df9e7fe9f640afb435aaaaaaaaaaaa%7C1%7C0%7C637152377552613589&sdata=rsMmPwsaZ8EKp2RmZCuaCUawKcCvF6B0Tsbpgqi18DU%3D&reserved=0");

                var body = JObject.Parse(response);

                var clients = JArray.Parse(body["body"].ToString());
                int counter = 0;

                foreach (var client in clients)
                {
                    Console.WriteLine(client);
                    DadosAnteriores searchResult = client.ToObject<DadosAnteriores>();
                    todosClientes.Add(searchResult);

                    // TODO: Inicialização por construtor...
                    Cliente newCli = new Cliente();
                    newCli.ID = counter;
                    newCli.NomeEmpresa = searchResult.empresa;
                    newCli.RamoAtividade = searchResult.ramoatividade;
                    newCli.Estado = searchResult.estado;
                    newCli.Cidade = searchResult.cidade;
                    newCli.NomeResponsavel = searchResult.contato;
                    newCli.Telefone = searchResult.telefonecontato;
                    newCli.Email = searchResult.emailcontato;

                    Concessionaria newConc = new Concessionaria();
                    newConc.ID = counter;
                    newConc.Nome = searchResult.concessionaria;
                    newConc.ContatoTecnico = searchResult.contatoconcessionaria;
                    newConc.Telefone = searchResult.telefonecontatoconcessionaria;
                    newConc.Email = searchResult.emailcontatoconcessionaria;

                    Console.WriteLine(newCli.NomeEmpresa);

                    newCli.Concessionaria = newConc;

                    //_context.Clientes.Add(newCli);
                    //await _context.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return todosClientes;
        }
    }
}
