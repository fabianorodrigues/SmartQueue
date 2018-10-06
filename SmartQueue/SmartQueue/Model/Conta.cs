using System;
using Newtonsoft.Json;

namespace SmartQueue.Model
{
    public sealed class Conta
    {
        [JsonProperty("id")]
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
