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

namespace OtelYeniProje.Formlar.Admin
{
    public partial class FrmSifreİslemleri : Form
    {
        public FrmSifreİslemleri()
        {
            InitializeComponent();
        }
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        public int id;
        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Repository<TblAdmin> repo = new Repository<TblAdmin>();
        private void FrmSifreİslemleri_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {
                var admin = repo.Find(x => x.ID == id);
                TxtKullanici.Text = admin.Kullanici;
                TxtMevcutSifre.Text = admin.Sifre;
                TxtRol.Text = admin.Rol;
            }   }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtYeniSifre1.Text == TxtYeniSifre2.Text)
            {
                TblAdmin t = new TblAdmin();
                t.Kullanici = TxtKullanici.Text;
                t.Sifre = TxtYeniSifre1.Text;
                db.TblAdmin.Add(t);
                db.SaveChanges();
                XtraMessageBox.Show("Yeni Kullanıcı başarılı bir şekilde oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Lütfen tekrar deneyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            var deger = repo.Find(x => x.ID == id);
            if (TxtYeniSifre1.Text == TxtYeniSifre2.Text)
            {
                deger.Kullanici = TxtKullanici.Text;
                deger.Sifre = TxtYeniSifre1.Text;
                deger.Rol = TxtRol.Text;
                repo.TUpdate(deger);
                XtraMessageBox.Show("Admin kartı bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                XtraMessageBox.Show("Lütfen Şifrelerin eşleştiğinden emin olun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            FrmAdminListesi fr = new FrmAdminListesi();
            fr.Show();
            this.Hide();

        }
    }
}
