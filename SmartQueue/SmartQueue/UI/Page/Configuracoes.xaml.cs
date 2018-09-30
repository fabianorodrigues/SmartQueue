using SmartQueue.Controller;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Configuracoes : ContentPage
	{
        private UsuarioController usuarioController;

		public Configuracoes ()
		{
			InitializeComponent();
            usuarioController = new UsuarioController();
		}

        public void BotaoClicado(string nomeBotao)
        {
            switch (nomeBotao)
            {
                case "Alterar Senha": AlterarSenha();
                    break;
                case "Sobre": Sobre();
                    break;
                case "Sair": Sair();
                    break;
                default:
                    break;
            }
        }

        public void AlterarSenha()
        {
            //await Navigation.PushAsync(new AlterarSenha());
        }

        public async void Sobre()
        {
            await Navigation.PushAsync(new Sobre());
        }

        public async void Sair()
        {
            if (await usuarioController.Sair())
                await Navigation.PopToRootAsync();
            else
                Sair();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BotaoClicado((sender as Button).Text);
        }
    }
}