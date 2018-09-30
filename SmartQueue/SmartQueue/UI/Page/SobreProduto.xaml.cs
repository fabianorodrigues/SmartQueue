using SmartQueue.Controller;
using SmartQueue.Model;
using SmartQueue.Utils;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SobreProduto : ContentPage
	{
        private ProdutoController controller;

		public SobreProduto(int idProduto)
		{
			InitializeComponent();
            controller = new ProdutoController();

            CarregaInformacoes(idProduto);
        }

        private async void CarregaInformacoes(int idProduto)
        {
            Produto produto = await controller.ConsultarProduto(idProduto);

            imagem.Source = Aplicacao.ConverteImagem(produto.Imagem);

            lblDescricaoProduto.Text = produto.Descricao;
            lblValorProduto.Text = string.Format("R${0}", produto.Valor);
            lblNomeProduto.Text = produto.Nome;
        }

        
    }
}