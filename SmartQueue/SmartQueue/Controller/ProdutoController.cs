using SmartQueue.Service;
using SmartQueue.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using SmartQueue.Utils;

namespace SmartQueue.Controller
{
    public sealed class ProdutoController
    {
        private ProdutoService service;

        public ProdutoController()
        {
            service = new ProdutoService();
        }

        public async Task<IEnumerable<ItemCardapio>> Cardapio()
        {
            List<Produto> produtos = await ListarProdutos();
            List<Categoria> categorias = await ListarCategorias();

            IEnumerable<ItemCardapio> cardapio = from produto in produtos
                                                 join categoria in categorias
                                                 on produto.CategoriaId equals categoria.Id
                                                 orderby categoria.Nome
                                                 select new ItemCardapio
                                                 {
                                                     ProdutoId = produto.Id,
                                                     ProdutoNome = produto.Nome,
                                                     ProdutoValor = produto.Valor,
                                                     CategoriaId = categoria.Id,
                                                     CategoriaNome = categoria.Nome
                                                 };

            return cardapio;
        }

        public async Task<Produto> ConsultarProduto(int id)
        {
            return await service.ConsultarProduto(id);
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            return await service.ListarProdutos();
        }

        public async Task<List<ItemRanking>> Ranking()
        {
            List<Produto> produtos;
            List<ItemRanking> ranking;

            try
            {
                ranking = new List<ItemRanking>();
                produtos = await service.Ranking();

                foreach (var produto in produtos)
                {
                    ItemRanking item = new ItemRanking();
                    item.IdProduto = produto.Id;
                    item.Nome = produto.Nome;
                    item.Valor = produto.Valor;
                    item.Imagem = Aplicacao.ConverteImagem(produto.Imagem);

                    ranking.Add(item);
                }

                return ranking;
            }
            catch (Exception ex)
            {
                new ApplicationException(string.Format("Erro ao montar o Raking.\n{0}", ex.Message));
                return new List<ItemRanking>();
            }
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            return await service.ListarCategorias();
        }

        public async Task<string> ListarProdutosPendentes()
        {
            return await service.ListarPedidosPendentes();
        }
    }
}
