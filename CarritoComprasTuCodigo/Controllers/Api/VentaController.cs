using CarritoComprasTuCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarritoComprasTuCodigo.Controllers.Api
{    
    public class VentaController : ApiController
    {
        private CarritoBDEntities ce = new CarritoBDEntities();

        public VentaMovil Post(IEnumerable<CarritoItem> compras)
        {
            VentaMovil Response = new VentaMovil();
            try
            {
                Venta nuevaVenta = new Venta();
                if (compras != null && compras.Count() > 0)
                {
                    compras = compras.Select(x =>
                    {
                        x.Producto = (from r in ce.Producto
                                      where r.Id == x.Producto.Id
                                      select r
                                     ).FirstOrDefault();
                        return x;
                    }).ToList();

                    nuevaVenta.DiaVenta = DateTime.Now;
                    nuevaVenta.Subtotal = compras.Sum(x => x.Producto.Precio * x.Cantidad);
                    nuevaVenta.Iva = Math.Round(nuevaVenta.Subtotal.Value * 0.16, 2);
                    nuevaVenta.Total = Math.Round(nuevaVenta.Subtotal.Value + nuevaVenta.Iva.Value, 2);

                    nuevaVenta.ListaVenta = (from producto in compras
                                             select new ListaVenta
                                             {
                                                 ProductoId = producto.Producto.Id,
                                                 Cantidad = producto.Cantidad,
                                                 Total = producto.Cantidad * producto.Producto.Precio
                                             }).ToList();
                    ce.Venta.Add(nuevaVenta);
                    ce.SaveChanges();

                    Response.Id = nuevaVenta.Id;
                    Response.DiaVenta = nuevaVenta.DiaVenta;
                    Response.Iva = nuevaVenta.Iva;
                    Response.Subtotal = nuevaVenta.Subtotal;
                    Response.Total = nuevaVenta.Total;
                    Response.ListaVenta = nuevaVenta.ListaVenta.Select(x => new ListaVentaMovil { Id=x.Id, Cantidad= x.Cantidad, ProductoId=x.ProductoId, Total= x.Total });

                }
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            return Response;
        }
    }
}
