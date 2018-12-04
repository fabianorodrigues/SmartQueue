using SmartQueue.Controller;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolicitarReserva : ContentPage
    {
        private ReservaController controller;

        public SolicitarReserva()
        {
            InitializeComponent();
            controller = new ReservaController();
        }

        private async void Solicitar()
        {
            IndicadorDeAtividade();

            try
            {
                if (int.Parse(lblQtdAssentos.Text) == 0)
                {
                    await DisplayAlert("Atenção", "Informe a quantidade de assentos", "Ok");
                }
                else if (await controller.SolicitarReserva(int.Parse(lblQtdAssentos.Text)))
                {
                    await Navigation.PushAsync(new MenuReserva());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        public void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            btnSolicitar.IsEnabled = false;
            Solicitar();
            btnSolicitar.IsEnabled = true;
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblQtdAssentos.Text = stepperQtdAssentos.Value.ToString();
        }
    }
}