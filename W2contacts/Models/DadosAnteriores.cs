using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W2contacts.Models
{
    // TODO: Tornar privado, colocar dentro da migração;
    public class DadosAnteriores
    {
        // seria melhor usar notação p/ o respectivo campo JSON

        public string empresa { get; set; }

        public string ramoatividade { get; set; }

        public string cidade { get; set; }

        public string estado { get; set; }

        public string contato { get; set; }

        public string emailcontato { get; set; }

        public string telefonecontato { get; set; }

        public string concessionaria { get; set; }

        public string contatoconcessionaria { get; set; }

        [JsonProperty("emailcontatoconecssionaria")]
        public string emailcontatoconcessionaria { get; set; }

        public string telefonecontatoconcessionaria { get; set; }

    }
}
