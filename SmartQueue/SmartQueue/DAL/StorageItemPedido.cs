using SmartQueue.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace SmartQueue.DAL
{
    public sealed class StorageItemPedido
    {
        private SQLiteConnection conexao;

        public StorageItemPedido()
        {
            conexao = new SQLiteConnection(Local("smartQueue.sqlite"));
            conexao.CreateTable<ItemPedido>();
        }

        public string Local(string nomeArquivo)
        {
            string caminhoDaPasta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string caminhoBanco = Path.Combine(caminhoDaPasta, nomeArquivo);

            return caminhoBanco;
        }

        public void Incluir(ItemPedido itemPedido)
        {
            conexao.Insert(itemPedido);
        }

        public List<ItemPedido> Listar()
        {
            return conexao.Table<ItemPedido>().ToList();
        }

        public void Alterar(ItemPedido itemPedido)
        {
            conexao.Update(itemPedido);
        }

        public void Excluir(ItemPedido itemPedido)
        {
            conexao.Delete(itemPedido);
        }

        public void ExcluirTodos()
        {
            conexao.DeleteAll<ItemPedido>();
        }

        public int Count()
        {
            return conexao.Table<ItemPedido>().Count();
        }
    }
}
