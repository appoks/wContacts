using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace W2contacts.Models
{
    public class Cliente
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("nomeEmpresa")]
        public string NomeEmpresa { get; set; }

        [JsonProperty("ramoAtividade")]
        public string RamoAtividade { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }

        [JsonProperty("cidade")]
        public string Cidade { get; set; }

        [JsonProperty("concessionariaID")]
        public int ConcessionariaID { get; set; }

        [JsonIgnore]
        public Concessionaria Concessionaria { get; set; }

        [JsonProperty("nomeResponsavel")]
        public string NomeResponsavel { get; set; }

        [JsonProperty("email")]
        // explicitar que é do contato técnico?
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }
    }
}
