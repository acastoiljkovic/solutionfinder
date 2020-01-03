using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {
            CassandraDataLayer.DataStore.GetInstance().SetKorisnik( CassandraDataLayer.DataProvider.ulogujKorisnika(korisnik));
            if (CassandraDataLayer.DataStore.GetInstance().GetKorisnik() != null)
            {
                return RedirectToAction("Index", "StartPage");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            CassandraDataLayer.DataStore.GetInstance().SetKorisnik(null);
            return Redirect("~/Login");
        }
    }
}