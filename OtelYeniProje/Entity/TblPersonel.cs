//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OtelYeniProje.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblPersonel
    {
        public int PersonelID { get; set; }
        public string AdSoyad { get; set; }
        public string TC { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public Nullable<System.DateTime> İseGirisTarihi { get; set; }
        public Nullable<System.DateTime> İstenCıkısTarihi { get; set; }
        public Nullable<int> Departman { get; set; }
        public Nullable<int> Gorev { get; set; }
        public string Acıklama { get; set; }
        public Nullable<int> Durum { get; set; }
        public string KimlikOn { get; set; }
        public string KimlikArka { get; set; }
        public string Sifre { get; set; }
        public string Yetki { get; set; }
    
        public virtual TblDepartman TblDepartman { get; set; }
        public virtual TblDurum TblDurum { get; set; }
        public virtual TblGorev TblGorev { get; set; }
    }
}
