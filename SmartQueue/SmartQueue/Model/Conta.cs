using System;

namespace SmartQueue.Model
{
    public sealed class Conta
    {
        public int Id { get; set; }

        public int ReservaId { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime DataFechamento { get; set; }

        public decimal Valor { get; set; }
    }
}
