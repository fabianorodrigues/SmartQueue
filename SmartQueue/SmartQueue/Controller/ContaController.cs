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

        public async Task<Historico> ConsultarConta(int idReserva)
        {
            return await service.ConsultarConta(idReserva);
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

        public async Task<Conta> FecharConta(int idReserva)
        {
            return await service.FecharConta(idReserva);
        }
    }
}
