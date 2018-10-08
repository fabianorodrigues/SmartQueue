using Newtonsoft.Json;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartQueue.Service
{
    public sealed class ContaService
    {
        private HttpClient client;

        public ContaService()
        {
            client = new HttpClient();
        }

        public async Task<Historico> ConsultarConta(int idReserva)
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("contas", "ConsultarConta", idReserva.ToString()));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Historico>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pedido> RealizarPedido(IEnumerable<ItemPedido> itensPedidos, int idConta)
        {
            try
            {
                var json = JsonConvert.SerializeObject(itensPedidos);

                var response = await client.PostAsync(Aplicacao.Url("contas", "RealizarPedido", idConta.ToString()), Aplicacao.Content(json));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Pedido>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CancelarPedido(int idPedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new object());

                var response = await client.PostAsync(Aplicacao.Url("contas", "CancelarPedido", idPedido.ToString()), 
                    Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ProcessarPedido(int idPedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new object());

                var response = await client.PostAsync(Aplicacao.Url("contas", "ProcessarPedido", idPedido.ToString()),
                    Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> FinalizarPedido(int idPedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new object());

                var response = await client.PostAsync(Aplicacao.Url("contas", "FinalizarPedido", idPedido.ToString()),
                    Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Conta> FecharConta(int idReserva)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new object());

                var response = await client.PostAsync(Aplicacao.Url("contas", "FecharConta", idReserva.ToString()),
                    Aplicacao.Content(json));

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Conta>(response.Content.ReadAsStringAsync().Result);
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
