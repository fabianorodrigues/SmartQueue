using Newtonsoft.Json;
using System;

namespace SmartQueue.Model
{
    public class Pedido
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("contaId")]
        public int ContaId { get; set; }
        [JsonProperty("data")]
        public DateTime Data { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
