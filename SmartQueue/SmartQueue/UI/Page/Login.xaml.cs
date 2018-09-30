using SmartQueue.Controller;
using SmartQueue.Utils;
using SmartQueue.UI.Master;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using SmartQueue.UI.PopUp;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private UsuarioController controller;

        public Login()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            controller = new UsuarioController();   
        }

        public void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private async Task<bool> Validacao()
        {
            if (!Aplicacao.EmailValido(txtEmail.Text))
            {
                await DisplayAlert("E-mail inválido", "Digite um e-mail válido.", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                await DisplayAlert("Senha inválida", "A senha deve ter ao menos 6 dígitos.", "Ok");
                return false;
            }             
            else
                return true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (controller.UsuarioLogado())
                await Navigation.PushAsync(new Master.Menu());
        }

        public async void Entrar()
        {
            IndicadorDeAtividade();

            try
            {
                if(await controller.Logar(txtEmail.Text, txtSenha.Text))
                    await Navigation.PushAsync(new Master.Menu());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private async void Entrar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            
            if (await Validacao())
                Entrar();

            ((Button)sender).IsEnabled = true;
        }

        private async void Cadastrar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            await Navigation.PushAsync(new Cadastro());

            ((Button)sender).IsEnabled = true;
        }

        private async void RedefinirSenha_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            await PopupNavigation.Instance.PushAsync(new PopUpRecuperarSenha());

            ((Button)sender).IsEnabled = true;
        }

    }
}