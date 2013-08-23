using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadMagazine.WebUI.Controllers
{
    public class ChannelController : Controller
    {
        //
        // GET: /Channel/

        public ActionResult Index()
        {
            var lila = "lila";            
            var coneko = "popo";
            var pepe = "pepe";
            var popo = "copia";
            return View();
        }

    }
}
