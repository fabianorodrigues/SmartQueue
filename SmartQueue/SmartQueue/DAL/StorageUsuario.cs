using SmartQueue.Model;
using SQLite;
using System;
using System.IO;

namespace SmartQueue.DAL
{
    public sealed class StorageUsuario
    {
        private SQLiteConnection conexao;


        public StorageUsuario()
        {
            conexao = new SQLiteConnection(Local("smartQueue.sqlite"));
            conexao.CreateTable<Usuario>();
        }

        public string Local(string nomeArquivo)
        {
            string caminhoDaPasta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string caminhoBanco = Path.Combine(caminhoDaPasta, nomeArquivo);

            return caminhoBanco;
        }

        public void Incluir(Usuario usuario)
        {
            conexao.Insert(usuario);
        }

        public Usuario Consultar()
        {
            return conexao.Table<Usuario>().FirstOrDefault();
        }

        public void Alterar(Usuario usuario)
        {
            conexao.Update(usuario);
        }

        public void Excluir()
        {
            conexao.DeleteAll<Usuario>();
        }

        public int Count()
        {
            return conexao.Table<Usuario>().Count();
        }
    }
}
