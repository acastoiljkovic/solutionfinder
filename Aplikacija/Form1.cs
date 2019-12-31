using CassandraDataLayer;
using CassandraDataLayer.QueryEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CassandraWinFormsSample
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDodajdelatnost_Click(object sender, EventArgs e)
        {
            if (DataProvider.DodajDelatnost(new Delatnost{
                delatnostID = "2",
                naziv = "nazivDelatnosti",
                opis = "opisDelatnosti",
                tip = "tip"
            }))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
        }

        private void btnVratiDelatnost_Click(object sender, EventArgs e)
        {
            Delatnost delatnost = DataProvider.VratiDelatnost("1");
            if (delatnost != null)
                MessageBox.Show("Naziv delatnosti: " + delatnost.naziv);
            else
                MessageBox.Show("Greska!");
        }

        private void btnVratiDelatnosti_Click(object sender, EventArgs e)
        {
            List<Delatnost> delatnosti = new List<Delatnost>();
            delatnosti = DataProvider.VratiSveDelatnosti();
            foreach(Delatnost d in delatnosti)
            {
                MessageBox.Show("Naziv delatnosti:"+d.naziv);
            }
        }

        private void btnObrisiDelatnost_Click(object sender, EventArgs e)
        {
            if (DataProvider.ObrisiDelatnost("1"))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
        }

        private void btnDodajKorisnika_Click(object sender, EventArgs e)
        {
            if (DataProvider.DodajKorisnika(new Korisnik
            {
                korisnikID = "2",
                ime = "ime1",
                prezime = "prezime1",
                telefon = "323232",
                vebsajt = "vebsajt1",
                email = "email1"
            }))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
            
        }

        private void btnVratiKorisnika_Click(object sender, EventArgs e)
        {
            Korisnik k = DataProvider.VratiKorisnika("1");
            if (k != null)
                MessageBox.Show("Ime korisnika: " + k.ime);
            else
                MessageBox.Show("Neuspesno!");

        }

        private void btnVratiKorisnike_Click(object sender, EventArgs e)
        {
            List<Korisnik> korisnici = DataProvider.VratiSveKorisnike();
            if (korisnici != null)
            {
                foreach (Korisnik k in korisnici)
                    MessageBox.Show("Ime korisnika: " + k.ime);
            }
        }

        private void btnObrisiKorisnika_Click(object sender, EventArgs e)
        {
            if (DataProvider.ObrisiKorisnika("2"))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
        }

        private void btnDodajTemu_Click(object sender, EventArgs e)
        {
            if (DataProvider.DodajTemu(new Tema
            {
                delatnostID = "1",
                korisnikID = "2",
                temaID = "1",
                datumKreiranja = DateTime.Now.ToString(),
                sadrzaj = "sadrzaj teme",
                vidljivost = "javno"
            }))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
        }

        private void btnVratiTemu_Click(object sender, EventArgs e)
        {
            Tema t = DataProvider.VratiTemu("1", "2", "1");
            if (t != null)
                MessageBox.Show("Sadrzaj teme:"+t.sadrzaj);
            else
                MessageBox.Show("Neuspesno!");
        }

        private void btnVratiTeme_Click(object sender, EventArgs e)
        {
            List<Tema> teme = DataProvider.VratiSveTeme();
            if(teme != null)
            {
                foreach (Tema t in teme)
                    MessageBox.Show("Sadrzaj teme: " + t.sadrzaj);
            }
        }

        private void btnObrisiTemu_Click(object sender, EventArgs e)
        {
            if (DataProvider.ObrisiTemu("1", "2", "1"))
                MessageBox.Show("Uspesno!");
            else
                MessageBox.Show("Neuspesno!");
        }

    }
}
