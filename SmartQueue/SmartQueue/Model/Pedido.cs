using System;

namespace SmartQueue.Model
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ContaId { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; }
    }
}
