using SmartQueue.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoricoReserva : ContentPage
	{
        private ReservaController controller;

		public HistoricoReserva ()
		{
			InitializeComponent ();

            controller = new ReservaController();

            ConsultarHistorico();
		}

        public void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        public async void ConsultarHistorico()
        {
            IndicadorDeAtividade();

            try
            {
                listaHistorico.ItemsSource = await controller.ConsultarHistorico();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private void listaHistorico_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listaHistorico.SelectedItem = null;
        }
    }
}