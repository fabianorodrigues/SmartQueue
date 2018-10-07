using Newtonsoft.Json;

namespace SmartQueue.Model
{
    public sealed class Categoria
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("NOME")]
        public string Nome { get; set; }
    }
}
