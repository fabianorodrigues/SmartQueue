using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
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
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}