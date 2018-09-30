using Plugin.LocalNotifications;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuReserva : ContentPage
    {
        private int _vezesTimer;
        public MenuReserva()
        {
            InitializeComponent();
            
        }

        private async void AbreTela(string nomeBotao)
        {
            switch (nomeBotao)
            {
                case "Cancelar Reserva": Teste();
                    break;
                case "Histórico":
                    await Navigation.PushAsync(new HistoricoReserva());
                    break;
                case "Ranking":
                    await Navigation.PushAsync(new Ranking());
                    break;
                case "Conta":
                    await Navigation.PushAsync(new Conta());
                    break;
                case "Fazer Pedido":
                    await Navigation.PushAsync(new FazerPedido());
                    break;
                //case "Conta": CrossLocalNotifications.Current.Show("Testando essa porra", "Text");
                //    break;
                default:
                    break;
            }
        }

        public void Teste()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (_vezesTimer == 10)
                {
                    _vezesTimer = 0;
                    btnteste.Text = "Cancelar Reserva";
                    CrossLocalNotifications.Current.Show("Testando essa porra", "Text");

                    return false;
                }

                btnteste.Text = $"Timer foi executado {++_vezesTimer} vezes";
                return true;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AbreTela((sender as Button).Text);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}