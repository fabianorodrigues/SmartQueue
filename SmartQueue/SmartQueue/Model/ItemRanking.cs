using Xamarin.Forms;

namespace SmartQueue.Model
{
    public sealed class ItemRanking
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public ImageSource Imagem { get; set;}
    }
}
