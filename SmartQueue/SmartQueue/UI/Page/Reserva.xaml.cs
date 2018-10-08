using Plugin.LocalNotifications;
using SmartQueue.Controller;
using SmartQueue.DAL;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            minutos = 0;
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
            string statusReserva = new StorageReserva().Consultar().Status;

            if (statusReserva != "Em Fila")
            {
                layoutTempo.IsVisible = false;
                if(statusReserva == "Em Uso")
                {
                    var menuReserva = this.Parent as TabbedPage;
                    menuReserva.CurrentPage = menuReserva.Children[2];
                    menuReserva.Children.RemoveAt(0);
                }
            }
                

            else
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
                                //CrossLocalNotifications.Current.Show("Mesa Liberada.", string.Format("Ao chegar na mesa realize checkin com o número e senha da mesa."));
                                layoutAtivarReserva.IsVisible = true;
                                layoutTempo.IsVisible = false;
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

        private async Task<bool> ValidaAtivarReserva()
        {
            if(string.IsNullOrEmpty(txtNmrMesa.Text) || string.IsNullOrEmpty(txtSenhaMesa.Text))
            {
                await DisplayAlert("Atenção", "Digite a senha e o número da mesa corretamente.", "OK");
                return false;
            }
            else if (txtSenhaMesa.Text.Length < 6)
            {
                await DisplayAlert("Senha inválida", "A senha deve ter ao menos 6 dígitos.", "Ok");
                return false;
            }

            return true;
        }

        private async void AtivarReserva()
        {         
            try
            {
                if (await controller.AtivarReserva(int.Parse(txtNmrMesa.Text), txtSenhaMesa.Text))
                {
                    var menuReserva = this.Parent as TabbedPage;
                    menuReserva.CurrentPage = menuReserva.Children[2];
                    menuReserva.Children.RemoveAt(0);
                }            
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        private void CancelarReserva_Clicked(object sender, EventArgs e)
        {
            CancelarReserva();
        }

        private void txt_Focused(object sender, FocusEventArgs e)
        {
            Aplicacao.MostrarLabel(true, (Entry)sender);
        }

        private void txt_Unfocused(object sender, FocusEventArgs e)
        {
            if (((Entry)sender).Text == string.Empty || ((Entry)sender).Text == null)
                Aplicacao.MostrarLabel(false, (Entry)sender);
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

        private void ButtonAtivarReserva_Clicked(object sender, EventArgs e)
        {
            AtivarReserva();
        }
    }
}