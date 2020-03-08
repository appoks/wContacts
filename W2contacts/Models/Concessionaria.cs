using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace W2contacts.Models
{
    public class Concessionaria
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("contatoTecnico")]
        public string ContatoTecnico { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonIgnore]
        public ICollection<Cliente> Clientes { get; set; }
    }
}
