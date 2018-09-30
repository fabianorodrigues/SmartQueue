using SmartQueue.Utils;
using SmartQueue.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmartQueue.Service
{
    public sealed class UsuarioService
    {
        private HttpClient client;

        public UsuarioService()
        {
            client = new HttpClient();
        }

        public async Task<Usuario> Logar(string email, string senha)
        {            
            try
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Email = email,
                    Senha = senha
                });

                var response = await client.PostAsync(Aplicacao.Url("usuarios","Logar"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> Cadastrar(Usuario usuario)
        {           
            try
            {
                var json = JsonConvert.SerializeObject(usuario);

                var response = await client.PostAsync(Aplicacao.Url("usuarios", "CadastrarCliente"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> AlterarSenha(Usuario usuario, string novaSenha)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new
                {
                    UsuarioAtual = usuario,
                    NovaSenha = novaSenha
                });

                var response = await client.PostAsync(Aplicacao.Url("usuarios", "AlterarSenha"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RecuperarSenhaCpf(string cpf)
        {
            try
            {
                var response = await client.PostAsync(Aplicacao.Url("usuarios", "RecuperarSenhaPorCpf", cpf), Aplicacao.Content(string.Empty));

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> RecuperarSenhaEmail(string email)
        {
            try
            {
                var response = await client.PostAsync(Aplicacao.Url("usuarios", "RecuperarSenhaPorEmail", email), Aplicacao.Content(string.Empty));

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Usuario> Sair(Usuario usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);

                var response = await client.PostAsync(Aplicacao.Url("usuarios", "Sair"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Usuario>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
