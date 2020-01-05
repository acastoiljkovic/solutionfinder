using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer
{
    public class DataStore
    {
        private static DataStore _instance;
        private static CassandraDataLayer.QueryEntities.Korisnik _user;

        private DataStore()
        {
            _user = null;
        }

        public static DataStore GetInstance()
        {
            if (_instance == null)
                _instance = new DataStore();
            return _instance;
        }
        public CassandraDataLayer.QueryEntities.Korisnik GetKorisnik()
        {
            return _user;
        }
        public bool SetKorisnik(CassandraDataLayer.QueryEntities.Korisnik k)
        {
            try
            {
                _user = k;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
