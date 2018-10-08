using SmartQueue.Controller;
using SmartQueue.DAL;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Conta : ContentPage
	{
        private ContaController controller;

		public Conta ()
		{
			InitializeComponent ();

            controller = new ContaController();
		}

        private void VerificaContaAberta()
        {
            if(layoutConta.IsVisible == false)
            {
                if (new StorageConta().Count() > 0 && new StorageItemPedido().Count() > 0)
                    RealizarPedidosAberturaConta();
            }
            
        }

        private async void RealizarPedidosAberturaConta()
        {
            try
            {
                if(await controller.RealizarPedido())
                {
                    await DisplayAlert("Confirmação", "Pedido(s) pendente(s) realizado(s) com sucesso.", "OK");
                    layoutConta.IsVisible = true;
                    VerificaPedidosFinalizados();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public void VerificaPedidosFinalizados()
        {
            //string statusReserva = new StorageReserva().Consultar().Status;

            //if (statusReserva != "Em Fila")
            //{
            //    layoutTempo.IsVisible = false;
            //    if (statusReserva == "Em Uso")
            //    {
            //        var menuReserva = this.Parent as TabbedPage;
            //        menuReserva.CurrentPage = menuReserva.Children[2];
            //        menuReserva.Children.RemoveAt(0);
            //    }
            //}


            //else
            //{
            //    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //    {
            //        if (reservaCancelada)
            //            return false;
            //        else if (segundos == 0)
            //        {
            //            if (minutos == 0)
            //            {
            //                if (horas == 0)
            //                {
            //                    //CrossLocalNotifications.Current.Show("Mesa Liberada.", string.Format("Ao chegar na mesa realize checkin com o número e senha da mesa."));
            //                    layoutAtivarReserva.IsVisible = true;
            //                    layoutTempo.IsVisible = false;
            //                    return false;
            //                }
            //                else
            //                {
            //                    horas -= 1;
            //                    minutos = 60;
            //                }
            //            }
            //            else
            //            {
            //                minutos -= 1;
            //                segundos = 60;
            //            }

            //        }
            //        else
            //            segundos -= 1;

            //        lblTempoLiberarMesa.Text = string.Format("{2}:{1}:{0}", segundos.ToString("00"), minutos.ToString("00"), horas.ToString("00"));
            //        return true;
            //    });
            //}
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();

            VerificaContaAberta();
        }
    }
}