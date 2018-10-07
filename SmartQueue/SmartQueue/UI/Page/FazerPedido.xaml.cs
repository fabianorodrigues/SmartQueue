using SmartQueue.Controller;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FazerPedido : ContentPage
	{
        private ProdutoController controller;
        private List<ItemPedido> listaItensPedidos;

        public FazerPedido ()
		{
			InitializeComponent ();

            controller = new ProdutoController();
            listaItensPedidos = new List<ItemPedido>();

            CarregaCardapio();
        }

        private void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private async void CarregaCardapio()
        {
            IndicadorDeAtividade();

            try
            {
                lvCardapio.ItemsSource = MontaCardapio(await controller.Cardapio());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private IEnumerable<Agrupar<string, ItemCardapio>> MontaCardapio(IEnumerable<ItemCardapio> cardapio)
        {       
            return from item in cardapio
                   orderby item.CategoriaCaracteristica
                   group item by item.CategoriaCaracteristica into grupos
                   select new Agrupar<string, ItemCardapio>(grupos.Key.ToString(), grupos);
        }

        private Label RetornaLabelQuantidade(Button button)
        {
            StackLayout layout = (StackLayout)button.Parent;
            return (Label)layout.Children[0];
        }

        private async void listaCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SobreProduto((e.Item as ItemCardapio).ProdutoId));
        }

        private void ButtonRemover_Clicked(object sender, EventArgs e)
        {
            Label lblQuantidade = RetornaLabelQuantidade((Button)sender);

            if (lblQuantidade.Text != "0")
            {
                int quantidade = int.Parse(lblQuantidade.Text);
                quantidade--;
                lblQuantidade.Text = quantidade.ToString();
            }
        }

        private void ButtonAdicionar_Clicked(object sender, EventArgs e)
        {
            Label lblQuantidade = RetornaLabelQuantidade((Button)sender);

            int quantidade = int.Parse(lblQuantidade.Text);
            quantidade++;
            lblQuantidade.Text = quantidade.ToString();
        }
    }
}