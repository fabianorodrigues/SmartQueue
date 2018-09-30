using SmartQueue.Service;
using SmartQueue.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
                                                 orderby categoria.Caracteristica
                                                 select new ItemCardapio
                                                 {
                                                     ProdutoId = produto.Id,
                                                     ProdutoNome = produto.Nome,
                                                     ProdutoValor = produto.Valor,
                                                     CategoriaId = categoria.Id,
                                                     CategoriaCaracteristica = categoria.Caracteristica
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

        public async Task<List<Produto>> Ranking()
        {
            return await service.Ranking();
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
