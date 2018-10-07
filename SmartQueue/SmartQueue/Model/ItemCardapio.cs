namespace SmartQueue.Model
{
    public sealed class ItemCardapio
    {
        public int ProdutoId { get; set; }

        public string ProdutoNome { get; set; }

        public decimal ProdutoValor { get; set; }

        public int CategoriaId { get; set; }

        public string CategoriaNome { get; set; }
    }
}
