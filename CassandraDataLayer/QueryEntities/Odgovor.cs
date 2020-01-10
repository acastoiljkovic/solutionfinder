using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Odgovor
    {
        public String TemaID { get; set; }
        public String KorisnikID { get; set; }
        public String OdgovorID { get; set; }
        public String Datum { get; set; }
        public String Sadrzaj { get; set; }
        public int Glasovi { get; set; }
        public SortedDictionary<string, IEnumerable<string>> komentari { get; set; }

        public Odgovor()
        {
            komentari = new SortedDictionary<string, IEnumerable<string>>();
        }
    }
}
