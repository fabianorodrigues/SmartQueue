using SmartQueue.Service;
using SmartQueue.Model;
using SmartQueue.DAL;
using System.Threading.Tasks;
using System;

namespace SmartQueue.Controller
{
    public sealed class UsuarioController
    {
        private UsuarioService service;
        private StorageUsuario storage;

        public UsuarioController()
        {
            service = new UsuarioService();
            storage = new StorageUsuario();
        }

        public bool UsuarioLogado()
        {
            if (storage.Count() <= 0)
                return false;
            else
                return true;
        } 

        public async Task<bool> Logar(string email, string senha)
        {
            try
            {
                Usuario usuario = await service.Logar(email, senha);

                if (usuario.Id == 0)
                    new ApplicationException("Erro desconhecido, tente novamente.");

                storage.Incluir(usuario);
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

        public async Task<bool> Cadastrar(Usuario usuario)
        {
            usuario = await service.Cadastrar(usuario);

            if (usuario.Id == 0)
                new ApplicationException("Erro desconhecido, tente novamente.");

            return true;
        }

        public async Task<bool> AlterarSenha(string senhaAtualDigitada, string novaSenha)
        {
            Usuario usuario = storage.Consultar();
            string senhaAnterior = usuario.Senha;
            usuario.Senha = senhaAtualDigitada;

            usuario = await service.AlterarSenha(usuario, novaSenha);

            if(usuario.Senha == senhaAnterior)
                new ApplicationException("Erro desconhecido, tente novamente.");

            storage.Alterar(usuario);
            return true;

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
                StorageConta storageConta = new StorageConta();
                StorageReserva storageReserva = new StorageReserva();

                if (storageConta.Count() > 0)
                    await new ContaController().FecharConta();
                else if (storageReserva.Count() > 0)
                    await new ReservaController().CancelarReserva();

                new StorageItemPedido().ExcluirTodos();
                storageReserva.Excluir();
                storageConta.Excluir();
                storage.Excluir();

                 if (storage.Count() > 0)
                    new ApplicationException("Erro desconhecido, tente novamente.");

                 return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
