using SmartQueue.Service;
using SmartQueue.Model;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace SmartQueue.Controller
{
    public sealed class ContaController
    {
        private ContaService service;

        public ContaController()
        {
            service = new ContaService();
        }

        public async Task<Historico> ConsultarConta(int idReserva)
        {
            return await service.ConsultarConta(idReserva);
        }

        public async Task<Pedido> RealizarPedido(Conta conta)
        {
            return await service.RealizarPedido(conta);
        }

        public async Task<string> CancelarPedido(int idPedido)
        {
            return await service.CancelarPedido(idPedido);
        }

        public async Task<string> ProcessarPedido(int idPedido)
        {
            return await service.ProcessarPedido(idPedido);
        }

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
