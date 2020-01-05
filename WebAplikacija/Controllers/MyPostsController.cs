using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class MyPostsController : Controller
    {
        // GET: MyPosts
        public ActionResult Index()
        {
            List<CassandraDataLayer.QueryEntities.Tema> lista = CassandraDataLayer.DataProvider.vratiSveTemeKorisnika(CassandraDataLayer.DataStore.GetInstance().GetKorisnik().korisnikID);

            return View(lista);
        }
        public ActionResult Delete(string id, string korID, string delaID)
        {
            CassandraDataLayer.DataProvider.ObrisiTemu(delaID, korID, id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, string korID, string delaID)
        {

            return View(CassandraDataLayer.DataProvider.VratiTemu(delaID,korID,id));
        }
        [HttpPost]
        public ActionResult Edit(CassandraDataLayer.QueryEntities.Tema tema)
        {
            CassandraDataLayer.DataProvider.azurirajTemu(tema);
            return RedirectToAction("Index");
        }
    }
}