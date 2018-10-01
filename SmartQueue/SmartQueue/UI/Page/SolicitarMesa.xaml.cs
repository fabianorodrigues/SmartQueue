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
        public SolicitarMesa()
        {
            InitializeComponent();
        }

        private async void Solicitar()
        {
            if (Int32.Parse(lblQtdAssentos.Text) == 0)
                await DisplayAlert("Atenção", "Informe a quantidade de assentos", "Ok");
            else
                await Navigation.PushAsync(new MenuReserva());

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