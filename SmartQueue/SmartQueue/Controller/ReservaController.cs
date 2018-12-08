using SmartQueue.DAL;
using SmartQueue.Model;
using SmartQueue.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartQueue.Controller
{
    public sealed class ReservaController
    {
        private ReservaService service;
        private StorageReserva storage;

        public ReservaController()
        {
            service = new ReservaService();
            storage = new StorageReserva();
        }

        public async Task<List<Historico>> ConsultarHistorico()
        {
            return await service.ConsultarHistorico(new StorageUsuario().Consultar().Id);
        }

        public async Task<bool> SolicitarReserva(int qtdAssentos)
        {
            Reserva reserva = new Reserva();

            try
            {
                if (storage.Count() > 0)
                {
                    storage.Excluir();
                }

                reserva.UsuarioId = new StorageUsuario().Consultar().Id;
                reserva.QuantidadePessoas = qtdAssentos;

                reserva = await service.SolicitarReserva(reserva);

                storage.Incluir(reserva);

                if (storage.Count() <= 0)
                {
                    new ApplicationException("Erro desconhecido, tente novamente.");
                }

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

        public async Task<bool> CancelarReserva()
        {
            try
            {
                if (storage.Count() <= 0)
                {
                    return false;
                }

                new StorageItemPedido().ExcluirTodos();

                await service.CancelarReserva(storage.Consultar());
                storage.Excluir();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AtivarReserva(int nmrMesa, string senhaDaMesa)
        {
            try
            {
                Reserva reserva = storage.Consultar();
                reserva.MesaId = nmrMesa;

                Conta conta = await service.AtivarReserva(reserva, senhaDaMesa);

                StorageConta storageConta = new StorageConta();

                if (storageConta.Count() > 0)
                {
                    storageConta.Excluir();
                }

                storageConta.Incluir(conta);

                reserva.Status = "Em Uso";
                storage.Alterar(reserva);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TimeSpan> ConsultarTempo()
        {
            try
            {
                Reserva reserva = storage.Consultar();
                string result = await service.ConsultarTempo(reserva.Id);


                if (int.TryParse(result, out int minutos))
                {
                    return new TimeSpan(0, minutos, 0);
                }
                else
                {
                    throw new ApplicationException("Erro ao consultar o tempo de espera, tente novamente mais tarde.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RegistrarPedidos(Dictionary<int, int> dicItensPedidos)
        {
            StorageItemPedido storageItem = new StorageItemPedido();

            try
            {
                foreach (KeyValuePair<int, int> item in dicItensPedidos)
                {
                    storageItem.Incluir(new ItemPedido()
                    {
                        ProdutoId = item.Key,
                        Quantidade = item.Value
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemPedido> ItensPedidosPendentes()
        {
            try
            {
                return new StorageItemPedido().Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
