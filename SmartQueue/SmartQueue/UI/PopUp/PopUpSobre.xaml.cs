using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpSobre : PopupPage
    {
		public PopUpSobre ()
		{
			InitializeComponent ();
		}

        private async void Voltar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            await PopupNavigation.Instance.PopAsync(true);
            ((Button)sender).IsEnabled = true;
        }
    }
}