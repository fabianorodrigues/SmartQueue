using System;
using SQLite;
using Newtonsoft.Json;

namespace SmartQueue.Model
{
    [Table("Conta")]
    public sealed class Conta
    {
        [JsonProperty("id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("reservaId")]
        public int ReservaId { get; set; }

        [JsonProperty("dataAbertura")]
        public DateTime DataAbertura { get; set; }

        [JsonProperty("dataFechamento")]
        public DateTime DataFechamento { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }
    }
}
