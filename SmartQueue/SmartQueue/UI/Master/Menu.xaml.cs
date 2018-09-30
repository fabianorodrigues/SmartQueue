using SmartQueue.UI.Page;
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

            listaMenu.Add(new MasterPageItem() { Titulo = "Solicitar Mesa", Icone = "logo.png", TargetType = typeof(SolicitarMesa) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Meu Histórico", Icone = "logo.png", TargetType = typeof(HistoricoReserva) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Cardápio", Icone = "logo.png", TargetType = typeof(Cardapio) });
            listaMenu.Add(new MasterPageItem() { Titulo = "Ranking", Icone = "logo.png", TargetType = typeof(Ranking) });
            

            listViewMenu.ItemsSource = listaMenu;

            Detail = new NavigationPage(new SolicitarMesa());
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Xamarin.Forms.Page)Activator.CreateInstance(page));
            IsPresented = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}