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
        public ActionResult Index()
        {
            return View();
        }
        [Route("korisnikview/korisnik/{id}")]
        public ActionResult Korisnik(string id)
        {
            return View(CassandraDataLayer.DataProvider.VratiKorisnika(id));
        }
    }
}