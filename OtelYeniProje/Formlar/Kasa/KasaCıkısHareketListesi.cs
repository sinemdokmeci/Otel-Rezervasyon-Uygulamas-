﻿using OtelYeniProje.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelYeniProje.Formlar.Kasa
{
    public partial class KasaCıkısHareketListesi : Form
    {
        public KasaCıkısHareketListesi()
        {
            InitializeComponent();
        }
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        private void KasaCıkısHareketListesi_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblKasaCikisHareketi
                                       select new
                                       {
                                           x.Aciklama,
                                           x.Tarih,
                                           x.Tutar
                                       }).ToList();
        }
    }
}
