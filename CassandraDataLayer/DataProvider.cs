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
        #region Delatnost

        public static bool InicijalizujDelatnosti()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Delatnost delatnost = new Delatnost();

                if (session == null)
                    return false;

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('1','Informacione tehnologije' ,'Informacione tehnologije obuhvataju izucavanje, " +
                            "projektovanje, razvoj, implementaciju (sprovođenje) i podrška ili upravljanje računarskim" +
                            "informacionim sistemima (IS), softverskim aplikacijama i hardverom' ,'Nauka');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('2','Narodna knjizevnost' ,'Narodna (usmena) književnost se sastoji od priča i pesama" +
                            " koje su stvorili nepoznati talentovani pojedinci. Njihove umotvorine prenosile su se usmeno sa" +
                            " generacije na generaciju, a prvobitna priča ili pesma često je menjala svoj oblik i sadržinu jer" +
                            " je neko od prenosilaca tokom pričanja ili pevanja što dodavao ili izostavljao' ,'Knjizevnost');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('3','Psihologija' ,'Psihologija je nauka o ponašanju i mentalnim procesima. Mentalni" +
                            " procesi obuhvataju svest i nesvesne procese. Psiholozi nastoje da objasne ulogu mentalnih " +
                            "procesa u pojedinačnom i grupnom ponašanju. Oni takođe proučavaju i biološke procese koji su u" +
                            " osnovi ponašanja.' ,'Nauka');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('4','Medicina' ,'Medicina se bavi dijagnostikom, preventivom i terapijom fizičke i " +
                            "psihičke bolesti čoveka. Medicina oznacava i nauku bolesti i prakticnu primenu. Savremena " +
                            "medicina primenjuje biomedicinske nauke, biomedicinska istrazivanja, kao i geneticku i medicinsku" +
                            " tehnologiju ' ,'Nauka');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('5','Masinstvo' ,'Masinstvo ili strojarstvo jeste disciplina inženjerstva koja se odnosi na principe " +
                            "inženjerstva, fizike i nauke o materijalima za analizu, dizajn, proizvodnju i upravljanje mehaničkim sistemima." +
                            " Masinstvo je grana inženjerstva koja uključuje proizvodnju i korištenje toplote i mehaničke snage za dizajn, " +
                            "proizvodnju i operiranje mašinama i alatima. Jedna je od najstarijih i najširih disciplina inženjerstva.' ,'Konstrukcije');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('6','Gradjevinarstvo' ,'Opste gradjevinarstvo predstavlja izgradnju kompletnih stambenih " +
                            "i poslovnih zgrada, prodavnica i drugih javnih zgrada, pomoćnih i poljoprivrednih zgrada i sl., " +
                            "ili, pak, izgradnju ostalih građevina kao što su autoputevi, ulice, mostovi, tuneli, železničke pruge," +
                            " aerodromi, pristaništa i drugi priobalni objekti, sistemi za navodnjavanje, kanalizacioni sistemi, " +
                            "industrijski objekti, cevovodi i električni vodovi, sportski objekti itd.' ,'Izgradnja');");

                session.Execute("INSERT INTO \"Delatnost\"(\"delatnostID\", naziv, opis, tip)" +
                            "VALUES ('7','Arhitektura', 'U uzem smislu, arhitektura je nauka i umetnost projektovanja i " +
                            "oblikovanja zgrada, odnosno unutrašnjeg i spoljašnjeg arhitektonskog prostora: enterijera i " +
                            "eksterijera.' ,'Nauka');");


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static Delatnost VratiDelatnost(string delatnostID)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Delatnost delatnost = new Delatnost();

                if (session == null)
                    return null;

                Row delatnostData = session.Execute("select * from \"Delatnost\" where \"delatnostID\"='" + delatnostID + "'").FirstOrDefault();

                if (delatnostData != null)
                {
                    OdradiDodeleDelatnost(delatnost, delatnostData);
                }

                return delatnost;
            }
            catch (Exception e)
            {
                return new Delatnost();
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



                foreach (var delatnostData in delatnostiData)
                {
                    Delatnost delatnost = new Delatnost();
                    OdradiDodeleDelatnost(delatnost, delatnostData);
                    delatnosti.Add(delatnost);
                }

                if (delatnosti.Count() == 0)
                {
                    if (InicijalizujDelatnosti())
                    {
                        delatnostiData = session.Execute("select * from \"Delatnost\"");
                        foreach (var delatnostData in delatnostiData)
                        {
                            Delatnost delatnost = new Delatnost();
                            OdradiDodeleDelatnost(delatnost, delatnostData);
                            delatnosti.Add(delatnost);
                        }
                    }
                    else
                        return delatnosti;
                }

                return delatnosti;
            }
            catch (Exception e)
            {
                return new List<Delatnost>();
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
            catch (Exception e)
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

                RowSet delatnostData = session.Execute("update \"Delatnost\" set naziv = '" + delatnost.naziv + "' ,"
                    + " opis = '" + delatnost.opis + "', tip = '" + delatnost.tip + "'" +
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

                RowSet delatnostPodaci = session.Execute("insert into \"Delatnost\" (\"delatnostID\", naziv, opis, tip)" +
                    "values ('" + delatnost.delatnostID + "', '" + delatnost.naziv + "', '" + delatnost.opis + "', '" + delatnost.tip + "')");

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
            catch (Exception e)
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
                return new Korisnik();
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
                return new List<Korisnik>();
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

        public static bool InicijalizujKorisnike()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Delatnost delatnost = new Delatnost();

                if (session == null)
                    return false;

                session.Execute("insert into \"Korisnik\" (\"korisnikID\", email, ime, prezime, telefon, vebsajt, sifra)" +
                    " values ('1', 'stefanpetkovic@gmail.com', 'Stefan', 'Petkovic', '433232', 'stefinsajt.com', '1234');");

                session.Execute("insert into \"Korisnik\" (\"korisnikID\", email, ime, prezime, telefon, vebsajt, sifra)" +
                    " values ('2', 'todor@gmail.com', 'Todor', 'Misic', '345632', 'todinsajt.com', '1234');");

                return true;
            }
            catch (Exception e)
            {
                return false;
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
                    " values ('" + korisnik.korisnikID + "', '" + korisnik.email + "', " +
                    "'" + korisnik.ime + "', '" + korisnik.prezime + "', '" + korisnik.telefon + "', " +
                    "'" + korisnik.vebsajt + "', '" + korisnik.sifra + "')");

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

                RowSet korisnikData = session.Execute("update \"Korisnik\" set email = '" + korisnik.email + "' ," +
                    "ime='" + korisnik.ime + "', prezime='" + korisnik.prezime + "',telefon='" + korisnik.telefon + "',vebsajt='" + korisnik.vebsajt + "' " +
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

        public static Korisnik ulogujKorisnika(Korisnik kor)
        {
            ISession session = SessionManager.GetSession();
            List<Korisnik> korisnici = new List<Korisnik>();

            if (session == null)
                return null;

            var korisniciData = session.Execute("select * from \"Korisnik\" " +
                "where \"email\" = '" + kor.email + "' and \"sifra\" = '" + kor.sifra + "' allow filtering");

            foreach (var korisnikData in korisniciData)
            {
                Korisnik korisnik = new Korisnik();
                OdradiDodeleKorisnik(korisnik, korisnikData);
                korisnici.Add(korisnik);
            }
            if (korisnici.Count > 0)
                return korisnici[0];
            return new Korisnik();
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

                var temaData = session.Execute("select * from \"Tema\" where \"delatnostID\" = '" + delatnostID + "' and \"korisnikID\" = '" + korisnikID + "' and \"temaID\" = '" + temaID + "'").FirstOrDefault();

                if (temaData != null)
                {
                    OdradiDodeleTema(tema, temaData);
                }

                return tema;
            }
            catch (Exception e)
            {
                return new Tema();
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

                if (teme.Count() == 0)
                {
                    if (InicijalizujDelatnosti() && InicijalizujKorisnike() && InicijalizujTeme())
                    {
                        temeData = session.Execute("select * from \"Tema\"");
                        foreach (var temaData in temeData)
                        {
                            Tema tema = new Tema();
                            OdradiDodeleTema(tema, temaData);
                            teme.Add(tema);
                        }
                    }
                    else
                        return teme;
                }

                return teme;
            }
            catch (Exception e)
            {
                return new List<Tema>();
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

                var temeData = session.Execute("select * from \"Tema\" where \"korisnikID\" = '" + id + "' allow filtering;");

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
                return new List<Tema>();
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
                    "naslov='" + tema.naslov + "', sadrzaj='" + tema.sadrzaj + "',vidljivost='" + tema.vidljivost + "'" +
                    "  where \"korisnikID\" = '" + tema.korisnikID + "' and \"delatnostID\" = '" + tema.delatnostID + "' and" +
                    "\"temaID\" = '" + tema.temaID + "'");
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


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static bool InicijalizujTeme()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Delatnost delatnost = new Delatnost();

                if (session == null)
                    return false;

                session.Execute("insert into \"Tema\" (\"delatnostID\", \"korisnikID\", \"temaID\", datumKreiranja, sadrzaj, vidljivost, naslov)" +
                    " values ('6', '1', '1', '" + DateTime.Now.ToString() + "', 'Pre oko mesec dana sam obavešten da će grad kupiti" +
                    " parcelu do moje, a zbog proširenja ulice (ne bih navodi još uvek lokaciju). Bio sam u Urbanizmu, gde sam " +
                    "obavešten da će se mojoj parceli pridodati (tj da je u njihovim planovima već pridodat) deo parcele koju grad" +
                    " planira da kupi, i to 5 metara širine, pa dužinom cele moje parcele. Ovo bi značilo da će moj plac biti na uglu" +
                    " dve ulice. Moja ideja je spajanjem stare kuće i dela nove parcele napravim novi objekat. Veličina tog objekta bi" +
                    " bila oko 8*10m spoljne dimenzije. Koliko sam obavešten, dozvoljena je izgradnja prizemlja, prvog sprata i " +
                    "potkrovlja, na teritoriji gde ja živim. Plan mi je da u prizemlju napravim poslovni prostor (iz razloga što će" +
                    " se on nalaziti na uglu dve ulice), dok bi na prvom spratu i potkrovlju napravio 4 ili 2 stana (mada bih ja više" +
                    " voleo 4, al otom potom).', 'svi', 'Izgradnja male zgrade')");

                session.Execute("insert into \"Tema\" (\"delatnostID\", \"korisnikID\", \"temaID\", datumKreiranja, sadrzaj, vidljivost, naslov)" +
                    " values ('7', '2', '2', '" + DateTime.Now.ToString() + "', 'Potreban mi je ozbiljan savet arhitekte koji se " +
                    "bavi izradom projekata (idejnih i glavnih) za legalizaciju tj. za dobijanje gradjevinske dozvole!Jedan gavni " +
                    "projekat kuce sam predao Sluzbi zaduzenoj za legalizaciju pre godinu dana. Oni su ga tek od nedavno uzeli u " +
                    "razmatranje. U njemu stoji da je to kuca sa prizemljem i potkrovljem. Medjutim, u medjuvremenu sam izgrdio sprat" +
                    " (umesto potkrovlja), a potkrovlja nece ni biti (samo tavan). Sada mi moj arhitakta trazi 500 evra za novi idejni" +
                    " i 2000 evra za novi glavni projekat. Da li postoji mogucnost da samo izmenim vec postojeci glavni projejat?" +
                    "', 'svi', 'Pomoć oko projekta za realizaciju')");


                DodajOdgovor(new Odgovor { 
                    KorisnikID = "2",
                    TemaID = "1",
                    OdgovorID = "1",
                    Sadrzaj = "Proveri koliko brojila smes da imas a samim tin i stanova. Tu nije više porodično stanovanje. "+
                              "500eur po kvm izgradnja. Pitanje je sta želiš da postignes, pa to odlučuje o isplativosti, "+
                              "40 % a mozda i manje je izgrađenost parcele.Videti lokacijske uslove.",
                    Datum = DateTime.Now.ToString(),
                    Glasovi = 0

                });

                DodajKomentarOdgovoru("1", "2", "1", "1", "Idealno bi bilo kada bi se ovaj objekat napravio, prodalo šta mora da se proda da" +
                    " bi se pokrili troškovi izgradnje, a ovo što ostane da se rentira. Što se tiče izgrađenosti, pronašao sam inforamciju " +
                    "je 50 % dozvoljeno, možda je tačno možda i ne, ne garantujem.");

                DodajKomentarOdgovoru("1", "2", "1", "2", "Onda bi bilo dobro da proveriš to.");

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
                    " values ('" + tema.delatnostID + "', '" + tema.korisnikID + "', '" + tema.temaID + "', '" + tema.datumKreiranja + "', '" + tema.sadrzaj + "', '" + tema.vidljivost + "', '" + tema.naslov + "')");

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

                RowSet korisnikData = session.Execute("delete from \"Tema\" where \"delatnostID\" = '" + delatnostID + "' and " +
                    "\"korisnikID\" = '" + korisnikID + "' and \"temaID\" = '" + temaID + "'");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion


        #region Odgovor

        public static List<Odgovor> VratiSveOdgovoreTeme(string temaID)
        {
            try
            {
                List<Odgovor> lista = new List<Odgovor>();

                ISession session = SessionManager.GetSession();

                if (session == null)
                    return new List<Odgovor>();

                var odgovorData = session.Execute("select * from \"Odgovor\" where \"temaID\" = '" + temaID + "' allow filtering;");

                foreach (var data in odgovorData)
                {
                    Odgovor odg = new Odgovor();
                    OdradiDodeleOdgovora(odg, data);
                    lista.Add(odg);
                }

                return lista;
            }
            catch (Exception e)
            {
                return new List<Odgovor>();
            }
        }

        private static void OdradiDodeleOdgovora(Odgovor odgovor, Row odgovorData)
        {
            try
            {
                if (odgovorData["temaID"] != null)
                    odgovor.TemaID = odgovorData["temaID"].ToString();
                else
                    odgovor.TemaID = "";
                if (odgovorData["korisnikID"] != null)
                    odgovor.KorisnikID = odgovorData["korisnikID"].ToString();
                else
                    odgovor.KorisnikID = "";
                if (odgovorData["odgovorID"] != null)
                    odgovor.OdgovorID = odgovorData["odgovorID"].ToString();
                else
                    odgovor.OdgovorID = "";
                if (odgovorData["datumkreiranja"] != null)
                    odgovor.Datum = odgovorData["datumkreiranja"].ToString();
                else
                    odgovor.Datum = "";
                if (odgovorData["sadrzaj"] != null)
                    odgovor.Sadrzaj = odgovorData["sadrzaj"].ToString();
                else
                    odgovor.Sadrzaj = "";
                if (odgovorData["glasovi"] != null)
                    odgovor.Glasovi = Int32.Parse(odgovorData["glasovi"].ToString());
                else
                    odgovor.Glasovi = 0;

                if (odgovorData["komentari"] != null)
                    odgovor.komentari = (SortedDictionary<string, IEnumerable<string>>)odgovorData["komentari"];

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Odgovor VratiOdgovor(string odgovorID, string temaID, string korisnikID)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                Odgovor odgovor = new Odgovor();

                if (session == null)
                    return null;

                var temaData = session.Execute("select * from \"Odgovor\" where \"odgovorID\" = '" + odgovorID + "' and \"korisnikID\" = '" + korisnikID + "' and \"temaID\" = '" + temaID + "'").FirstOrDefault();

                if (temaData != null)
                {
                    OdradiDodeleOdgovora(odgovor, temaData);
                }

                return odgovor;
            }
            catch (Exception e)
            {
                return new Odgovor();
            }
        }

        public static bool ObrisiOdgovor(string odgovorID,string korisnikID,string temaID)
        {

            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                session.Execute("delete from \"Odgovor\" where \"odgovorID\" = '"+ odgovorID + "' and \"korisnikID\" = '" + korisnikID + "' and \"temaID\" = '" + temaID + "'");

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public static bool DodajOdgovor(Odgovor o)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;
                var odgovorData = session.Execute("insert into \"Odgovor\" (\"temaID\", \"korisnikID\", \"odgovorID\", datumkreiranja, glasovi, sadrzaj)" +
                    " values('" + o.TemaID + "','" + o.KorisnikID + "','" + o.OdgovorID + "','" + o.Datum + "'," + o.Glasovi + ",'" + o.Sadrzaj + "')");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool PromeniGlas(string odgovorID, string korisnikID, string temaID, int glasovi)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;
                var odgovorData = session.Execute("update \"Odgovor\" set glasovi = " + glasovi +
                    " where \"odgovorID\" = '" + odgovorID + "' AND \"korisnikID\" = '" + korisnikID + "' AND \"temaID\" = '" + temaID + "'");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool ObrisiKomentareSaOdgovora(string odgovorID, string korisnikID, string temaID, string korisnikCijJeKom)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                Odgovor odgovor = VratiOdgovor(odgovorID, temaID, korisnikID);

                string upit = "update \"Odgovor\" set komentari = ";

                if (odgovor.komentari != null)
                {
                    if (odgovor.komentari.Count > 1)
                    {
                        upit += "{";
                        foreach (KeyValuePair<string, IEnumerable<string>> kv in odgovor.komentari)
                        {
                            if (!kv.Key.Equals(korisnikCijJeKom))
                            {
                                upit += "'" + kv.Key + "':['";

                                foreach (string s in kv.Value)
                                {
                                    upit += s + "', '";
                                }

                                upit = upit.Substring(0, upit.Length - 3);
                                upit += "], ";
                            }
                            else
                            {
                                continue;
                            }
                        }

                        upit = upit.Substring(0, upit.Length - 2);

                        upit += "}";
                    }
                    else
                    {
                        upit += "null";
                    }

                    upit += " where \"odgovorID\" = '" + odgovorID + "' and \"korisnikID\" = '"
                            + korisnikID + "' and \"temaID\" = '" + temaID + "';";
                    session.Execute(upit);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool DodajKomentarOdgovoru(string odgovorID, string korisnikID, string temaID, string korisnikKojiKomentarise, string komentar)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                // pribavvi korisnika
                //
                Odgovor odgovor = VratiOdgovor(odgovorID, temaID, korisnikID);

                string upit = "update \"Odgovor\" set komentari = {";

                bool korisnikovPrviKomentar = true;

                // proveravamo da li postoji neki komentar
                if (odgovor.komentari != null)
                {
                    // za svakog korisnika uzimamo listu komentara
                    foreach (KeyValuePair<string, IEnumerable<string>> kv in odgovor.komentari)
                    {
                        // u upit dodajemo korisnikov id i pocetak liste komentara
                        upit += "'" + kv.Key + "':['";

                        bool dodat = false;
                        // za svaki komentar iz liste kreira upit sa starim komentarima
                        foreach (string s in kv.Value)
                        {
                            upit += s;
                            upit += "', '";
                        }
                        // ukoliko je to korisnik koji dodaje dodaj mu novi komentar
                        if (kv.Key.Equals(korisnikKojiKomentarise))
                        {
                            korisnikovPrviKomentar = false;
                            upit += komentar;
                        }
                        // inace ocisti upit
                        else
                        {
                            upit = upit.Substring(0, upit.Length - 4);
                        }
                        // kompletiraj koemtare za tog korisnika
                        upit += "'], ";
                    }
                    // ukoliko korisnik nema komentare
                    if (korisnikovPrviKomentar)
                    {
                        upit += "'" + korisnikKojiKomentarise + "':['" + komentar;
                    }
                    // inace ocisti upit
                    else
                    {
                        upit = upit.Substring(0, upit.Length - 4);
                    }
                }
                // ukoliko odgovor nema komentare
                else
                {
                    upit += " {'" + korisnikKojiKomentarise + "':['" + komentar;
                }
                // kompletiraj upit
                upit += "']} where \"odgovorID\" = '" + odgovorID + "' and \"korisnikID\" = '"
                        + korisnikID + "' and \"temaID\" = '" + temaID + "';";
                session.Execute(upit);

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



