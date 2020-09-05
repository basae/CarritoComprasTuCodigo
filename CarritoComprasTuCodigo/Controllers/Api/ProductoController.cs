using CarritoComprasTuCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebMatrix.WebData;

namespace CarritoComprasTuCodigo.Controllers.Api
{
    public class ProductoController : ApiController
    {

        private CarritoBDEntities ce = new CarritoBDEntities();
        //api/Producto
        public IEnumerable<ProductoMovil> Get()
        {
            IEnumerable<ProductoMovil> Respuesta = null;

            if (Request.Headers.Contains("Authorization"))
            {
                try
                {
                    byte[] datos = Convert.FromBase64String(Request.Headers.GetValues("Authorization").FirstOrDefault());
                    string[] elements = Encoding.UTF8.GetString(datos).Split('|');
                    if (elements.Length == 2)
                    {
                        if (WebSecurity.Login(elements[0], elements[1]))
                        {

                            return ce.Producto.ToList().Where(x => x.Estatus == true).OrderBy(x => x.Nombre).Select(x => new ProductoMovil
                            {
                                Id = x.Id,
                                Nombre = x.Nombre,
                                Precio = x.Precio,
                                Image = x.Image,
                                FechaCreacion = x.FechaCreacion,
                                Categoria = x.CategoriaId.HasValue ?
                                new Generico { Id = x.Categoria.Id, Nombre = x.Categoria.Nombre } : new Generico()
                            });
                        }
                        else
                            throw new Exception("usuario invalido.");

                    }
                    else
                        throw new Exception("esquema de seguridad incompatible.");
                    return Respuesta;
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                }
            }
            else
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Header de seguridad requerido"));
            }
        }
        //api/Producto/5
        public ProductoMovil Get(long Id)
        {
            if (Request.Headers.Contains("Authorization"))
            {
                try
                {
                    byte[] datos = Convert.FromBase64String(Request.Headers.GetValues("Authorization").FirstOrDefault());
                    string[] elements = Encoding.UTF8.GetString(datos).Split('|');
                    if (elements.Length == 2)
                    {
                        if (WebSecurity.Login(elements[0], elements[1]))
                        {

                            var product = ce.Producto.Find(Id);
                            if (product != null)
                            {
                                return new ProductoMovil
                                {
                                    Id = product.Id,
                                    Precio = product.Precio,
                                    Categoria = product.CategoriaId.HasValue ? new Generico { Id = product.Categoria.Id, Nombre = product.Categoria.Nombre } : new Generico(),
                                    Image = product.Image,
                                    Nombre = product.Nombre,
                                    FechaCreacion = product.FechaCreacion
                                };
                            }
                            else
                                return null;
                        }
                        else
                            throw new Exception("usuario invalido.");

                    }
                    else
                        throw new Exception("esquema de seguridad incompatible.");
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                }
            }
            else
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Header de seguridad requerido"));
            }
        }

    }
}
