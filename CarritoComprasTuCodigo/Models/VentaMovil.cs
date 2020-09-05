using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoComprasTuCodigo.Models
{
    public class VentaMovil
    {
        public int? Id { get; set; }
        public DateTime? DiaVenta { get; set; }
        public double? Subtotal { get; set; }
        public double? Iva { get; set; }
        public double? Total { get; set; }
        public IEnumerable<ListaVentaMovil> ListaVenta { get; set; }
    }
}