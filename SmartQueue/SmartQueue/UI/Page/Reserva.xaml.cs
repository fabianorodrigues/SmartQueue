using Plugin.LocalNotifications;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Reserva : ContentPage
	{
        private int segundos;
        private int minutos;
        private int horas;
        private bool reservaCancelada;
        //private bool reservaAtivada;

        public Reserva ()
		{
			InitializeComponent();

            segundos = 30;
            minutos = 1;
            horas = 0;

            LiberarMesa();
        }

        public void LiberarMesa()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (reservaCancelada)
                    return false;
                else if (segundos == 0)
                {
                    if (minutos == 0)
                    {
                        if (horas == 0)
                        {
                            CrossLocalNotifications.Current.Show("Teste", "Text");
                            //reservaAtivada = true;
                            return false;
                        }
                        else
                        {
                            horas -= 1;
                            minutos = 60;
                        }
                    }
                    else
                    {
                        minutos -= 1;
                        segundos = 60;
                    }
                        
                }
                else
                    segundos -= 1;

                lblTempoLiberarMesa.Text = string.Format("{2}:{1}:{0}", segundos.ToString("00"), minutos.ToString("00"), horas.ToString("00"));
                return true;
            });
        }

        private async void CancelarReserva()
        {
            var sair = await DisplayAlert("Cancelar Reserva", "Tem certeza que deseja cancelar a reserva?", "Sim", "Não");

            if (sair)
            {
                reservaCancelada = true;
                await Navigation.PopAsync(true);
            }
                
        }

        protected override void OnDisappearing()
        {
            reservaCancelada = true;
            
            base.OnDisappearing();
        }

        private void CancelarReserva_Clicked(object sender, EventArgs e)
        {
            CancelarReserva();
        }
    }
}