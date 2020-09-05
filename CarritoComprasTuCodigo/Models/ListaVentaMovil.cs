using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoComprasTuCodigo.Models
{
    public class ListaVentaMovil
    {
        public int? Id { get; set; }
        public int? VentaId { get; set; }
        public int? ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public double? Total { get; set; }
    }
}