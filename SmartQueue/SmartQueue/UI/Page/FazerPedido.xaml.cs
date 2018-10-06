using SmartQueue.Controller;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FazerPedido : ContentPage
	{
        private ProdutoController controller;
        private IEnumerable<ItemCardapio> cardapio;

        public FazerPedido ()
		{
			InitializeComponent ();
            controller = new ProdutoController();
            CarregaCardapio();
        }

        private async void CarregaCardapio()
        {
            try
            {
                cardapio = await controller.Cardapio();
                listaCardapio.ItemsSource = Listar();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private void Procurar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaCardapio.ItemsSource = Listar(txtProcurar.Text);
        }

        public IEnumerable<Agrupar<string, ItemCardapio>> Listar(string filtro = "")
        {
            IEnumerable<ItemCardapio> cardapioFiltrado = cardapio;

            if (!string.IsNullOrEmpty(filtro))
                cardapioFiltrado = cardapio.Where(l => (l.ProdutoNome.ToLower().Contains(filtro.ToLower())) || l.CategoriaId.ToString().ToLower().Contains(filtro.ToLower()));
            return from item in cardapioFiltrado
                   orderby item.CategoriaCaracteristica
                   group item by item.CategoriaCaracteristica into grupos
                   select new Agrupar<string, ItemCardapio>(grupos.Key.ToString(), grupos);
        }

        private async void listaCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SobreProduto((e.Item as ItemCardapio).ProdutoId));
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Stepper stepper = (Stepper)sender;
            StackLayout layout = (StackLayout)stepper.Parent;

            Label labelQuantidade = (Label)layout.Children[0];

            labelQuantidade.Text = stepper.Value.ToString();
        }
    }
}