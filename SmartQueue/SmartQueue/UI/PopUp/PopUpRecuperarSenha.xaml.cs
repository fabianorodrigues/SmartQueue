using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SmartQueue.Controller;
using SmartQueue.Utils;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpRecuperarSenha : PopupPage
    {
        private UsuarioController controller;

        public PopUpRecuperarSenha ()
		{
			InitializeComponent ();
            controller = new UsuarioController();
		}

        public async void Recuperar()
        {
            try
            {
                if (!Aplicacao.EmailValido(txtEmail.Text))
                    await DisplayAlert("E-mail inválido", "Digite um e-mail válido.", "Ok");
                else if (await controller.RecuperarSenhaEmail(txtEmail.Text))
                {
                    await DisplayAlert("Senha alterada com sucesso.", "Nova senha encaminhada para o e-mail cadastrado.", "Ok");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Ok");
            }
        }

        private void Recuperar_Clicked(object sender, System.EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            Recuperar();
            ((Button)sender).IsEnabled = true;
        }

        private void txt_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Aplicacao.MostrarLabel(true, ((Entry)sender));
        }

        private void txt_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (((Entry)sender).Text == string.Empty || ((Entry)sender).Text == null)
                Aplicacao.MostrarLabel(false, (Entry)sender);
        }

        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            await PopupNavigation.Instance.PopAsync(true);
            ((Button)sender).IsEnabled = true;
        }
    }
}