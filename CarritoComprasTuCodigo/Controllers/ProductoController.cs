using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarritoComprasTuCodigo.Models;
using System.IO;

namespace CarritoComprasTuCodigo.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/

        private CarritoBDEntities ce = new CarritoBDEntities();
        [Authorize]
        public ActionResult Index()
        {

            return View(ce.Producto.ToList().Where(x => x.Estatus == true).OrderBy(x=>x.Nombre));
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult EditProduct(long id)
        {
            ViewBag.Categories = (from c in ce.Categoria.ToList()
                           select new SelectListItem
                           {
                               Text = c.Nombre,
                               Value = c.Id.ToString()
                           }).ToList();
            
            Producto product=ce.Producto.Find(id);
            if (product != null)
            {
                ViewBag.Base64Img = product.Image != null ? Convert.ToBase64String(product.Image) : "";
                return View(product);
            }
            else
                return View(product);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult EditProduct(Producto product)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = ce.Producto.Find(product.Id);
                if (product.FileImg != null)
                {
                    MemoryStream ms = new MemoryStream();
                    product.FileImg.InputStream.CopyTo(ms);
                    updateProduct.Image = ms.ToArray();
                }
                //updateProduct = product;
                ce.SaveChanges();
            }
            return RedirectToAction("AdministrarProductos");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AdministrarProductos()
        {

            return View(ce.Producto.ToList().Where(x=>x.Estatus==true).OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(long id)
        {
            

            Producto product = ce.Producto.Find(id);
            product.Estatus = false;
            ce.SaveChanges();
            return RedirectToAction("AdministrarProductos");
        }

    }
}
