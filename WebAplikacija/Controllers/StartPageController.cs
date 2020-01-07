using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class StartPageController : Controller
    {
        // GET: StartPage
        public ActionResult Index()
        {
            if (CassandraDataLayer.DataStore.GetInstance().GetKorisnik().korisnikID == null)
                return View();
            else
            {
                return Redirect("~/Home");
            }
        }
    }
}