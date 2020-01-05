using Cassandra;
using CassandraDataLayer.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace CassandraDataLayer
{
    public static class DataProvider
    {

    private static Korisnik _user = null;

        public static bool SetKorisnik(Korisnik k)
        {
            try
            {
                _user = k;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static Korisnik GetKorisnik()
        {
            return _user;
        }

        #region Delatnost

        public static Delatnost VratiDelatnost(string delatnostID)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Delatnost delatnost = new Delatnost();

                if (session == null)
                    return null;

                Row delatnostData = session.Execute("select * from \"Delatnost\" where \"delatnostID\"='"+delatnostID+"'").FirstOrDefault();

                if (delatnostData != null)
                {
                    OdradiDodeleDelatnost(delatnost, delatnostData);
                }

                return delatnost;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static List<Delatnost> VratiSveDelatnosti()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                List<Delatnost> delatnosti = new List<Delatnost>();

                if (session == null)
                    return null;

                var delatnostiData = session.Execute("select * from \"Delatnost\"");

                foreach(var delatnostData in delatnostiData)
                {
                    Delatnost delatnost = new Delatnost();
                    OdradiDodeleDelatnost(delatnost, delatnostData);
                    delatnosti.Add(delatnost);
                }

                return delatnosti;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private static void OdradiDodeleDelatnost(Delatnost delatnost, Row delatnostData)
        {
            try
            {
                if (delatnostData["delatnostID"] != null)
                    delatnost.delatnostID = delatnostData["delatnostID"].ToString();
                else
                    delatnost.delatnostID = "";

                if (delatnostData["naziv"] != null)
                    delatnost.naziv = delatnostData["naziv"].ToString();
                else
                    delatnost.naziv = "";

                if (delatnostData["opis"] != null)
                    delatnost.opis = delatnostData["opis"].ToString();
                else
                    delatnost.opis = "";

                if (delatnostData["tip"] != null)
                    delatnost.tip = delatnostData["tip"].ToString();
                else
                    delatnost.tip = "";
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static bool azurirajDelatnost(Delatnost delatnost)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet delatnostData = session.Execute("update \"Delatnost\" set naziv = '" + delatnost.naziv+ "' ,"
                    + " opis = '"+ delatnost.opis+ "', tip = '"+delatnost.tip+"'" +
                    "  where \"delatnostID\" = '" + delatnost.delatnostID + "'");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool DodajDelatnost(Delatnost delatnost)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet delatnostPodaci = session.Execute("insert into \"Delatnost\" (\"delatnostID\", naziv, opis, tip)"+ 
                    "values ('" + delatnost.delatnostID + "', '"+delatnost.naziv+"', '"+delatnost.opis+"', '"+delatnost.tip+"')");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiDelatnost(string delatnostID)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet delatnostData = session.Execute("delete from \"Delatnost\" where \"delatnostID\" = '" + delatnostID + "'");

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        #endregion

        #region Korisnik

        public static Korisnik VratiKorisnika(string korisnikID)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Korisnik korisnik = new Korisnik();

                if (session == null)
                    return null;

                Row korisnikData = session.Execute("select * from \"Korisnik\" where \"korisnikID\"='" + korisnikID + "'").FirstOrDefault();

                if (korisnikData != null)
                {
                    OdradiDodeleKorisnik(korisnik, korisnikData);
                }

                return korisnik;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Korisnik> VratiSveKorisnike()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                List<Korisnik> korisnici = new List<Korisnik>();

                if (session == null)
                    return null;

                var korisniciData = session.Execute("select * from \"Korisnik\"");

                foreach (var korisnikData in korisniciData)
                {
                    Korisnik korisnik = new Korisnik();
                    OdradiDodeleKorisnik(korisnik, korisnikData);
                    korisnici.Add(korisnik);
                }

                return korisnici;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static void OdradiDodeleKorisnik(Korisnik korisnik, Row korisnikData)
        {
            try
            {
                if (korisnikData["korisnikID"] != null)
                    korisnik.korisnikID = korisnikData["korisnikID"].ToString();
                else
                    korisnik.korisnikID = "";

                if (korisnikData["ime"] != null)
                    korisnik.ime = korisnikData["ime"].ToString();
                else
                    korisnik.ime = "";

                if (korisnikData["prezime"] != null)
                    korisnik.prezime = korisnikData["prezime"].ToString();
                else
                    korisnik.prezime = "";

                if (korisnikData["email"] != null)
                    korisnik.email = korisnikData["email"].ToString();
                else
                    korisnik.email = "";

                if (korisnikData["telefon"] != null)
                    korisnik.telefon = korisnikData["telefon"].ToString();
                else
                    korisnik.telefon = "";

                if (korisnikData["vebsajt"] != null)
                    korisnik.vebsajt = korisnikData["vebsajt"].ToString();
                else
                    korisnik.vebsajt = "";

                if (korisnikData["sifra"] != null)
                    korisnik.sifra = korisnikData["sifra"].ToString();
                else
                    korisnik.sifra = "";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool DodajKorisnika(Korisnik korisnik)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet delatnostPodaci = session.Execute("insert into \"Korisnik\" " +
                    "(\"korisnikID\", email, ime, prezime, telefon, vebsajt, sifra)" +
                    " values ('"+korisnik.korisnikID+"', '"+korisnik.email+"', " +
                    "'"+korisnik.ime+"', '"+korisnik.prezime+"', '"+korisnik.telefon+"', " +
                    "'"+korisnik.vebsajt+ "', '" + korisnik.sifra+"')");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool azurirajKorisnika(Korisnik korisnik)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet korisnikData = session.Execute("update \"Korisnik\" set email = '"+korisnik.email+"' ," +
                    "ime='"+korisnik.ime+ "', prezime='"+korisnik.prezime+"',telefon='"+korisnik.telefon+"',vebsajt='"+korisnik.vebsajt+"' " +
                    "  where \"korisnikID\" = '" + korisnik.korisnikID + "'");
                CassandraDataLayer.DataStore.GetInstance().GetKorisnik().email = korisnik.email;
                CassandraDataLayer.DataStore.GetInstance().GetKorisnik().ime = korisnik.ime;
                CassandraDataLayer.DataStore.GetInstance().GetKorisnik().prezime = korisnik.prezime;
                CassandraDataLayer.DataStore.GetInstance().GetKorisnik().telefon = korisnik.telefon;
                CassandraDataLayer.DataStore.GetInstance().GetKorisnik().vebsajt = korisnik.vebsajt;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiKorisnika(string korisnikID)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet korisnikData = session.Execute("delete from \"Korisnik\" where \"korisnikID\" = '" + korisnikID + "'");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public  static Korisnik ulogujKorisnika(Korisnik kor)
        {
            ISession session = SessionManager.GetSession();
            List<Korisnik> korisnici = new List<Korisnik>();

            if (session == null)
                return null;

            var korisniciData = session.Execute("select * from \"Korisnik\" " +
                "where \"email\" = '" + kor.email + "' and \"sifra\" = '" + kor.sifra +"' allow filtering");

            foreach (var korisnikData in korisniciData)
            {
                Korisnik korisnik = new Korisnik();
                OdradiDodeleKorisnik(korisnik, korisnikData);
                korisnici.Add(korisnik);
            }
            if (korisnici.Count > 0)
                return korisnici[0];
            return null;
        }

        #endregion

        #region Tema

        public static Tema VratiTemu(string delatnostID, string korisnikID, string temaID)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Tema tema = new Tema();

                if (session == null)
                    return null;

                var temaData = session.Execute("select * from \"Tema\" where \"delatnostID\" = '"+delatnostID+"' and \"korisnikID\" = '"+korisnikID+"' and \"temaID\" = '"+temaID+"'").FirstOrDefault();

                if (temaData != null)
                {
                    OdradiDodeleTema(tema, temaData);
                }

                return tema;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Tema> VratiSveTeme()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                List<Tema> teme = new List<Tema>();

                if (session == null)
                    return null;

                var temeData = session.Execute("select * from \"Tema\"");

                foreach (var temaData in temeData)
                {
                    Tema tema = new Tema();
                    OdradiDodeleTema(tema, temaData);
                    teme.Add(tema);
                }

                return teme;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Tema> vratiSveTemeKorisnika(string id)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                List<Tema> teme = new List<Tema>();

                if (session == null)
                    return null;

                var temeData = session.Execute("select * from \"Tema\" where \"korisnikID\" = '"+id+ "' allow filtering;");

                foreach (var temaData in temeData)
                {
                    Tema tema = new Tema();
                    OdradiDodeleTema(tema, temaData);
                    teme.Add(tema);
                }

                return teme;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static bool azurirajTemu(Tema tema)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet temaData = session.Execute("update \"Tema\" set datumkreiranja = '" + tema.datumKreiranja + "' ," +
                    "naslov='" + tema.naslov + "', sadrzaj='" + tema.sadrzaj + "',vidljivost='" + tema.vidljivost+ "'" +
                    "  where \"korisnikID\" = '" + tema.korisnikID + "' and \"delatnostID\" = '"+tema.delatnostID+"' and" +
                    "\"temaID\" = '"+tema.temaID+"'");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private static void OdradiDodeleTema(Tema tema, Row temaData)
        {
            try
            {
                if (temaData["delatnostID"] != null)
                    tema.delatnostID = temaData["delatnostID"].ToString();
                else
                    tema.delatnostID = "";

                if (temaData["korisnikID"] != null)
                    tema.korisnikID = temaData["korisnikID"].ToString();
                else
                    tema.korisnikID = "";

                if (temaData["temaID"] != null)
                    tema.temaID = temaData["temaID"].ToString();
                else
                    tema.temaID = "";

                if (temaData["datumkreiranja"] != null)
                    tema.datumKreiranja = temaData["datumkreiranja"].ToString();
                else
                    tema.datumKreiranja = "";

                if (temaData["sadrzaj"] != null)
                    tema.sadrzaj = temaData["sadrzaj"].ToString();
                else
                    tema.sadrzaj = "";

                if (temaData["vidljivost"] != null)
                    tema.vidljivost = temaData["vidljivost"].ToString();
                else
                    tema.vidljivost = "";

                if (temaData["naslov"] != null)
                    tema.naslov = temaData["naslov"].ToString();
                else
                    tema.naslov = "";
                if(temaData["komentari"] != null)
                    tema.komentari = (SortedDictionary<string, IEnumerable<string>>)temaData["komentari"];


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static bool DodajIzmeniKomentar(string delatnostID, string korisnikID, string temaID, string korisnikKojiKomentarise, string komentar, string stariKomentar)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                Tema tema = VratiTemu(delatnostID, korisnikID, temaID);

                string upit = "update \"Tema\" set komentari = {";

                bool korisnikovPrviKomentar = true;

                if (tema.komentari != null)
                {
                    foreach (KeyValuePair<string, IEnumerable<string>> kv in tema.komentari)
                    {
                        upit += "'" + kv.Key + "':['";

                        bool promenjen = false;

                        foreach (string s in kv.Value)
                        {
                            if (s.Equals(stariKomentar) && !promenjen && kv.Key.Equals(korisnikKojiKomentarise))
                            {
                                upit += komentar;
                                promenjen = true;
                                korisnikovPrviKomentar = false;
                            }
                            else
                            {
                                upit += s;
                            }

                            upit += "', '";
                        }

                        if (kv.Key.Equals(korisnikKojiKomentarise) && stariKomentar.Equals(""))
                        {
                            korisnikovPrviKomentar = false;
                            upit += komentar;
                        }
                        else
                        {
                            upit = upit.Substring(0, upit.Length - 4);
                        }

                        upit += "'], ";
                    }

                    if (korisnikovPrviKomentar)
                    {
                        upit += "'" + korisnikKojiKomentarise + "':['" + komentar;
                    }
                    else
                    {
                        upit = upit.Substring(0, upit.Length - 4);
                    }
                }
                else
                {
                    upit += " {'" + korisnikKojiKomentarise + "':['" + komentar;
                }

                upit += "']} where \"delatnostID\" = '" + delatnostID + "' and \"korisnikID\" = '"
                        + korisnikID + "' and \"temaID\" = '" + temaID + "';";
                session.Execute(upit);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public static bool DodajTemu(Tema tema)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet temaPodaci = session.Execute("insert into \"Tema\" (\"delatnostID\", \"korisnikID\", \"temaID\", datumKreiranja, sadrzaj, vidljivost, naslov)" +
                    " values ('" + tema.delatnostID + "', '" + tema.korisnikID + "', '" + tema.temaID + "', '" + tema.datumKreiranja + "', '" + tema.sadrzaj + "', '" + tema.vidljivost + "', '"+tema.naslov+"')");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiTemu(string delatnostID, string korisnikID, string temaID)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet korisnikData = session.Execute("delete from \"Tema\" where \"delatnostID\" = '" + delatnostID + "' and "+
                    "\"korisnikID\" = '"+korisnikID+"' and \"temaID\" = '"+temaID+"'");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

    }
}
