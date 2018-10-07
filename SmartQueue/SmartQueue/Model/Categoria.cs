using Newtonsoft.Json;

namespace SmartQueue.Model
{
    public sealed class Categoria
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("caracteristica")]
        public string Caracteristica { get; set; }
    }
}
