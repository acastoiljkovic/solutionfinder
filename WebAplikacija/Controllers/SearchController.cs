using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplikacija.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchText,string delatnostID)
        {
            List<CassandraDataLayer.QueryEntities.Tema> lista = new List<CassandraDataLayer.QueryEntities.Tema>();

            foreach(CassandraDataLayer.QueryEntities.Tema t in CassandraDataLayer.DataProvider.VratiSveTeme())
            {
                if (t.naslov.Contains(searchText) && (delatnostID == "nema" || t.delatnostID == delatnostID))
                    lista.Add(t);
            }

            return View("Index",lista);
        }
    }
}