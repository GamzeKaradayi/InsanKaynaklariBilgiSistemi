using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Globalization;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {

        List<Calisan> isciler = new List<Calisan>();
       
        HashMap ilanlar = new HashMap();
        int secilanIlan;
        Calisan guncellenecekCalisan;
        IsIlani guncellenecekIlan;
        Calisan IsciBilgi;
        LinkedList<Egitim> egitimIslem;
        LinkedList<Tecrube> deneyimIslem;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DosyaAc();
        }

        
        private void DosyaAc()
        {
             
            FileStream fs = new FileStream("eleman.txt", FileMode.Open);
            BufferedStream bs = new BufferedStream(fs);
            StreamReader sr = new StreamReader(bs);
            String temp;
            
            while ((temp = sr.ReadLine()) != null)
            {
                IsciBilgi = new Calisan()
                {
                    Isim = temp,
                    SirketAdres = sr.ReadLine(),
                    SirketTel = sr.ReadLine(),
                    SirketEposta = sr.ReadLine(),
                    Ulke = sr.ReadLine(),
                    DogumYeri = sr.ReadLine(),
                    DogumTarihi = sr.ReadLine(),
                    Hobiler = sr.ReadLine(),
                    Referanscisi = sr.ReadLine()
                };
                Egitim egitim = new Egitim()
                {
                    OkulIsim = sr.ReadLine(),
                    Bolumu = sr.ReadLine(),
                    BaslangicYili = sr.ReadLine(),
                    BitisYili = sr.ReadLine(),
                    NotOrtalamasi = sr.ReadLine()
                };
                IsciBilgi.egitimleri.AddLast(egitim);
                egitim = new Egitim()
                {
                    OkulIsim = sr.ReadLine(),
                    Bolumu = sr.ReadLine(),
                    BaslangicYili = sr.ReadLine(),
                    BitisYili = sr.ReadLine(),
                    NotOrtalamasi = sr.ReadLine()
                };
                IsciBilgi.egitimleri.AddLast(egitim);
                Tecrube deneyim = new Tecrube()
                {
                    SirketIsim = sr.ReadLine(),
                    SirketAdres = sr.ReadLine(),
                    Pozisyon = sr.ReadLine()
                };
                IsciBilgi.deneyimleri.AddLast(deneyim);
                isciler.Add(IsciBilgi);
            }
            fs.Close();
            

            fs = new FileStream("sirket.txt", FileMode.Open);
            bs = new BufferedStream(fs);
            sr = new StreamReader(bs);
            temp = null;

            while ((temp = sr.ReadLine()) != null)
            {
                IsIlani ilan = new IsIlani()
                {
                    SirketIsim = temp,
                    SirketAdres = sr.ReadLine(),
                    SirketTel = sr.ReadLine(),
                    SirketFaks = sr.ReadLine(),
                    SirketEposta = sr.ReadLine(),
                    ilanTanim = sr.ReadLine(),
                    kriterler = sr.ReadLine()
                };
                int key = GetKey(ilan.SirketIsim);
                ilanlar.AddIlan(key, ilan);
            }
            fs.Close();
                    }


        private void bIsciKariyerBilgisiEkle_Click(object sender, EventArgs e)
        {
            IsciBilgi = new Calisan()
            {
                Isim = tbIsciAd.Text,
                SirketAdres = tbIsciAdres.Text,
                SirketTel = tbIsciSirketTel.Text,
                SirketEposta = tbIsciSirketEposta.Text,
                Ulke = tbIsciUlke.Text,
                DogumYeri = tbIsciDogumYeri.Text,
                DogumTarihi = dtpIsciDogumTarih.Text,
                Hobiler = tbIsciHobiler.Text,
                Referanscisi = tbIsciReferans.Text
            };
            
            egitimIslem = new LinkedList<Egitim>();
            deneyimIslem = new LinkedList<Tecrube>();
            MessageBox.Show("Kariyer Bilgileri Eklendi egitim ve tecrübeden devam ediniz...");
      



        }

  
        private void bIsIlanEkle_Click(object sender, EventArgs e)
        {
            IsIlani ilan = new IsIlani()
            {
                SirketIsim = tbIsIlanAd.Text,
                SirketAdres = tbIsIlanAdres.Text,
                SirketTel = tbIsIlanSirketTel.Text,
                SirketFaks = tbIsIlanSirketFaks.Text,
                SirketEposta = tbIsIlanSirketEposta.Text,
                ilanTanim = tbIsIlanTanimi.Text,
                kriterler = tbIsIlankriterler.Text
            };
            int key = GetKey(ilan.SirketIsim);
            if (ilanlar.GetIlan(key) != null)    
            {
                MessageBox.Show("Bu isimde ilan vardır. Yeniden deneyin !");
            }
            else
            {
                ilanlar.AddIlan(key, ilan);
                MessageBox.Show("İlan Kaydedildi.");
                tbIsIlanAd.Text = "";
                tbIsIlanAdres.Text = "";
                tbIsIlanSirketTel.Text = "";
                tbIsIlanSirketFaks.Text = "";
                tbIsIlanSirketEposta.Text = "";
                tbIsIlankriterler.Text = "";
                tbIsIlanTanimi.Text = "";

            }


        }

      

        private void cbİlanlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsIlani temp = ilanlar.GetIlan(GetKey(cbİlanlar.Text));
            if (temp != null)
            {
                lIlanIsim.Text = temp.SirketIsim;
                lIlanilanTanim.Text = temp.ilanTanim;
                tbIlankriterler.Text = temp.kriterler;
                List<String> isciadaylar = new List<string>();
                foreach (Calisan item in isciler)
                {
                    if (item != null)
                        isciadaylar.Add(item.Isim);
                }
                if (isciadaylar.Count > 0)
                {
                    cbIlanIcınIscıler.Items.Clear();
                    cbIlanIcınIscıler.Items.AddRange(isciadaylar.ToArray());
                    bIsBasvurusuYap.Enabled = true;
                }
                else bIsBasvurusuYap.Enabled = false;

            }

        }

        private void cbİlanlar_DropDownClosed(object sender, EventArgs e)
        {

        }

      
   
        private void isIlanlariniLisSirketTele()
        {
            cbIsIlanlari.Items.Clear();
            List<String> liste = new List<string>();
            IsIlani ilan;
            foreach (var item in ilanlar.table)
            {
                if (item != null) liste.Add(((IsIlani)item.Deger).SirketIsim);

            }
            if (liste.Count > 0) cbIsIlanlari.Items.AddRange(liste.ToArray());
        }

        private void bIsIlanAra_Click(object sender, EventArgs e)
        {
            bool bulunduMu = false;
            String aranacak = tbIsIlanAra.Text;
            if (!aranacak.Equals(""))
            {
                guncellenecekIlan = ilanlar.GetIlan(GetKey(aranacak));
                if (guncellenecekIlan != null)
                {
                    bulunduMu = true;
                }
            }
            if (bulunduMu)
            {
                tbIsIlanAd.Text = guncellenecekIlan.SirketIsim;
                tbIsIlanAd.Enabled = false;
                tbIsIlanAdres.Text = guncellenecekIlan.SirketAdres;
                tbIsIlanSirketTel.Text = guncellenecekIlan.SirketTel;
                tbIsIlanSirketFaks.Text = guncellenecekIlan.SirketFaks;
                tbIsIlanSirketEposta.Text = guncellenecekIlan.SirketEposta;
                tbIsIlanTanimi.Text = guncellenecekIlan.ilanTanim;
                tbIsIlankriterler.Text = guncellenecekIlan.kriterler;

            }
            else
            {
                MessageBox.Show("Böyle bir ilan bulunamadı !");
                tbIsIlanAd.Enabled = true;
            }
        }

        private void bIsIlanDuzenle_Click(object sender, EventArgs e)
        {
            if (guncellenecekIlan != null)
            {
                guncellenecekIlan.SirketAdres = tbIsIlanAdres.Text;
                guncellenecekIlan.SirketTel = tbIsIlanSirketTel.Text;
                guncellenecekIlan.SirketFaks = tbIsIlanSirketFaks.Text;
                guncellenecekIlan.SirketEposta = tbIsIlanSirketEposta.Text;
                guncellenecekIlan.ilanTanim = tbIsIlanTanimi.Text;
                guncellenecekIlan.kriterler = tbIsIlankriterler.Text;
                guncellenecekIlan = null;
                tbTemizle();
                MessageBox.Show("Düzenleme kaydedildi !");
                tbIsIlanAd.Enabled = true;

            }
        }

        private void bIsIlanSil_Click(object sender, EventArgs e)
        {
            if (guncellenecekIlan != null)
            {
                ilanlar.RemoveIlan(secilanIlan);
                guncellenecekCalisan = null;
                secilanIlan = -1;
                tbTemizle();
                MessageBox.Show("Kayıt başarıyla silindi !");
                tbIsIlanAd.Enabled = true;

            }
        }

        private void cbIsIlanlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            String str = cbIsIlanlari.Text;
            tbIsIlanBasvuranlar.Text = "";
            bElamanSec.Enabled = false;
            if (!str.Equals(""))
            {
                IsIlani ilan = ilanlar.GetIlan(GetKey(str));
                if (ilan != null)
                {
                    secilanIlan = GetKey(str);
                    lIsIlanAd.Text = ilan.SirketIsim;
                    lIsIlanTanim.Text = ilan.ilanTanim;
                    bElamanSec.Enabled = true;
                    foreach (var item in ilan.adaylar.heapArray)
                    {
                        if (item != null)
                            tbIsIlanBasvuranlar.Text += item.IsciVerileri.Isim + " -Uygunluk " + item.Uygunluk.ToString("N2") + "\n";
                    }
                }
            }
        }

        private void cbIsIlanlari_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void bElamanSec_Click(object sender, EventArgs e)
        { 

            
            IsIlani secilenIlan = ilanlar.GetIlan(secilanIlan);
            if (secilenIlan != null)
            {
                StringBuilder stb = new StringBuilder();
                StringBuilder stb2 = new StringBuilder();

                StringBuilder stb3 = new StringBuilder();

                StringBuilder stb4 = new StringBuilder();

                HeapNode secilenEleman = secilenIlan.enUygunAdaySec();
                if (secilenEleman != null)
                {
                    stb.Append("Aşağıdaki Elaman İşe Alındı !\n");
                    stb.Append("Ad      : " + secilenEleman.IsciVerileri.Isim + "\n");
                    stb.Append("SirketTelefon : " + secilenEleman.IsciVerileri.SirketTel + "\n");
                    stb.Append("SirketEposta  : " + secilenEleman.IsciVerileri.SirketEposta + "\n");

                    Egitim egitim = secilenEleman.IsciVerileri.egitimleri.First.Value;
                    if (egitim != null)
                    {
                        stb2.Append("Egitim  Durumu\n");
                        stb2.Append("Okul Adı    : " + egitim.OkulIsim + "\n");
                        stb2.Append("Bolumu      : " + egitim.Bolumu + "\n");
                        stb2.Append("NotOrt      : " + egitim.NotOrtalamasi + "\n");
                    }

                    Tecrube deneyim = secilenEleman.IsciVerileri.deneyimleri.First.Value;
                    if (deneyim != null)
                    {
                        stb3.Append("Deneyim  Durumu\n");
                        stb3.Append("Isyeri Adı    : " + deneyim.SirketIsim + "\n");
                        stb3.Append("Pozisyonu     : " + deneyim.Pozisyon + "\n");
                    }
                    stb4.Append("\n\n");
                    stb4.Append("Bu işçi aşağıdaki işe alınmıştır !\n");
                    stb4.Append("İş Yeri Adı  : " + secilenIlan.SirketIsim + "\n");
                    stb4.Append("SirketEposta       : " + secilenIlan.SirketEposta + "\n");
                    stb4.Append("Adress       : " + secilenIlan.SirketAdres + "\n");
                    stb4.Append("\n\n\nİlgili ilan yayından kaldırılmıştır..");
                    ilanlar.RemoveIlan(secilanIlan);
                    MessageBox.Show(stb.ToString());
                    MessageBox.Show(stb2.ToString());
                    MessageBox.Show(stb3.ToString());
                    MessageBox.Show(stb4.ToString());
                }


            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isIlanlariniLisSirketTele();
        }
        private void tbTemizle()
        {
            tbIsciAd.Text = "";
            tbIsciAdres.Text = "";
            tbIsciSirketTel.Text = "";
            tbIsciSirketEposta.Text = "";
            tbIsciUlke.Text = "";
            tbIsciDogumYeri.Text = "";
            dtpIsciDogumTarih.Text = "";
            tbIsciHobiler.Text = "";
            tbIsciReferans.Text = "";
            tbIsIlanAd.Text = "";
            tbIsIlanAdres.Text = "";
            tbIsIlanSirketTel.Text = "";
            tbIsIlanSirketFaks.Text = "";
            tbIsIlanSirketEposta.Text = "";
            tbIsIlanTanimi.Text = "";
            tbIsIlankriterler.Text = "";
            tbEgitimAd.Text = "";
            tbEgitimBolum.Text = "";
            tbEgitimBaslangic.Text = "";
            tbEgitimBitis.Text = "";
            tbEgitimNotOrtalama.Text = "";
            tbTecrubeAd.Text = "";
            tbTecrubeAdres.Text = "";
            tbTecrubePozisyon.Text = "";
        }

      
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        
        private int GetKey(String input)
        {
            int key;
            using (System.Security.Cryptography.MD5 md5Algorithm = System.Security.Cryptography.MD5.Create())
            {
                key = (int)BigInteger.ModPow(new BigInteger(md5Algorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input))), 1, 300);
            }
            if (key < 0) key += 300;
            return key;
        }

        private void bEgitimEkle_Click_1(object sender, EventArgs e)
        {
            Egitim temp = new Egitim()
            {
                OkulIsim = tbEgitimAd.Text,
                Bolumu = tbEgitimBolum.Text,
                BaslangicYili = tbEgitimBaslangic.Text,
                BitisYili = tbEgitimBitis.Text,
                NotOrtalamasi = tbEgitimNotOrtalama.Text
            };
            egitimIslem.AddLast(temp);
            tbEgitimAd.Text = "";
            tbEgitimBolum.Text = "";
            tbEgitimBaslangic.Text = "";
            tbEgitimBitis.Text = "";
            tbEgitimNotOrtalama.Text = "";
            label40.Text = "Egitim Eklendi. Yenisi için \n Efitim eklemeye devam ediniz.";

        }

        private void bTecrubeEkle_Click_1(object sender, EventArgs e)
        {
            Tecrube temp = new Tecrube()
            {
                SirketIsim = tbTecrubeAd.Text,
                SirketAdres = tbTecrubeAdres.Text,
                Pozisyon = tbTecrubePozisyon.Text
            };
            deneyimIslem.AddLast(temp);
            tbTecrubeAd.Text = "";
            tbTecrubeAdres.Text = "";
            tbTecrubePozisyon.Text = "";
            label40.Text = "Tecrube Eklendi. Yenisi için \n Tecrube eklemeye devam ediniz.";


        }

        private void bIsciKaydet_Click_1(object sender, EventArgs e)
        {
            tbTemizle();
            IsciBilgi.egitimleri = egitimIslem;
            IsciBilgi.deneyimleri = deneyimIslem;
            isciler.Add(IsciBilgi);
            tbTemizle();
            tabControl1.SelectedIndex = 0;

        }

        private void bİsciAra_Click_1(object sender, EventArgs e)
        {
            bool bulunduMu = false;
            String aranacak = tbIsciAra.Text;
            if (!aranacak.Equals(""))
            {
                foreach (Calisan item in isciler)
                {
                    if (item.Isim == aranacak)
                    {
                        bulunduMu = true;
                        guncellenecekCalisan = item;
                        break;
                    }
                }
            }

            if (bulunduMu)
            {
                tbIsciAd.Text = guncellenecekCalisan.Isim;
                tbIsciAd.Enabled = false;
                tbIsciAdres.Text = guncellenecekCalisan.SirketAdres;
                tbIsciSirketTel.Text = guncellenecekCalisan.SirketTel;
                tbIsciSirketEposta.Text = guncellenecekCalisan.SirketEposta;
                tbIsciUlke.Text = guncellenecekCalisan.Ulke;
                tbIsciDogumYeri.Text = guncellenecekCalisan.DogumYeri;
                try
                {
                    dtpIsciDogumTarih.Value = DateTime.ParseExact(guncellenecekCalisan.DogumTarihi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {

                }
                tbIsciHobiler.Text = guncellenecekCalisan.Hobiler;
                tbIsciReferans.Text = guncellenecekCalisan.Referanscisi;
            }
            else
            {
                MessageBox.Show("Böyle bir işçi bulunamadı. !");
            }
        }

        private void bIlanAramayiIptalEt_Click(object sender, EventArgs e)
        {
            tbIsciAd.Enabled = true;
            tbTemizle();
        }

        private void bIsciAramaIptal_Click_1(object sender, EventArgs e)
        {
            tbIsIlanAd.Enabled = true;
            tbTemizle();
        }

        private void bIsciDuzenle_Click_1(object sender, EventArgs e)
        {
            if (guncellenecekCalisan != null)
            {
                guncellenecekCalisan.SirketAdres = tbIsciAdres.Text;
                guncellenecekCalisan.SirketTel = tbIsciSirketTel.Text;
                guncellenecekCalisan.SirketEposta = tbIsciSirketEposta.Text;
                guncellenecekCalisan.Ulke = tbIsciUlke.Text;
                guncellenecekCalisan.DogumYeri = tbIsciDogumYeri.Text;
                guncellenecekCalisan.DogumTarihi = dtpIsciDogumTarih.Text;
                guncellenecekCalisan.Hobiler = tbIsciHobiler.Text;
                guncellenecekCalisan.Referanscisi = tbIsciReferans.Text;
                guncellenecekCalisan = null;
                tbTemizle();
                MessageBox.Show("Başarıyla Guncellendi!");
                tbIsciAd.Enabled = true;
            }
        }

        private void bIsciSil_Click_1(object sender, EventArgs e)
        {
            if (guncellenecekCalisan != null)
            {
                isciler.Remove(guncellenecekCalisan);
                guncellenecekCalisan = null;
                tbTemizle();
                MessageBox.Show("Kayıt başarıyla silindi !");
                tbIsciAd.Enabled = true;
            }
        }

        private void bIlanAra_Click_1(object sender, EventArgs e)
        {
            bIsBasvurusuYap.Enabled = false;
            String aranacak = tbIlanAra.Text;
            List<String> bulunanlar = new List<string>();
            cbİlanlar.Items.Clear();
            foreach (var item in ilanlar.table)
            {
                if (item != null && ((IsIlani)item.Deger).SirketIsim.IndexOf(aranacak) >= 0)
                {
                    bulunanlar.Add(((IsIlani)item.Deger).SirketIsim);
                }
            }
            if (bulunanlar.Count > 0)
            {
                cbİlanlar.Items.AddRange(bulunanlar.ToArray());
                MessageBox.Show("İş ilanı bulundu !");
            }
            else
            {
                MessageBox.Show("İş ilanı bulunamadı !");
            }
        }

     

        private void cbİlanlar_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            IsIlani temp = ilanlar.GetIlan(GetKey(cbİlanlar.Text));
            if (temp != null)
            {
                lIlanIsim.Text = temp.SirketIsim;
                lIlanilanTanim.Text = temp.ilanTanim;
                tbIlankriterler.Text = temp.kriterler;
                List<String> isciadaylar = new List<string>();
                foreach (Calisan item in isciler)
                {
                    if (item != null)
                        isciadaylar.Add(item.Isim);
                }
                if (isciadaylar.Count > 0)
                {
                    cbIlanIcınIscıler.Items.Clear();
                    cbIlanIcınIscıler.Items.AddRange(isciadaylar.ToArray());
                    bIsBasvurusuYap.Enabled = true;
                }
                else bIsBasvurusuYap.Enabled = false;

            }
        }

        private void cbIlanIcınIscıler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bIsBasvurusuYap_Click_1(object sender, EventArgs e)
        {
            String basvuran = cbIlanIcınIscıler.Text;
            if (!basvuran.Equals(""))
            {
                int temp = isciler.FindIndex(x => x.Isim == basvuran);
                if (temp >= 0)
                {
                    bool basvurmusMu = false;

                    IsIlani basvuralacakIlan = ilanlar.GetIlan(GetKey(cbİlanlar.Text));
                    foreach (HeapNode item in basvuralacakIlan.adaylar.heapArray)
                    {
                        if (item != null && item.IsciVerileri.Isim == isciler[temp].Isim)
                        {
                            basvurmusMu = true;
                        }
                    }
                    if (basvurmusMu)    
                    {
                        MessageBox.Show(isciler[temp].Isim + " adli aday daha önce başvuru yapmış");
                    }
                    else
                    {
                        double basvuruUygunlugu = (new Random()).NextDouble() * 10.0;
                        basvuralacakIlan.adaylar.Insert(
                                new HeapNode()
                                {
                                    IsciVerileri = isciler[temp],
                                    Uygunluk = basvuruUygunlugu
                                }
                            );
                        MessageBox.Show("Aday başarıyla başvurmuştur..Uygunluk Puanı: " + basvuruUygunlugu.ToString("N2"));
                        tbIlanAra.Text = "";
                        cbIsIlanlari.SelectedValue = "";
                        lIlanIsim.Text = "-";
                        lIlanilanTanim.Text = "-";
                        tbIlankriterler.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("İşçi Bulunamadı !");
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
