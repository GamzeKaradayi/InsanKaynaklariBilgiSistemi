using System;
using System.Collections.Generic;

namespace WindowsFormsApplication3
{
    public class Calisan:ICloneable
    {
        public String Isim { get; set; }
        public String SirketAdres { get; set; }
        public String SirketTel { get; set; }
        public String SirketEposta { get; set; }
        public String Ulke { get; set; }
        public String DogumYeri { get; set; }
        public String DogumTarihi { get; set; }
        public String Hobiler { get; set; }
        public String Referanscisi { get; set; }
        public int Puan { get; set; }
        public LinkedList<Tecrube> deneyimleri = new LinkedList<Tecrube>();
        public LinkedList<Egitim> egitimleri = new LinkedList<Egitim>();
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
