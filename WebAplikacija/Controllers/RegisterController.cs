using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {
            korisnik.korisnikID = Guid.NewGuid().ToString("N");
            if (CassandraDataLayer.DataProvider.DodajKorisnika(korisnik))
                return RedirectToAction("Index", "StartPage");
            else
                return View();
        }
    }
}