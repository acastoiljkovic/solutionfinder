using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Tema
    {
        public string delatnostID { get; set; }
        public string korisnikID { get; set; }
        public string temaID { get; set; }
        public string sadrzaj { get; set; }
        public string vidljivost { get; set; }
        public string datumKreiranja { get; set; }

        public string naslov { get; set; }

        public SortedDictionary<string, IEnumerable<string>> komentari { get; set; }
        public List<string> tagovi { get; set; }

        public Tema()
        {
            komentari = new SortedDictionary<string, IEnumerable<string>>();
            tagovi = new List<string>();
        }
    }
}
