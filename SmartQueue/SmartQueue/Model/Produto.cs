using Newtonsoft.Json;

namespace SmartQueue.Model
{
    public class Produto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("categoriaId")]
        public int CategoriaId { get; set; }

        [JsonProperty("imagem")]
        public byte[] Imagem { get; set; }
    }
}