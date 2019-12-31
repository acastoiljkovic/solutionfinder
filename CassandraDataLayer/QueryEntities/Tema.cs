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

        public string indikator { get; set; }

        List<string> komentari { get; set; }
        List<string> tagovi { get; set; }
    }
}
