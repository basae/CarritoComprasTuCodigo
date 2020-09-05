using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarritoComprasTuCodigo.Models;

namespace CarritoComprasTuCodigo.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class VentaController : Controller
    {
        //
        // GET: /Venta/
        private CarritoBDEntities ce = new CarritoBDEntities();
        
        public ActionResult Index()
        {
            return View(ce.Venta.ToList().OrderBy(x => x.DiaVenta));
        }

        public ActionResult Detalles(int Id)
        {
            return View(ce.Venta.Find(Id));
        }

    }
}
