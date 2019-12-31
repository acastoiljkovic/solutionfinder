using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Delatnost
    {
        public string delatnostID { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public string tip { get; set; }
    }
}
