using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SmartQueue.Controller;
using SmartQueue.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpAlterarSenha : PopupPage
	{
        private UsuarioController controller;
        public PopUpAlterarSenha ()
		{
			InitializeComponent ();
            controller = new UsuarioController();
        }

        public async Task<bool> Validacao()
        {
            if(string.IsNullOrEmpty(txtSenhaAtual.Text) || string.IsNullOrEmpty(txtNovaSenha.Text) 
                || string.IsNullOrEmpty(txtConfirmarSenha.Text))
            {
                await DisplayAlert("Atenção", "Digite os campos corretamente.", "Ok");
                return false;
            }

            if (txtSenhaAtual.Text.Length < 6)
            {
                await DisplayAlert("Senha atual inválida", "A senha digitada não corresponde a senha cadastrada.", "Ok");
                return false;
            }
            else if (txtNovaSenha.Text.Length < 6)
            {
                await DisplayAlert("Nova senha inválida", "A nova senha deve ter ao menos 6 dígitos.", "Ok");
                return false;
            }
            else if (txtConfirmarSenha.Text != txtNovaSenha.Text)
            {
                await DisplayAlert("Senha confirmada inválida", "A senha confirmada não é igual a nova senha.", "Ok");
                return false;
            }
            else if (txtSenhaAtual.Text == txtNovaSenha.Text)
            {
                await DisplayAlert("Nova senha inválida", "A nova senha não pode ser igual a senha cadastrada.", "Ok");
                return false;
            }
            else
                return true;
        }

        public async void Salvar()
        {
            try
            {
                if (await controller.AlterarSenha(txtSenhaAtual.Text, txtNovaSenha.Text))
                {
                    await DisplayAlert(string.Empty, "Senha alterada com sucesso", "Ok");
                    await Navigation.PopAsync();
                }
                else
                    throw new ApplicationException("Não foi possível alterar a senha.\nPor favor, tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Ok");
            }
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            if (await Validacao())
                Salvar();

            ((Button)sender).IsEnabled = true;
        }

        private void txt_Focused(object sender, FocusEventArgs e)
        {
            Aplicacao.MostrarLabel(true, (CustomEntry)sender);
        }

        private void txt_Unfocused(object sender, FocusEventArgs e)
        {
            if (((CustomEntry)sender).Text == string.Empty || ((CustomEntry)sender).Text == null)
                Aplicacao.MostrarLabel(false, (CustomEntry)sender);
        }

        private async void Cancelar_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}