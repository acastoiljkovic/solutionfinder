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
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            CassandraDataLayer.DataProvider.ObrisiDelatnost(id);
            return RedirectToAction("ViewDelatnost");
        }
        public ActionResult Edit(string id)
        {
            return View(CassandraDataLayer.DataProvider.VratiDelatnost(id));
        }
        [HttpPost]
        public ActionResult Edit(CassandraDataLayer.QueryEntities.Delatnost delatnost)
        {
            CassandraDataLayer.DataProvider.azurirajDelatnost(delatnost);
            return RedirectToAction("ViewDelatnost");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CassandraDataLayer.QueryEntities.Delatnost delatnost)
        {
            delatnost.delatnostID = Guid.NewGuid().ToString("N");
            CassandraDataLayer.DataProvider.DodajDelatnost(delatnost);
            return RedirectToAction("ViewDelatnost");
        }
    }
}