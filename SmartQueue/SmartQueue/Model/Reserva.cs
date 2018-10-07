using System;
using SQLite;
using Newtonsoft.Json;

namespace SmartQueue.Model
{
    [Table("Reserva")]
    public class Reserva
    {
        [JsonProperty("id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonProperty("mesaId")]
        public int MesaId { get; set; }

        [JsonProperty("dataReserva")]
        public DateTime DataReserva { get; set; }

        [JsonProperty("quantidadePessoas")]
        public int QuantidadePessoas { get; set; }

        [JsonProperty("tempoDeEspera")]
        public TimeSpan TempoDeEspera { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
