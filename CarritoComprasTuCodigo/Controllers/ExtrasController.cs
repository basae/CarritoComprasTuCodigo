using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoComprasTuCodigo.Controllers
{
    public class ExtrasController : Controller
    {
        //
        // GET: /Extras/

        public ActionResult condicionesservicio()
        {
            return View();
        }
        public ActionResult politicas()
        {
            return View();
        }

    }
}
