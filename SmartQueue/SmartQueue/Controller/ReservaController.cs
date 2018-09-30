using SmartQueue.Model;
using SmartQueue.Service;
using SmartQueue.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartQueue.Controller
{
    public sealed class ReservaController
    {
        private ReservaService service;

        public ReservaController()
        {
            service = new ReservaService();   
        }

        public async Task<List<Historico>> ConsultarHistorico()
        {
            Usuario usuario = new Storage().Consultar();

            return await service.ConsultarHistorico(usuario.Id);
        }

        public async Task<Reserva> SolicitarMesa(Reserva reserva)
        {
            return await service.SolicitarMesa(reserva);
        }

        public async Task<string> CancelarMesa(Reserva reserva)
        {
            return await service.CancelarMesa(reserva);
        }

        public async Task<Conta> AtivarReserva(Reserva reserva, int numeroDaMesa)
        {
            return await service.AtivarReserva(reserva, numeroDaMesa);
        }

        public async Task<string> ConsultarTempo(int quantidadePessoas)
        {
            return await service.ConsultarTempo(quantidadePessoas);
        }

    }
}
