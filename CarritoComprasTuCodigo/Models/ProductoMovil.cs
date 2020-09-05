using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoComprasTuCodigo.Models
{
    public class ProductoMovil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double? Precio { get; set; }
        public DateTime? FechaCreacion { get; set; }
        string Foto { get; set; }        
        public byte[] Image { get; set; }
        public Generico Categoria { get; set; }

        public ProductoMovil()
        {
            Categoria = new Generico();
        }
    }
}