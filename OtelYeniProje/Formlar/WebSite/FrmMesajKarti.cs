﻿using DevExpress.XtraEditors;
using OtelYeniProje.Entity;
using OtelYeniProje.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelYeniProje.Formlar.WebSite
{
    public partial class FrmMesajKarti : Form
    {
        public FrmMesajKarti()
        {
            InitializeComponent();
        }
        public int id;
        public int id2;
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        Repository<TblMesaj2> repo = new Repository<TblMesaj2>();
        Repository<TblMesaj> repoiletisim = new Repository<TblMesaj>();
        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMesajKarti_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {
                var mesaj = repo.Find(x => x.MesajID == id); 
                TxtGonderenMail.Text = mesaj.Gonderen;
                TxtKonu.Text = mesaj.Konu;
                TxtMesaj.Text = mesaj.Mesaj;
                TxtTarih.Text = mesaj.Tarih.ToString();

                var kişi = db.TblYeniKayit.Where(x => x.Mail == mesaj.Gonderen).Select(y => y.AdSoyad).FirstOrDefault();
                if (kişi != null)
                {
                    TxtAdSoyad.Text = kişi.ToString();
                }
                else
                {
                    TxtAdSoyad.Text = "Admin";
                }
            }
            if (id2 != 0)
            {
                var mesaj = repoiletisim.Find(x => x.MesajID == id2);
                TxtGonderenMail.Text = mesaj.Gonderen;
                TxtKonu.Text = mesaj.Konu;
                TxtMesaj.Text = mesaj.Mesaj;
                TxtAdSoyad.Text = mesaj.Gonderen;

            }
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            TblMesaj2 t = new TblMesaj2();
            t.Gonderen = "Admin";
            t.Alici = TxtGonderenMail.Text;
            t.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            t.Konu = TxtKonu.Text;
            t.Mesaj = TxtMesaj.Text;
            repo.TAdd(t);
            XtraMessageBox.Show("Mesajınız başarılı bir şekilde iletildi.");
        }
    }
}
