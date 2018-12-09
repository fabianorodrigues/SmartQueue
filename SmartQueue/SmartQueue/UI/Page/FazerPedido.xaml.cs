using SmartQueue.Controller;
using SmartQueue.DAL;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FazerPedido : ContentPage
	{
        private ProdutoController controller;
        private Dictionary<int, int> dicItensPedidos;
        private List<Label> listaLabelQuantidade;

        public FazerPedido ()
		{
			InitializeComponent ();

            controller = new ProdutoController();
            listaLabelQuantidade = new List<Label>();


        }

        private void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private async void CarregaCardapio()
        {
            IndicadorDeAtividade();

            try
            {
                if(lvCardapio.ItemsSource == null)
                {
                    dicItensPedidos = new Dictionary<int, int>();

                    IEnumerable<ItemCardapio> cardapio = await controller.Cardapio();
                    lvCardapio.ItemsSource = from item in cardapio
                                             orderby item.CategoriaNome
                                             group item by item.CategoriaNome into grupos
                                             select new Agrupar<string, ItemCardapio>(grupos.Key.ToString(), grupos);
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private Label RetornaLabelQuantidade(Button button)
        {
            StackLayout layout = (StackLayout)button.Parent;
            return (Label)layout.Children[0];
        }

        private int RetornaNumeroProduto(StackLayout layout)
        {
            Label lblProdutoId = (Label)layout.Children[1];
            return int.Parse(lblProdutoId.Text);
        }

        private void AjustaLabelQuantidadeZero()
        {
            foreach (var item in listaLabelQuantidade)
            {
                item.Text = "0";
            }

            listaLabelQuantidade.Clear();
        }

        private async void RegistrarPedidos()
        {
            try
            {
                if(dicItensPedidos != null)
                {
                    if(dicItensPedidos.Count() > 0)
                    {
                        if (new StorageReserva().Consultar().Status == "Em Fila")
                        {
                            new ReservaController().RegistrarPedidosNaFila(dicItensPedidos);
                            var menuReserva = this.Parent as TabbedPage;
                            if (menuReserva.Children.Count > 0)
                                menuReserva.CurrentPage = menuReserva.Children[0];
                        }
                        else if (await new ContaController().RealizarPedido(dicItensPedidos))
                        {
                            var menuReserva = this.Parent as TabbedPage;
                            if (menuReserva.Children.Count > 1)
                                menuReserva.CurrentPage = menuReserva.Children[1];
                        }

                        dicItensPedidos = new Dictionary<int, int>();

                        await DisplayAlert("Confirmação", "Pedido registrado com sucesso.", "OK");
                        AjustaLabelQuantidadeZero();
                    }
                    else
                        await DisplayAlert("Atenção", "Não há itens selecionados.", "OK");
                }
                
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Erro desconhecido, tente novamente.", "Ok");
            }
        }

        private async void listaCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SobreProduto((e.Item as ItemCardapio).ProdutoId));
        }

        private void ButtonRemover_Clicked(object sender, EventArgs e)
        {
            Label lblQuantidade = RetornaLabelQuantidade((Button)sender);

            if (lblQuantidade.Text != "0")
            {
                int quantidade = int.Parse(lblQuantidade.Text);
                quantidade--;
                lblQuantidade.Text = quantidade.ToString();

                int produtoId = RetornaNumeroProduto((StackLayout)lblQuantidade.Parent);
                dicItensPedidos.Remove(produtoId);

                if (quantidade > 0)
                    dicItensPedidos.Add(produtoId, quantidade);
                else
                    listaLabelQuantidade.Remove(lblQuantidade);
            }
        }

        private void ButtonAdicionar_Clicked(object sender, EventArgs e)
        {
            Label lblQuantidade = RetornaLabelQuantidade((Button)sender);

            int quantidade = int.Parse(lblQuantidade.Text);
            quantidade++;
            lblQuantidade.Text = quantidade.ToString();

            int produtoId = RetornaNumeroProduto((StackLayout)lblQuantidade.Parent);
            dicItensPedidos.Remove(produtoId);
            dicItensPedidos.Add(produtoId, quantidade);

            if(!listaLabelQuantidade.Contains(lblQuantidade))
                listaLabelQuantidade.Add(lblQuantidade);
        }

        protected override void OnAppearing()
        {
            CarregaCardapio();
            base.OnAppearing();            
        }

        protected override void OnDisappearing()
        {   
            lvCardapio.ItemsSource = null;
            base.OnDisappearing();         
        }

        private void ButtonRegistrarPedidos_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            RegistrarPedidos();
            ((Button)sender).IsEnabled = true;
        }
    }
}