using SQLite;
using Newtonsoft.Json;

namespace SmartQueue.Model
{
    [Table("ItemPedido")]
    public class ItemPedido
    {
        [JsonProperty("produtoId"), PrimaryKey]
        public int ProdutoId { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
