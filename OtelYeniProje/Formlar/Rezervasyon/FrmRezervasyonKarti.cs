using DevExpress.XtraEditors;
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

namespace OtelYeniProje.Formlar.Rezervasyon
{
    public partial class FrmRezervasyonKarti : Form
    {
        public FrmRezervasyonKarti()
        {
            InitializeComponent();
        }
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        public int id;
        Repository<TblRezervasyon> repo = new Repository<TblRezervasyon>();
        TblRezervasyon t = new TblRezervasyon();
        private void FrmRezervasyonKarti_Load(object sender, EventArgs e)
        {
            lookUpEditMisafir.Properties.DataSource = (from x in db.TblMisafir select new { x.MisafirID, x.AdSoyad }).ToList();
            lookUpEditKisi2.Properties.DataSource = (from x in db.TblMisafir select new { x.MisafirID, x.AdSoyad }).ToList();
            lookUpEditKisi3.Properties.DataSource = (from x in db.TblMisafir select new { x.MisafirID, x.AdSoyad }).ToList();
            lookUpEditKisi4.Properties.DataSource = (from x in db.TblMisafir select new { x.MisafirID, x.AdSoyad }).ToList();

            lookUpEditOda.Properties.DataSource = (from x in db.TblOda select new { x.OdaID, x.OdaNo , x.TblDurum.DurumAd }).Where(y => y.DurumAd == "Aktif").ToList();


            lookUpEditDurum.Properties.DataSource = (from x in db.TblDurum select new { x.DurumID, x.DurumAd }).ToList();

            if (id != 0)
            {
                var rezervasyon = repo.Find(x => x.RezervasyonID == id);
                lookUpEditMisafir.EditValue = rezervasyon.Misafir;
                dateEditGiris.Text = rezervasyon.Giriş.ToString();
                dateEditCikis.Text = rezervasyon.CikisTarihi.ToString();
                numericUpDown1.Value = decimal.Parse(rezervasyon.Kişi.ToString());
                lookUpEditOda.EditValue = rezervasyon.Oda;
                TxtTelefon.Text = rezervasyon.Telefon;
                lookUpEditDurum.EditValue = rezervasyon.Durum;
                TxtAcıklama.Text = rezervasyon.Aciklama.ToString();
                TxtToplam.Text = rezervasyon.Toplam.ToString();
                lookUpEditKisi2.EditValue = rezervasyon.Kisi1;
                lookUpEditKisi3.EditValue = rezervasyon.Kisi2;
                lookUpEditKisi4.EditValue = rezervasyon.Kisi3;
                TxtOdaNo.Text = rezervasyon.TblOda.OdaNo;

            }


        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblRezervasyon t = new TblRezervasyon();
            if (numericUpDown1.Value == 1)
            { t.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString()); }
            if (numericUpDown1.Value == 2)
            {
                t.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                t.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString()); }
            if (numericUpDown1.Value == 3)
            {
                t.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                t.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString());
                t.Kisi2 = int.Parse(lookUpEditKisi3.EditValue.ToString()); }
            if (numericUpDown1.Value == 4)
            {
                t.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                t.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString());
                t.Kisi2 = int.Parse(lookUpEditKisi3.EditValue.ToString());
                t.Kisi3 = int.Parse(lookUpEditKisi4.EditValue.ToString()); }

            
            
            t.Giriş = DateTime.Parse(dateEditGiris.Text);
            t.CikisTarihi = DateTime.Parse(dateEditCikis.Text);
            t.Kişi = numericUpDown1.Value.ToString();
            t.Oda = int.Parse(lookUpEditOda.EditValue.ToString());
           // t.RezervasyonAdSoyad = TxtRezervasyonAdSoyad.Text;
            t.Aciklama = TxtAcıklama.Text;
            t.Durum = int.Parse(lookUpEditDurum.EditValue.ToString());
            t.Toplam = decimal.Parse(TxtToplam.Text);
            repo.TAdd(t); ;
            XtraMessageBox.Show("Rezervasyon başarılı bir şekilde oluşturuldu.");


        }

        private void lookUpEditMisafir_EditValueChanged(object sender, EventArgs e)
        {
            int secilen;
            secilen = int.Parse(lookUpEditMisafir.EditValue.ToString());
            var telefon = db.TblMisafir.Where(x => x.MisafirID == secilen).Select(y => y.Telefon).FirstOrDefault();
            TxtTelefon.Text = telefon.ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            var rezervasyon = repo.Find(x => x.RezervasyonID == id);
            Repository<TblOda> repo2 = new Repository<TblOda>();
            if (lookUpEditDurum.Text == "Çıkış Yapıldı") 
            {
                var odadurum = repo2.Find(x => x.OdaID == rezervasyon.Oda);
                odadurum.Durum = 3;
                repo2.TUpdate(odadurum);
                rezervasyon.KasayaAktar = true;
                //Kasaya aktarma işlemi
                TblKasaHareketi tkasa = new TblKasaHareketi();
                Repository<TblKasaHareketi> repokasa = new Repository<TblKasaHareketi>();
                tkasa.Misafir = lookUpEditMisafir.Text;
                tkasa.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                tkasa.Tutar = decimal.Parse(TxtToplam.Text);
                repokasa.TAdd(tkasa);
            }
            
            rezervasyon.Giriş = DateTime.Parse(dateEditGiris.Text);
            rezervasyon.CikisTarihi= DateTime.Parse(dateEditCikis.Text);
            rezervasyon.Kişi = numericUpDown1.Value.ToString();
            rezervasyon.Oda = int.Parse(lookUpEditOda.EditValue.ToString());
            rezervasyon.Telefon = TxtTelefon.Text;
            rezervasyon.Durum = int.Parse(lookUpEditDurum.EditValue.ToString());
            rezervasyon.Toplam = decimal.Parse(TxtToplam.Text);
            rezervasyon.Aciklama = TxtAcıklama.Text;

            if (numericUpDown1.Value == 1)
            { rezervasyon.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString()); }
            if (numericUpDown1.Value == 2)
            {
                rezervasyon.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                rezervasyon.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString());
            }
            if (numericUpDown1.Value == 3)
            {
                rezervasyon.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                rezervasyon.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString());
                rezervasyon.Kisi2 = int.Parse(lookUpEditKisi3.EditValue.ToString());
            }
            if (numericUpDown1.Value == 4)
            {
                rezervasyon.Misafir = int.Parse(lookUpEditMisafir.EditValue.ToString());
                rezervasyon.Kisi1 = int.Parse(lookUpEditKisi2.EditValue.ToString());
                rezervasyon.Kisi2 = int.Parse(lookUpEditKisi3.EditValue.ToString());
                rezervasyon.Kisi3 = int.Parse(lookUpEditKisi4.EditValue.ToString());
            }
            repo.TUpdate(rezervasyon);
            XtraMessageBox.Show("Rezervasyon başarılı bir şekilde güncellendi.");
            
        }
    }
}
