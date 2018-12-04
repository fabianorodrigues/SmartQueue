using Newtonsoft.Json;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartQueue.Service
{
    public sealed class ReservaService
    {
        private HttpClient client;

        public ReservaService()
        {
            client = new HttpClient();
        }

        public async Task<List<Historico>> ConsultarHistorico(int idUsuario)
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("reservas", "ConsultarHistorico", idUsuario.ToString()));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Historico>>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Reserva> SolicitarReserva(Reserva reserva)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reserva);

                var response = await client.PostAsync(Aplicacao.Url("reservas", "SolicitarReserva"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Reserva>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Conta> AtivarReserva(Reserva reserva, string senhaDaMesa)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reserva);

                var response = await client.PostAsync(Aplicacao.Url("reservas", "AtivarReserva", senhaDaMesa), 
                    Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Conta>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> ConsultarTempo(int idReserva)
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("reservas", "ConsultarTempo", idReserva.ToString()));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> CancelarReserva(Reserva reserva)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reserva);

                var response = await client.PostAsync(Aplicacao.Url("reservas", "CancelarReserva"), Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
