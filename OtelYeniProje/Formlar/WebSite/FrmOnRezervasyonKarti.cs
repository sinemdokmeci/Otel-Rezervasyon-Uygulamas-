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
    public partial class FrmOnRezervasyonKarti : Form
    {
        public FrmOnRezervasyonKarti()
        {
            InitializeComponent();
        }
        public int id;
        DbOtelYeniEntities db = new DbOtelYeniEntities();
        Repository<TblOnRezervasyon> repo = new Repository<TblOnRezervasyon>();
        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmOnRezervasyonKarti_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {
                var rezervasyon = repo.Find(x => x.ID == id);
                
                dateEditGiris.Text = rezervasyon.Giriş.ToString();
                dateEditCikis.Text = rezervasyon.CikisTarihi.ToString();
                dateEditTarih.Text = rezervasyon.Tarih.ToString();
                numericUpDown1.Value = decimal.Parse(rezervasyon.Kişi.ToString());
                TxtTelefon.Text = rezervasyon.Telefon;
                TxtAdSoyad.Text = rezervasyon.AdSoyad;
                TxtMail.Text = rezervasyon.Mail;
                TxtAcıklama.Text = rezervasyon.Aciklama.ToString();
                

            }
        }
    }
}
