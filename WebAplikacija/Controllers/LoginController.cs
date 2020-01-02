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
        private CassandraDataLayer.QueryEntities.Korisnik _user;

        public LoginController()
        {
            _user = null;
        }

        public bool checkIsLogIn()
        {
            if (_user != null)
                return true;
            return false;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Korisnik korisnik)
        {
            _user = CassandraDataLayer.DataProvider.ulogujKorisnika(korisnik);
            if (_user != null)
            {
                return RedirectToAction("Index", "StartPage");
            }

            return View();
        }

    }
}