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

        public ActionResult ViewPost(string delatnostID, string korisnikID, string temaID)
        {
            return View(CassandraDataLayer.DataProvider.VratiTemu(delatnostID,korisnikID,temaID));
        }

        [HttpPost]
        public ActionResult Index(CassandraDataLayer.QueryEntities.Tema tema)
        {
            tema.korisnikID = CassandraDataLayer.DataStore.GetInstance().GetKorisnik().korisnikID;
            tema.temaID = Guid.NewGuid().ToString("N");
            tema.datumKreiranja = DateTime.Now.ToString();
            if (CassandraDataLayer.DataProvider.DodajTemu(tema))
                return Redirect("~/Home");
            return View();
        }

        [HttpPost]
        public ActionResult AddReply(string korisnikID,string temaID,string sadrzaj)
        {
            CassandraDataLayer.QueryEntities.Odgovor o = new CassandraDataLayer.QueryEntities.Odgovor();
            o.KorisnikID = korisnikID;
            o.TemaID = temaID;
            o.OdgovorID = Guid.NewGuid().ToString("N");
            o.Datum = DateTime.Now.ToString();
            o.Glasovi = 0;
            o.Sadrzaj = sadrzaj;
            if (CassandraDataLayer.DataProvider.DodajOdgovor(o))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult DeleteReply(string odgovorID, string korisnikID, string temaID)
        {
            if(CassandraDataLayer.DataProvider.ObrisiOdgovor(odgovorID,korisnikID,temaID))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }

        [HttpPost]
        public ActionResult DeleteComment(string odgovorID, string korisnikID, string temaID, string komKorisnikID)
        {
            if (CassandraDataLayer.DataProvider.ObrisiKomentareSaOdgovora(odgovorID, korisnikID, temaID, komKorisnikID))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }
        [HttpPost]
        public ActionResult AddComment(string odgovorID, string korisnikID, string temaID, string komKorisnikID,string komentar)
        {
            if (CassandraDataLayer.DataProvider.DodajKomentarOdgovoru(odgovorID, korisnikID, temaID, komKorisnikID, komentar))
                return Redirect("~/Home");
            return Redirect("~/Login");
        }
        [HttpPost]
        public ActionResult AddVote(string odgovorID, string korisnikID, string temaID, int glasovi)
        {
            glasovi++;
            CassandraDataLayer.DataProvider.PromeniGlas(odgovorID, korisnikID, temaID, glasovi);
            return Redirect("~/Home");
        }
        [HttpPost]
        public ActionResult RemoveVote(string odgovorID, string korisnikID, string temaID, int glasovi)
        {
            glasovi--;
            CassandraDataLayer.DataProvider.PromeniGlas(odgovorID, korisnikID, temaID, glasovi);
            return Redirect("~/Home");
        }
    }
}