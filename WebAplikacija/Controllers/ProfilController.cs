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
        public ActionResult ChangeProfile(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {

            return Redirect("~/StartPage");
        }
        [HttpGet]
        public ActionResult ChangeProfile()
        {
            return View();
        }
    }
}