using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class DelatnostController : Controller
    {
        // GET: Delatnost
        public ActionResult Index(string id)
        {
            
            List<CassandraDataLayer.QueryEntities.Delatnost> delatnosti = new List<CassandraDataLayer.QueryEntities.Delatnost>();
            delatnosti.Add(CassandraDataLayer.DataProvider.VratiDelatnost(id));
            return View(delatnosti);
        }

        public ActionResult ViewDelatnost()
        {
            return View("Index",CassandraDataLayer.DataProvider.VratiSveDelatnosti());
        }

        [HttpPost]
        public ActionResult ViewDelatnost(string iden)
        {
            return RedirectToAction("Index", "Delatnost",new { id = iden });
        }
    }
}