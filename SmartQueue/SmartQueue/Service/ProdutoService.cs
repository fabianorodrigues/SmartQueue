using Newtonsoft.Json;
using SmartQueue.Model;
using SmartQueue.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartQueue.Service
{
    public class ProdutoService
    {
        private HttpClient client;

        public ProdutoService()
        {
            client = new HttpClient();
        }

        public async Task<Produto> ConsultarProduto(int id)
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("produtos", "BuscarPorId", id.ToString()));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Produto>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("produtos", "listarcategorias"));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Categoria>>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("produtos", "listarprodutos"));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Produto>>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Produto>> Ranking()
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("produtos", "ConsultarRanking"));
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<Produto>>(response.Content.ReadAsStringAsync().Result);
                else
                    throw new ApplicationException(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ListarPedidosPendentes()
        {
            try
            {
                var response = await client.GetAsync(Aplicacao.Url("produtos", "ListarPedidosPendentes"));
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

    }

}
