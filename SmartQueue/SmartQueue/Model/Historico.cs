using System;
using Newtonsoft.Json;

namespace SmartQueue.Model
{
    public sealed class Historico
    {
        [JsonProperty("dataReserva")]
        public DateTime DataReserva { get; set; }

        [JsonProperty("quantidadePessoas")]
        public int QuantidadePessoas { get; set; }

        [JsonProperty("mesa")]
        public string Mesa { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("pedidos")]
        public string Pedidos { get; set; }
    }
}
