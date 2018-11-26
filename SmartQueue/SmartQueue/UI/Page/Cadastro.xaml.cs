using SmartQueue.Controller;
using SmartQueue.Utils;
using SmartQueue.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SmartQueue.UI.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastro : ContentPage
	{
        private UsuarioController controller;

		public Cadastro ()
		{
            InitializeComponent();
            dtNascimento.Date = DateTime.Now;
            controller = new UsuarioController();
		}

        private async Task<bool> CampoVazio(Entry entrada)
        {
            if (!string.IsNullOrEmpty(entrada.Text))
                return false;
            else
            {
                await DisplayAlert("Atenção", string.Format("O campo {0} é obrigatório.", entrada.Placeholder), "Ok");
                return true;
            }
        }

        public void IndicadorDeAtividade()
        {
            activityIndicator.IsRunning = !activityIndicator.IsRunning;
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
        }

        private async Task<bool> Validacao()
        {
            if (await CampoVazio(txtNome) || await CampoVazio(txtSobrenome) || await CampoVazio(txtCpf) || await CampoVazio(txtEmail) ||
                await CampoVazio(txtSenha))
                return false;
            else if (!Aplicacao.CpfValido(txtCpf.Text))
            {
                await DisplayAlert("CPF inválido", "Digite um CPF válido.", "Ok");
                return false;
            }
            else if (!Aplicacao.EmailValido(txtEmail.Text))
            {
                await DisplayAlert("E-mail inválido", "Digite um e-mail válido.", "Ok");
                return false;
            }     
            else if (txtSenha.Text.Length < 6)
            {
                await DisplayAlert("Senha inválida", "A senha deve ter ao menos 6 dígitos.", "Ok");
                return false;
            }
            else if(txtSenha.Text != txtConfirmarSenha.Text)
            {
                await DisplayAlert("Atenção", "A senha confirmada não é igual a senha requerida.", "Ok");
                return false;
            }
            else
                return true;
        }

        public async void Cadastrar()
        {
            IndicadorDeAtividade();

            try
            {
                Usuario usuario = new Usuario
                {
                    Id = 0,
                    Nome = txtNome.Text,
                    Sobrenome = txtSobrenome.Text,
                    DataNascimento = dtNascimento.Date,
                    Cpf = txtCpf.Text,
                    Email = txtEmail.Text,
                    CidadeNatal = txtCidade.Text,
                    Senha = txtSenha.Text

                };

                if (await controller.Cadastrar(usuario))
                {
                    await DisplayAlert("Confirmação", "Cadastro realizado com sucesso.", "OK");
                    await Navigation.PopAsync(true);
                }
                   
                else
                    throw new ApplicationException("Não foi possível realizar o cadastro.\nPor favor, tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "Ok");
            }

            IndicadorDeAtividade();
        }

        private async void Cadastrar_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            if (await Validacao())
                Cadastrar();

            ((Button)sender).IsEnabled = true;
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