using SmartQueue.UI.Page;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Master
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuReserva : MasterDetailPage
	{
        //private bool reservaAtivada;
        //private bool reservaCancelada;

        public List<MasterPageItem> listaMenu;

        public MenuReserva ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            listaMenu = new List<MasterPageItem>();

            listaMenu.Add(new MasterPageItem() { Titulo = "Reserva", Icone = "icone_mesa.png", TargetType = typeof(Reserva) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Fazer Pedido", Icone = "icone_cardapio.png", TargetType = typeof(FazerPedido) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Conta", Icone = "icone_conta.png", TargetType = typeof(Conta) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Meu Histórico", Icone = "icone_historico.png", TargetType = typeof(HistoricoReserva) });    
            listaMenu.Add(new MasterPageItem() { Titulo = "Ranking", Icone = "icone_ranking.png", TargetType = typeof(Ranking) });


            listViewMenu.ItemsSource = listaMenu;

            Detail = new NavigationPage(new Reserva());

            //VerificaReserva();
        }

        private async void CancelarReserva()
        {
            var sair = await DisplayAlert("Cancelar Reserva", "Tem certeza que deseja cancelar a reserva?", "Sim", "Não");

            if (sair)
                await Navigation.PopAsync(true);
        }

        //public void VerificaReserva()
        //{
        //    Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        //    {
        //        if (reservaAtivada)
        //            return false;
        //        if (reservaCancelada)
        //        {
        //            Navigation.PopAsync(true);
        //            return false;
        //        }

        //        return true;
        //    });
        //}

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (page == typeof(Reserva)) return;

            if (page != typeof(NullReferenceException))
                await Detail.Navigation.PushAsync((Xamarin.Forms.Page)Activator.CreateInstance(page));
            else
                CancelarReserva();

            IsPresented = false;
            listViewMenu.SelectedItem = listaMenu[0];
        }

        protected override bool OnBackButtonPressed()
        {
            if (Detail.Navigation.NavigationStack.Count > 0)
                base.OnBackButtonPressed();

            return true;
        }
    }
}