using SmartQueue.Model;
using SQLite;
using System;
using System.IO;

namespace SmartQueue.DAL
{
    public sealed class StorageConta
    {
        private SQLiteConnection conexao;


        public StorageConta()
        {
            conexao = new SQLiteConnection(Local("smartQueue.sqlite"));
            conexao.CreateTable<Conta>();
        }

        public string Local(string nomeArquivo)
        {
            string caminhoDaPasta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string caminhoBanco = Path.Combine(caminhoDaPasta, nomeArquivo);

            return caminhoBanco;
        }

        public void Incluir(Conta conta)
        {
            conexao.Insert(conta);
        }

        public Conta Consultar()
        {
            return conexao.Table<Conta>().FirstOrDefault();
        }

        public void Alterar(Conta conta)
        {
            conexao.Update(conta);
        }

        public void Excluir()
        {
            conexao.DeleteAll<Conta>();
        }

        public int Count()
        {
            return conexao.Table<Conta>().Count();
        }
    }
}
