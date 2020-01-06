using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Statistika
    {
        public string korisnikID { get; set; }
        public string delatnostID { get; set; }

        public List<Tema> mojeTeme { get; set; }
        
    }
}
