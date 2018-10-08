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
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }    

        protected override void OnAppearing()
        {            
            base.OnAppearing();

            VerificaContaAberta();
        }
    }
}