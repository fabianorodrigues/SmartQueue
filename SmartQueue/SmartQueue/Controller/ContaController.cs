using SmartQueue.Service;
using SmartQueue.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartQueue.DAL;
using System;

namespace SmartQueue.Controller
{
    public sealed class ContaController
    {
        private ContaService service;
        private StorageConta storage;

        public ContaController()
        {
            service = new ContaService();
            storage = new StorageConta();
        }

        public async Task<Dictionary<string, string>> ConsultarConta()
        {
            try
            {
                Historico historico = await service.ConsultarConta(new StorageReserva().Consultar().Id);

                var pedidos = historico.Pedidos.Split('|');

                Dictionary<string, string> retorno = new Dictionary<string, string>();

                foreach (var item in pedidos)
                {
                    var split = item.Split('=');
                    retorno.Add(split[0], split[1]);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> ValorTotalDaConta()
        {
            try
            {
                Historico historico = await service.ConsultarConta(new StorageReserva().Consultar().Id);

                return historico.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RealizarPedido(Dictionary<int, int> dicItensPedidos)
        {
            List<ItemPedido> listaItens = new List<ItemPedido>();

            try
            {
                foreach (var item in dicItensPedidos)
                {
                    listaItens.Add(new ItemPedido()
                    {
                        ProdutoId = item.Key,
                        Quantidade = item.Value
                    });
                }

                Pedido pedido = await service.RealizarPedido(listaItens, storage.Consultar().Id);

                if (pedido.Id != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RealizarPedido()
        {
            StorageItemPedido storageItem = new StorageItemPedido();
            try
            {
                Pedido pedido = await service.RealizarPedido(storageItem.Listar(), storage.Consultar().Id);

                if (pedido.Id != 0)
                {
                    storageItem.ExcluirTodos();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CancelarPedido(int idPedido)
        {
            return await service.CancelarPedido(idPedido);
        }

        //public async Task<string> ProcessarPedido(int idPedido)
        //{
        //    return await service.ProcessarPedido(idPedido);
        //}

        public async Task<string> FinalizarPedido(int idPedido)
        {
            return await service.FinalizarPedido(idPedido);
        }

        public async Task<bool> FecharConta()
        {
            try
            {
                Conta conta = await service.FecharConta(new StorageReserva().Consultar().Id);

                storage.Excluir();
                new StorageReserva().Excluir();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
