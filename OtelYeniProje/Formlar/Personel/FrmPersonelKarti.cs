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
using static DevExpress.Utils.HashCodeHelper.Blob;

namespace OtelYeniProje.Formlar.Personel
{
    public partial class FrmPersonelKarti : Form
    {
        public FrmPersonelKarti()
        {
            InitializeComponent();
        }
        DbOtelYeniEntities db = new DbOtelYeniEntities();

        public int id;
        Repository<TblPersonel> repo = new Repository<TblPersonel>();
        private void FrmPersonelKarti_Load(object sender, EventArgs e)
        {
            //this.Text = id.ToString();
            if (id != 0) 
            {
                var personel = repo.Find(x => x.PersonelID == id);
                TxtAdSoyad.Text = personel.AdSoyad;
                TxtTc.Text = personel.TC;
                TxtAdres.Text = personel.Adres;
                TxtTelefon.Text = personel.Telefon;
                TxtMail.Text = personel.Mail;
                dateEditGiris.Text = personel.İseGirisTarihi.ToString();
                dateEditCikis.Text = personel.İstenCıkısTarihi.ToString();             
                TxtAcıklama.Text = personel.Acıklama;
                TxtSifre.Text = personel.Sifre;
                PictureEditKimlikOn.Image = Image.FromFile(personel.KimlikOn);
                PictureEditKimlikArka.Image = Image.FromFile(personel.KimlikArka);
                labelControl15.Text = personel.KimlikOn;
                labelControl16.Text = personel.KimlikArka;
                lookUpEditDepartman.EditValue = personel.Departman;
                lookUpEditGorev.EditValue = personel.Gorev;

            }
            lookUpEditDepartman.Properties.DataSource = (from x in db.TblDepartman select new { x.DepartmanID, x.DepartmanAd }).ToList();
            lookUpEditGorev.Properties.DataSource = (from x in db.TblGorev select new { x.GorevID, x.GorevAd }).ToList();

        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            
            TblPersonel t = new TblPersonel();
            t.AdSoyad = TxtAdSoyad.Text;
            t.TC = TxtTc.Text;
            t.Adres = TxtAdres.Text;
            t.Telefon = TxtTelefon.Text;
            t.İseGirisTarihi = DateTime.Parse(dateEditGiris.Text);
            t.Departman = int.Parse(lookUpEditDepartman.EditValue.ToString());
            t.Gorev =int.Parse( lookUpEditGorev.EditValue.ToString());
            t.Acıklama = TxtAcıklama.Text;
            t.Mail = TxtMail.Text;
            t.KimlikOn = PictureEditKimlikOn.GetLoadedImageLocation();
            t.KimlikArka = PictureEditKimlikArka.GetLoadedImageLocation();
            t.Sifre = TxtSifre.Text;
            t.Durum = 1;
            repo.TAdd(t);
            XtraMessageBox.Show("Personel başarılı bir şekilde sisteme kaydedildi.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            var deger = repo.Find(x => x.PersonelID == id);
            deger.AdSoyad = TxtAdSoyad.Text;
            deger.TC = TxtTc.Text;
            deger.Adres = TxtAdres.Text;
            deger.Telefon = TxtTelefon.Text;
            deger.İseGirisTarihi = DateTime.Parse(dateEditGiris.Text);
            deger.Departman = int.Parse(lookUpEditDepartman.EditValue.ToString());
            deger.Gorev = int.Parse(lookUpEditGorev.EditValue.ToString());
            deger.Acıklama = TxtAcıklama.Text;
            deger.Mail = TxtMail.Text;
            deger.KimlikOn = labelControl15.Text;
            deger.KimlikArka = labelControl16.Text;
           deger.Sifre = TxtSifre.Text;
            repo.TUpdate(deger);
            XtraMessageBox.Show("Personel kartı bilgileri başarıyla güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void PictureEditKimlikOn_EditValueChanged(object sender, EventArgs e)
        {
            labelControl15.Text = PictureEditKimlikOn.GetLoadedImageLocation().ToString();
        }

        private void PictureEditKimlikArka_EditValueChanged(object sender, EventArgs e)
        {
           labelControl16.Text=PictureEditKimlikArka.GetLoadedImageLocation().ToString();
        }
    }
}
