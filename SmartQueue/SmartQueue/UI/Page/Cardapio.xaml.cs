using SmartQueue.Controller;
using SmartQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cardapio : ContentPage
	{
        private ProdutoController controller;
        private IEnumerable<ItemCardapio> cardapio;

        public Cardapio()
        {
            InitializeComponent();
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

        private void listaCardapio_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listaCardapio.SelectedItem = null;
        }

        private async void listaCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SobreProduto((e.Item as ItemCardapio).ProdutoId));
        }
    }
}