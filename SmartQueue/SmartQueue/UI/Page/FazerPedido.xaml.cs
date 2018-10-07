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
        private Dictionary<int, int> dicItensPedidos;
        private IEnumerable<ItemCardapio> cardapio;

        public FazerPedido ()
		{
			InitializeComponent ();

            controller = new ProdutoController();
            dicItensPedidos = new Dictionary<int, int>();

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
                cardapio = await controller.Cardapio();
                lvCardapio.ItemsSource = from item in cardapio
                                         orderby item.CategoriaNome
                                         group item by item.CategoriaNome into grupos
                                         select new Agrupar<string, ItemCardapio>(grupos.Key.ToString(), grupos);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private Label RetornaLabelQuantidade(Button button)
        {
            StackLayout layout = (StackLayout)button.Parent;
            return (Label)layout.Children[0];
        }

        private int RetornaNumeroProduto(StackLayout layout)
        {
            Label lblProdutoId = (Label)layout.Children[1];
            return int.Parse(lblProdutoId.Text);
        }

        private async void RegistrarPedidos()
        {
            try
            {
                new ReservaController().RegistrarPedidos(dicItensPedidos);

                dicItensPedidos = new Dictionary<int, int>();

                await DisplayAlert("Confirmação", "Pedidos registrados com sucesso.", "OK");

                var menuReserva = this.Parent as TabbedPage;
                menuReserva.CurrentPage = menuReserva.Children[0];
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Erro desconhecido, tente novemente.", "Ok");
            }
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

                int produtoId = RetornaNumeroProduto((StackLayout)lblQuantidade.Parent);
                dicItensPedidos.Remove(produtoId);

                if (quantidade > 0)
                    dicItensPedidos.Add(produtoId, quantidade);
            }
        }

        private void ButtonAdicionar_Clicked(object sender, EventArgs e)
        {
            Label lblQuantidade = RetornaLabelQuantidade((Button)sender);

            int quantidade = int.Parse(lblQuantidade.Text);
            quantidade++;
            lblQuantidade.Text = quantidade.ToString();

            int produtoId = RetornaNumeroProduto((StackLayout)lblQuantidade.Parent);
            dicItensPedidos.Remove(produtoId);
            dicItensPedidos.Add(produtoId, quantidade);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            lvCardapio.ItemsSource = from item in cardapio
                                     orderby item.CategoriaNome
                                     group item by item.CategoriaNome into grupos
                                     select new Agrupar<string, ItemCardapio>(grupos.Key.ToString(), grupos);
        }

        private void ButtonRegistrarPedidos_Clicked(object sender, EventArgs e)
        {
            RegistrarPedidos();
        }
    }
}