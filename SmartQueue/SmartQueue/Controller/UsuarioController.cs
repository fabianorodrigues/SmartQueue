using SmartQueue.Service;
using SmartQueue.Model;
using SmartQueue.Utils;
using System.Threading.Tasks;
using System;

namespace SmartQueue.Controller
{
    public sealed class UsuarioController
    {
        private UsuarioService service;
        private Storage storage;

        public UsuarioController()
        {
            service = new UsuarioService();
            storage = new Storage();
        }

        public bool UsuarioLogado()
        {
            
            Usuario usuario;

            try
            {
                usuario = storage.Consultar();

                if (usuario == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        } 

        public async Task<bool> Logar(string email, string senha)
        {
            Usuario usuario = await service.Logar(email, senha);

            if (usuario.Id != 0)
            {
                storage.Incluir(usuario);
                return true;
            }
            else return false;
        }

        public async Task<bool> Cadastrar(Usuario usuario)
        {
            usuario = await service.Cadastrar(usuario);

            if (usuario.Id != 0) return true;
            else return false;
        }

        public async Task<bool> AlterarSenha(string novaSenha)
        {
            Usuario usuario = storage.Consultar();
            string senhaAnterior = usuario.Senha;

            usuario = await service.AlterarSenha(usuario, novaSenha);

            if (usuario.Senha != senhaAnterior)
            {
                storage.Alterar(usuario);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> RecuperarSenhaCpf(string cpf)
        {
            return await service.RecuperarSenhaCpf(cpf);
        }

        public async Task<bool> RecuperarSenhaEmail(string email)
        {
            return await service.RecuperarSenhaEmail(email);
        }

        public async Task<bool> Sair()
        {
            try
            {
               await service.Sair(storage.Consultar());
               storage.Excluir();

                if (storage.Consultar() == null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return await Sair();
            }
            
        }
    }
}
