using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaDeportiva.API.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public decimal MontoTotal { get; set; }

        [Required]
        public string Estado { get; set; } = "Pendiente";

        // Relación
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}