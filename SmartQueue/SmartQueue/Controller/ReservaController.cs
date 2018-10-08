using SmartQueue.Model;
using SmartQueue.Service;
using SmartQueue.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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

        public async Task<bool> SolicitarMesa(int qtdAssentos)
        {
            Reserva reserva = new Reserva();

            try
            {
                if(storage.Count() > 0)
                    storage.Excluir();

                reserva.UsuarioId = new StorageUsuario().Consultar().Id;
                reserva.QuantidadePessoas = qtdAssentos;

                reserva = await service.SolicitarMesa(reserva);

                storage.Incluir(reserva);

                if (storage.Count() <= 0)
                    new ApplicationException("Erro desconhecido, tente novamente.");

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

        public async Task<bool> CancelarMesa()
        {
            try
            {
                if (storage.Count() <= 0)
                    return true;

                await service.CancelarMesa(storage.Consultar());

                storage.Excluir();

                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AtivarReserva(int nmrMesa , string senhaDaMesa)
        {
            try
            {
                Reserva reserva = storage.Consultar();
                reserva.MesaId = nmrMesa;

                Conta conta = await service.AtivarReserva(reserva, senhaDaMesa);

                storage.Alterar(reserva);

                StorageConta storageConta = new StorageConta();

                if (storageConta.Count() > 0)
                    storageConta.Excluir();

                storageConta.Incluir(conta);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ConsultarTempo()
        {
            try
            {
                Reserva reserva = storage.Consultar();
                string result = await service.ConsultarTempo(reserva.QuantidadePessoas);

                return result;
                //result = result.Split(' ')[1];

                //TimeSpan tempo = TimeSpan.Parse(result);

                ////se não houver tempo de espera retorna string vazia.
                //if (tempo.TotalSeconds < 0)
                //    return string.Empty;
                //else
                //{
                //    return string.Format("Senha para checkin: {0}\r\nTempo estimado para liberação da mesa: {1}:{2}", reserva.SenhaCheckIn,
                //        tempo.Hours, tempo.Minutes);
                //}

            }
            catch (Exception ex )
            {
                throw ex;
            }
            
        }

        public void RegistrarPedidos(Dictionary<int, int> dicItensPedidos)
        {
            StorageItemPedido storageItem = new StorageItemPedido();

            try
            {
                foreach (var item in dicItensPedidos)
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
