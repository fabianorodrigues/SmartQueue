using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SmartQueue.Controller;
using SmartQueue.UI.Page;
using SmartQueue.UI.PopUp;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {
        private UsuarioController usuarioController;

        public List<MasterPageItem> listaMenu;

        public Menu()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            usuarioController = new UsuarioController();

            listaMenu = new List<MasterPageItem>();

            listaMenu.Add(new MasterPageItem() { Titulo = "Solicitar Mesa", Icone = "icone_mesa.png", TargetType = typeof(SolicitarMesa) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Meu Histórico", Icone = "icone_historico.png", TargetType = typeof(HistoricoReserva) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Cardápio", Icone = "icone_cardapio.png", TargetType = typeof(Cardapio) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Ranking", Icone = "icone_ranking.png", TargetType = typeof(Ranking) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Alterar Senha", Icone = "icone_senha.png", TargetType = typeof(PopUpAlterarSenha) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Sobre o App", Icone = "icone_sobre.png", TargetType = typeof(PopUpSobre) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Sair", Icone = "icone_logout.png", TargetType = typeof(NullReferenceException) });


            listViewMenu.ItemsSource = listaMenu;

            Detail = new NavigationPage(new SolicitarMesa());
        }

        public async void Sair()
        {
            var sair = await DisplayAlert("Confirmação de Logout", "Tem certeza que deseja sair?", "Sim", "Não");

            if (sair)
                if (await usuarioController.Sair())
                    await Navigation.PopAsync(true);
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (page == typeof(SolicitarMesa)) return;


            if (page == typeof(PopUpSobre) || page == typeof(PopUpAlterarSenha))
                await PopupNavigation.Instance.PushAsync((PopupPage)Activator.CreateInstance(page));
            else if (page != typeof(NullReferenceException))
                await Detail.Navigation.PushAsync((Xamarin.Forms.Page)Activator.CreateInstance(page));
            else
                Sair();

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