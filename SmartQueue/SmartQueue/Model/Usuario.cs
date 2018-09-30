using Newtonsoft.Json;
using SQLite;
using System;

namespace SmartQueue.Model
{
    [Table("Usuario")]
    public sealed class Usuario
    {
        
        [JsonProperty("id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome {get; set;}

        [JsonProperty("sobrenome")]
        public string Sobrenome { get; set; }

        [JsonProperty("dataNascimento")]
        public DateTime DataNascimento { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("cidadeNatal")]
        public string CidadeNatal { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }
    }
}
