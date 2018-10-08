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

        private void VerificaContaAberta()
        {
            if (layoutConta.IsVisible == false)
            {
                if (new StorageConta().Count() > 0 && new StorageItemPedido().Count() > 0)
                    RealizarPedidosAberturaConta();
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
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void VerificaPedidosFinalizados()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (contaFechada || !threadAtivada)
                    return false;
                else
                {
                    ExibePedidos();
                }

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
    }
}