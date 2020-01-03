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
            if (CassandraDataLayer.DataProvider.DodajTemu(tema))
                return Redirect("~/Home");
            return View();
        }
    }
}