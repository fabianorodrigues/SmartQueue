using SmartQueue.Controller;
using SmartQueue.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ranking : ContentPage
    {
        private ProdutoController controller;
        public Ranking()
        {
            InitializeComponent();
            controller = new ProdutoController();

            ConsultarRanking();
        }

        public async void ConsultarRanking()
        {
            try
            {
                listaRanking.ItemsSource = await controller.Ranking();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private async void listaRanking_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SobreProduto((e.Item as ItemRanking).IdProduto));            
        }
    }
}