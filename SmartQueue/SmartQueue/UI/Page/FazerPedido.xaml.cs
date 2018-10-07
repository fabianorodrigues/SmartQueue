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
                listaCardapio.ItemsSource = Listar();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IndicadorDeAtividade();
            }
            
        }

        public IEnumerable<Agrupar<string, ItemCardapio>> Listar()
        {       
            return from item in cardapio
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