using Plugin.LocalNotifications;
using SmartQueue.Controller;
using SmartQueue.DAL;
using SmartQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Reserva : ContentPage
	{
        private ReservaController controller;
        private int segundos;
        private int minutos;
        private int horas;
        private bool reservaCancelada;
        private Dictionary<string, int> dicListaItensPendentes;

        public Reserva ()
		{
			InitializeComponent();
            controller = new ReservaController();

            segundos = 10;
            minutos = 1;
            horas = 0;

            LiberarMesa();

        }

        private async void CarregaDados()
        {
            if(lvPedidosPendentes.ItemsSource == null)
            {
                List<Produto> produtos = await new ProdutoController().ListarProdutos();
                List<ItemPedido> itens = controller.ItensPedidosPendentes();

                dicListaItensPendentes = new Dictionary<string, int>();

                foreach (var item in itens)
                {
                    var produto = produtos.First(x => x.Id == item.ProdutoId);
                    dicListaItensPendentes.Add(produto.Nome, item.Quantidade);
                }


                lvPedidosPendentes.ItemsSource = dicListaItensPendentes;
            }
            
        }

        //public async void ConsultarTempo()
        //{
        //    lblInfoReserva.Text = await controller.ConsultarTempo();
        //}

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
                            CrossLocalNotifications.Current.Show("Mesa Liberada.", string.Format("Ao chegar na mesa realize checkin com o número e senha da mesa."));

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
            try
            {
                var sair = await DisplayAlert("Cancelar Reserva", "Tem certeza que deseja cancelar a reserva?", "Sim", "Não");

                if (sair)
                {
                    if(await controller.CancelarMesa())
                    {
                        reservaCancelada = true;
                        await Navigation.PopAsync(true);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
            
                
        }

        private void CancelarReserva_Clicked(object sender, EventArgs e)
        {
            CancelarReserva();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregaDados();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            lvPedidosPendentes.ItemsSource = null;
        }
    }
}