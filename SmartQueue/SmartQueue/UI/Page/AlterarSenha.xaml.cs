using SmartQueue.Controller;
using SmartQueue.Utils;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlterarSenha : ContentPage
	{
        private UsuarioController controller;

		public AlterarSenha ()
		{
			InitializeComponent ();
            controller = new UsuarioController();
		}

        public async Task<bool> Validacao()
        {
            if(txtSenhaAtual.Text.Length < 6)
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
                if (await controller.AlterarSenha(txtNovaSenha.Text))
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await Validacao())
                Salvar();
        }

        private void txt_Focused(object sender, FocusEventArgs e)
        {
            Aplicacao.MostrarLabel(true, (Entry)sender);
        }

        private void txt_Unfocused(object sender, FocusEventArgs e)
        {
            if (((Entry)sender).Text == string.Empty || ((Entry)sender).Text == null)
                Aplicacao.MostrarLabel(false, (Entry)sender);
        }
    }
}