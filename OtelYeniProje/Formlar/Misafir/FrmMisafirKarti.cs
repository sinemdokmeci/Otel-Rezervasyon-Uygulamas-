using DevExpress.Data.ODataLinq.Helpers;
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

namespace OtelYeniProje.Formlar.Misafir
{
    public partial class FrmMisafirKarti : Form
    {
        public FrmMisafirKarti()
        {
            InitializeComponent();
        }
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        Repository<TblMisafir> repo = new Repository<TblMisafir>();
        TblMisafir t = new TblMisafir();
        public int id;
        string resim1, resim2;
        private void FrmMisafirKarti_Load(object sender, EventArgs e)
        {
            lookUpEditUlke.Properties.DataSource = (from x in db.TblUlke select new { x.UlkeID, x.UlkeAd }).ToList();
            //şehir listesi
            lookUpEditSehir.Properties.DataSource = (from x in db.iller select new { Id = x.id, Şehir = x.sehir }).ToList();


            try
            {
                if (id != 0)
                {
                    var misafir = repo.Find(x => x.MisafirID == id);
                    TxtAdSoyad.Text = misafir.AdSoyad;
                    TxtTc.Text = misafir.TC;
                    TxtAdres.Text = misafir.Adres;
                    TxtTelefon.Text = misafir.Telefon;
                    TxtMail.Text = misafir.Mail;
                    TxtAcıklama.Text = misafir.Açiklama;
                    lookUpEditSehir.EditValue = misafir.sehir;
                    lookUpEditUlke.EditValue = misafir.Ulke;
                    lookUpEditİlce.EditValue = misafir.ilce;
                    PictureEditKimlikOn.Image = Image.FromFile(misafir.KimlikFoto1);
                    PictureEditKimlikArka.Image = Image.FromFile(misafir.KimlikFoto2);
                    resim1 = misafir.KimlikFoto1;
                    resim2 = misafir.KimlikFoto2;

                }
               
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir hata oluştu lütfen sütunları kontrol edin.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                
            }
            //Güncellenecek kart bilgileri

            //Ülke listesi
            

        }

        private void lookUpEditSehir_EditValueChanged(object sender, EventArgs e)
        {
            int secilen;
            secilen = int.Parse(lookUpEditSehir.EditValue.ToString());
            lookUpEditİlce.Properties.DataSource = 
                (from x in db.ilceler select new {Id = x.id, İlçe =x.ilce,Şehir = x.sehir })
                .Where(y => y.Şehir == secilen).ToList();
        }
        

        private void PictureEditKimlikOn_EditValueChanged(object sender, EventArgs e)
        {
            resim1 = PictureEditKimlikOn.GetLoadedImageLocation().ToString();
        }

        private void PictureEditKimlikArka_EditValueChanged(object sender, EventArgs e)
        {
            resim2 = PictureEditKimlikArka.GetLoadedImageLocation().ToString();
        }

        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            var deger = repo.Find(x => x.MisafirID == id);
            deger.AdSoyad = TxtAdSoyad.Text;
            deger.TC = TxtTc.Text;
            deger.Mail = TxtMail.Text;
            deger.Telefon = TxtTelefon.Text;
            deger.Adres = TxtAdres.Text;
            deger.Açiklama = TxtAcıklama.Text;
            deger.KimlikFoto1 = resim1;
            deger.KimlikFoto2 = resim2;
            deger.Ulke = int.Parse(lookUpEditUlke.EditValue.ToString());
            deger.sehir = int.Parse(lookUpEditSehir.EditValue.ToString());
            deger.ilce = int.Parse(lookUpEditİlce.EditValue.ToString());
            deger.Durum = 1;
            repo.TUpdate(deger);
            XtraMessageBox.Show("Misafir kartı bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            
            t.AdSoyad = TxtAdSoyad.Text;
            t.TC = TxtTc.Text;
            t.Telefon = TxtTelefon.Text;
            t.Mail = TxtMail.Text;
            t.Adres = TxtAdres.Text;
            t.Açiklama = TxtAcıklama.Text;
            t.Durum = 1;
            t.sehir = int.Parse(lookUpEditSehir.EditValue.ToString());
            t.ilce = int.Parse(lookUpEditİlce.EditValue.ToString());
            t.Ulke = int.Parse(lookUpEditUlke.EditValue.ToString());
            t.KimlikFoto1 = resim1;
            t.KimlikFoto2 = resim2;

            repo.TAdd(t);
            XtraMessageBox.Show("Misafir sisteme başarılı bir şekilde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
