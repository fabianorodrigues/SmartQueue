using Rg.Plugins.Popup.Services;
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
        public List<MasterPageItem> listaMenu;

        public Menu()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            listaMenu = new List<MasterPageItem>();

            listaMenu.Add(new MasterPageItem() { Titulo = "Solicitar Mesa", Icone = "icone_mesa.png", TargetType = typeof(SolicitarMesa) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Meu Histórico", Icone = "icone_historico.png", TargetType = typeof(HistoricoReserva) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Cardápio", Icone = "icone_cardapio.png", TargetType = typeof(Cardapio) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Ranking", Icone = "icone_ranking.png", TargetType = typeof(Ranking) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Alterar Senha", Icone = "icone_senha.png", TargetType = typeof(AlterarSenha) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Sobre o App", Icone = "icone_sobre.png", TargetType = typeof(PopUpSobre) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Sair", Icone = "icone_logout.png", TargetType = typeof(Conta) });


            listViewMenu.ItemsSource = listaMenu;

            Detail = new NavigationPage(new SolicitarMesa());
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if(item.TargetType == typeof(PopUpSobre))
            {
                await PopupNavigation.Instance.PushAsync(new PopUpSobre());
                IsPresented = false;
                return;
            }
                
            await Detail.Navigation.PushAsync((Xamarin.Forms.Page)Activator.CreateInstance(page));

            IsPresented = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}