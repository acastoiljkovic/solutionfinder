using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class KorisnikViewController : Controller
    {
        // GET: KorisnikView
        public ActionResult Index(string id)
        {
            return View(CassandraDataLayer.DataProvider.VratiKorisnika(id));
        }

        [HttpPost]
        public ActionResult Korisnik(string iden)
        {
            return RedirectToAction("Index","KorisnikView",new { id = iden });
        }
    }
}