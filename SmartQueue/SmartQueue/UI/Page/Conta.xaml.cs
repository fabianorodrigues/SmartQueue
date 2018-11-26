using SmartQueue.Controller;
using SmartQueue.DAL;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Conta : ContentPage
	{
        private ContaController controller;
        private bool threadAtivada;
        private bool contaFechada;

		public Conta ()
		{
			InitializeComponent ();

            controller = new ContaController();
		}

        private async void VerificaContaAberta()
        {
            if (new StorageConta().Count() > 0 && layoutConta.IsVisible == false)
            {
                if (new StorageItemPedido().Count() > 0)
                    RealizarPedidosAberturaConta();

                else if(new StorageReserva().Consultar().Status == "Em Uso")
                {
                    Dictionary<string, string> dicConta = await controller.ConsultarConta();
                    if (dicConta.Keys.Count > 0)
                    {
                        lblPedidos.IsVisible = false;
                        layoutConta.IsVisible = true;
                        threadAtivada = true;
                        VerificaPedidosFinalizados();
                    }
                    else
                        lblPedidos.IsVisible = true;

                }
                    
                else
                    lblPedidos.IsVisible = true;
            }
            else if (!threadAtivada)
            {
                threadAtivada = true;
                VerificaPedidosFinalizados();
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
                    threadAtivada = true;
                    VerificaPedidosFinalizados();                                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void ExibePedidos()
        {
            try
            {
                Dictionary<string,string> dicConta = await controller.ConsultarConta();

                if (dicConta.Keys.Count <= 0)
                    lblPedidos.IsVisible = true;                 
                else
                {
                    lblPedidos.IsVisible = false;
                    lvConta.ItemsSource = dicConta;

                    decimal total = await controller.ValorTotalDaConta();
                    lblValorTotal.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void FecharConta()
        {           
            try
            {
                threadAtivada = false;

                if (await controller.FecharConta())
                {
                    contaFechada = true;
                    await Navigation.PopAsync(true);

                    await DisplayAlert("Confirmação", "Obrigado e volte sempre!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
                threadAtivada = true;
            }
        }

        private void VerificaPedidosFinalizados()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (contaFechada || !threadAtivada)
                    return false;
                else
                    ExibePedidos();

                return true;
            });
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();

            VerificaContaAberta();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            threadAtivada = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            FecharConta();
            ((Button)sender).IsEnabled = true;
        }
    }
}