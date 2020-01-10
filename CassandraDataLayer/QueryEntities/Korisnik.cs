using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Korisnik
    {
        public string korisnikID { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string vebsajt { get; set; }
        public string sifra { get; set; }
    }
}