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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuReserva());
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblQtdAssentos.Text = stepperQtdAssentos.Value.ToString();
        }
    }
}