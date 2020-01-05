using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Tema tema)
        {
            tema.korisnikID = CassandraDataLayer.DataStore.GetInstance().GetKorisnik().korisnikID;
            tema.temaID = Guid.NewGuid().ToString("N");
            tema.datumKreiranja = DateTime.Now.ToString();
            if (CassandraDataLayer.DataProvider.DodajTemu(tema))
                return Redirect("~/Home");
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(string delatnostID,string korisnikID,string temaID,string komKorisnikID,string komentar)
        {
            if (CassandraDataLayer.DataProvider.DodajIzmeniKomentar(delatnostID, korisnikID, temaID, komKorisnikID, komentar, ""))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }
        [HttpPost]
        public ActionResult DeleteComment(string delatnostID, string korisnikID, string temaID, string komKorisnikID)
        {
            if (CassandraDataLayer.DataProvider.ObrisiKomentareSaTeme(delatnostID, korisnikID, temaID, komKorisnikID))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }
    }
}