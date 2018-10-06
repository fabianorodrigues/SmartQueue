using SmartQueue.Model;
using SQLite;
using System;
using System.IO;

namespace SmartQueue.DAL
{
    public class StorageReserva
    {
        private SQLiteConnection conexao;


        public StorageReserva()
        {
            conexao = new SQLiteConnection(Local("smartQueue.sqlite"));
            conexao.CreateTable<Reserva>();
        }

        public string Local(string nomeArquivo)
        {
            string caminhoDaPasta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string caminhoBanco = Path.Combine(caminhoDaPasta, nomeArquivo);

            return caminhoBanco;
        }

        public void Incluir(Reserva reserva)
        {
            conexao.Insert(reserva);
        }

        public Reserva Consultar()
        {
            return conexao.Table<Reserva>().FirstOrDefault();
        }

        public void Alterar(Reserva reserva)
        {
            conexao.Update(reserva);
        }

        public void Excluir()
        {
            conexao.DeleteAll<Reserva>();
        }

        public int Count()
        {
            return conexao.Table<Reserva>().Count();
        }
    }
}
