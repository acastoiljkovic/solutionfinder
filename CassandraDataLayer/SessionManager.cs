using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CassandraDataLayer
{
    //Projekat ima 2 dela, jedna je ova formica. Druga je Cassandra data layer projekat
    //U tom projektu imamo jedan DataProvider i SessionManager. 
    //
    //U ovom projektu mi koristimo provajder za Cassandru kompanije DataStacks provider za c# i on nam je u referencama
    //
    //Nasi entiteti su nazvani QueryEntities! Vazno!
    //I kaze, e ovo sto snimas, to su zapravo rezultati pretrage. Cassandra je query... sistem.
    //
    //Nas DataProvider--idemo tamo--> je zaduzen za izvrsenje upita i on ima osnovne operacije za svaki od nasih entiteta.
    //Moze da pribavi hotele, da snimi novi. Da update-uje neki, da ga obrise. I to vazi za svaki od entiteta.
    //
    public static class SessionManager
    {
        public static ISession session;

        public static ISession GetSession()
        {
            if(session == null)
            {
                Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("Blogovi");
            }

            return session;
        }
    }
}

