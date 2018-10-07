using SmartQueue.Controller;
using SmartQueue.UI.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolicitarMesa : ContentPage
    {
        private ReservaController controller;

        public SolicitarMesa()
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
                    await DisplayAlert("Atenção", "Informe a quantidade de assentos", "Ok");

                else if(await controller.SolicitarMesa(int.Parse(lblQtdAssentos.Text)))
                        await Navigation.PushAsync(new MenuReserva());
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
            Solicitar();
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblQtdAssentos.Text = stepperQtdAssentos.Value.ToString();
        }
    }
}