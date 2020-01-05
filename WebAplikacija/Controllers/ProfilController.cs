using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult ChangeProfile(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {
            korisnik.korisnikID = CassandraDataLayer.DataStore.GetInstance().GetKorisnik().korisnikID;
            if (CassandraDataLayer.DataProvider.azurirajKorisnika(korisnik))
                return Redirect("~/Home");

            return View();
        }
        [HttpGet]
        public ActionResult ChangeProfile()
        {
            return View();
        }
    }
}