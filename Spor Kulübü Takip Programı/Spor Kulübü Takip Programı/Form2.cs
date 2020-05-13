/****************************************************************************
**                     SAKARYA ÜNİVERSİTESİ
**          BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**           BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
**              NESNEYE DAYALI PROGRAMLAMA DERSİ
**                  2019-2020 BAHAR DÖNEMİ
**
**
**              PROJE NUMARASI...........:01
**              ÖĞRENCİ ADI..............:ANIL
**              ÖĞRENCİ SOYADI...........:ELMASTAŞI
**              DERSİN ALINDIĞI GRUP....:A
**
*****************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //Veritabanı kullandığım için bu kütüphaneyi ekliyorum.


namespace Spor_Kulübü_Takip_Programı
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Veritabani Sağlayıcısının(Microsoft Access) Ve Kaynağın Belirlenmesi
        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Ace.OleDB.12.0;Data Source=Kisiler.accdb");


        private void kisileriGoster() //DataGridView1'i doldurmak için yazılan metot.
        {
            try
            {
                myConnection.Open();

                OleDbDataAdapter kisileri_listele = new OleDbDataAdapter
                    //Access Tablosunda Sütün İsimlerini Köşeli Parantez İçindekilerle Değiştirdim.
                    ("select tcno AS[TC Kimlik No], ad AS[Adı], soyad as[Soyadı], kullaniciTur AS[Kullanıcı Türü]," +
                    "kullaniciAd AS[Kullanıcı İsmi], sifre AS[Şifre] from kullanicilar order by ad ASC", myConnection);

                DataSet dataSetUstundekiler = new DataSet();  //DataSet oluşturdum  
                kisileri_listele.Fill(dataSetUstundekiler);   //DataAdapter'deki verileri DataSet'e aktardım.
                dataGridView1.DataSource = dataSetUstundekiler.Tables[0];  //DataGridView1'in kaynağını datasettekiler olarak belirledim.
                myConnection.Close();
            }
            catch (Exception errorMessage)
            {
                MessageBox.Show(errorMessage.Message, "HATA", MessageBoxButtons.OK); //Select sorgusunda yanlış bir ifade olursa
                myConnection.Close();                                                //hatayı kontrol etmemiz için bunu oluşturdum.
            }
        }

        private void kuluptekileriGoster() //DataGridView 2'yi doldurmak için yazılan metot
        {
            try
            {
                myConnection.Open();

                OleDbDataAdapter kuluptekileri_listele = new OleDbDataAdapter
                    //Access Tablosunda Sütün İsimlerini Köşeli Parantez İçindekilerle Değiştirdim.
                    ("select tcno AS[TC Kimlik No], ad AS[Adı], soyad AS[Soyadı], cinsiyet AS[Cinsiyeti]," +
                    "dogumTarih AS[Doğum Tarihi], kulupGirisTarih AS[Kulübe Giriş Tarihi], brans AS[Branşı]," +
                    "mevkii AS[Mevkiisi], aylikKazanc AS[Aylık Kazancı] from kuluptekiler order by ad ASC", myConnection);

                DataSet dataSetUstundekiler = new DataSet();  //DataSet oluşturdum  
                kuluptekileri_listele.Fill(dataSetUstundekiler);   //DataAdapter'deki verileri DataSet'e aktardım.
                dataGridView2.DataSource = dataSetUstundekiler.Tables[0];  //DataGridView2'nin kaynağını datasettekiler olarak belirledim.
                myConnection.Close();
            }
            catch (Exception errorMessage)
            {
                MessageBox.Show(errorMessage.Message, "HATA", MessageBoxButtons.OK); //Select sorgusunda yanlış bir ifade olursa
                myConnection.Close();                                                //hatayı kontrol etmemiz için bunu oluşturdum.
            }
        }

        private void hesapIslemleri_Temizle() //Hesap İşlemleri sekmesindeki kutucukları temizler.
        {
            textBox4.Clear();
            textBox5.Clear();
            maskedTextBox5.Clear();
            maskedTextBox6.Clear();
            maskedTextBox7.Clear();
        }

        private void kulupKayitIslemleri_Temizle() //Kulüp Kayıt İşlemleri sekmesindeki Kutucukları temizler.
        {
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e) //Form2 Load kodları
        {

            //hesap işlemleri
            maskedTextBox5.Mask = "00000000000"; //TC Kimlik No Yerine sadece rakam yazabiliriz.

            maskedTextBox6.Mask = "LL???????????????????"; //İsim yerine en az 2 harfli sadece harf girebiliriz.
            maskedTextBox6.Text.ToUpper();

            maskedTextBox7.Mask = "LL???????????????????"; //Soyad yerine en az 2 harfli sadece harf girebiliriz.
            maskedTextBox7.Text.ToUpper();

            kisileriGoster(); //DataGridView 1 için yukarıda yazılan metot.

            //kuluüp kayıt işlemleri
            maskedTextBox1.Mask = "00000000000"; //TC Kimlik No Yerine sadece rakam yazabiliriz.

            maskedTextBox2.Mask = "LL???????????????????"; //İsim yerine en az 2 harfli sadece harf girebiliriz.
            maskedTextBox2.Text.ToUpper();

            maskedTextBox3.Mask = "LL???????????????????"; //Soyad yerine en az 2 harfli sadece harf girebiliriz.
            maskedTextBox3.Text.ToUpper();

            maskedTextBox4.Mask = "000009999999"; //Maaşa sadece rakam yazabiliriz.

            DateTime dogumTarih = DateTime.Now;
            int yil = int.Parse(dogumTarih.ToString("yyyy"));
            int ay = int.Parse(dogumTarih.ToString("MM"));
            int gun = int.Parse(dogumTarih.ToString("dd"));
            dateTimePicker1.MinDate = new DateTime(1930, 1, 1); //Doğum Tarihi Seçiminin minimum tarihi
            dateTimePicker1.MaxDate = new DateTime(yil, ay, gun); //Doğum Tarihi Seçiminin maksimum tarihi (şimdiki zaman)   
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            //Branşı kutusuna eklenecekler 
            comboBox1.Items.Add("Futbol");
            comboBox1.Items.Add("Basketbol");
            comboBox1.Items.Add("Amerikan Futbolu");
            comboBox1.Items.Add("Su Topu");
            comboBox1.Items.Add("Hentbol");
            comboBox1.Items.Add("Sağlık");

            //Mevkii kutusuna eklenecekler
            comboBox2.Items.Add("Kaleci");
            comboBox2.Items.Add("Defans");
            comboBox2.Items.Add("Orta Saha");
            comboBox2.Items.Add("Forvet");
            comboBox2.Items.Add("Teknik Direktör");
            comboBox2.Items.Add("Teknik Direktör Yardımcısı");
            comboBox2.Items.Add("Sağlık Personeli");


            dateTimePicker2.MinDate = new DateTime(1930, 1, 1); //Kulube Giriş Tarihi Seçiminin minimum tarihi
            dateTimePicker2.MaxDate = new DateTime(yil, ay, gun); //Kulübe Tarihi Seçiminin maksimum tarihi (şimdiki zaman) 
            dateTimePicker2.Format = DateTimePickerFormat.Short;


            kuluptekileriGoster(); //DataGridView 2 için yukarıda yazdılan metot
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox5_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox5.Text.Length < 11)  //11 Haneden az miktar girilirse uyarı verecek.
            {
                errorProvider1.SetError(maskedTextBox5, "TC Kimlik No 11 karakter olmalıdır.");
            }
            else
                errorProvider1.Clear();
            kisileriGoster();
        }//TC Kimlik No Uyarı

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length < 11)  //11 Haneden az miktar girilirse uyarı verecek.
            {
                errorProvider1.SetError(maskedTextBox1, "TC Kimlik No 11 karakter olmalıdır.");
            }
            else
                errorProvider1.Clear();
            kuluptekileriGoster();
        } //TC Kimlik NO Uyarı


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //BUL BUTONU
        {
            bool kayitBulma = false;

            if (maskedTextBox5.Text.Length == 11) //11 karakter girilmişse.
            {
                myConnection.Open();
                //Veritabanındaki tcno kısmı ile girilen tcnoyu karşılaştırır.
                OleDbCommand selectSorgusu = new OleDbCommand("select * from kullanicilar where tcno='" + maskedTextBox5.Text + "'", myConnection);
                OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader();
                while (kayitOkuyucu.Read())
                {
                    kayitBulma = true; //kayıt bulunur.

                    //Veritabanındaki 1.kısım(ad) kısmı formdaki ad kısmına yazılır.
                    maskedTextBox6.Text = kayitOkuyucu.GetValue(1).ToString();

                    //Veritabanındaki 2.kısım(soyad) kısmı formdaki soyad kısmına yazılır.
                    maskedTextBox7.Text = kayitOkuyucu.GetValue(2).ToString();

                    //Veritabanındaki 3.kısım(hesap türü) kısmı formdaki hesap türü kısmında işaretlenir.
                    if (kayitOkuyucu.GetValue(3).ToString() == "Admin")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    //Kullanıcı ismi
                    textBox4.Text = kayitOkuyucu.GetValue(5).ToString();

                    //Şifre
                    textBox5.Text = kayitOkuyucu.GetValue(4).ToString();
                    break;
                }

                if (kayitBulma == false) //kayıt bulunmazsa
                {
                    MessageBox.Show("Sistemde böyle bir kayıt yok.");
                }

                myConnection.Close();
            }
            else //11 karakter girilmemiş ise
            {
                MessageBox.Show("11 karakter giriniz.");
                hesapIslemleri_Temizle();
            }
        }

        private void button2_Click(object sender, EventArgs e) //EKLE BUTONU
        {
            string kullaniciTuru = "";
            bool kayitKontrol = false;

            myConnection.Open();
            OleDbCommand selectSorgusu = new OleDbCommand("select * from kullanicilar where tcno='" + maskedTextBox5.Text + "'", myConnection);
            OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader();
            while (kayitOkuyucu.Read())
            {
                kayitKontrol = true;
                break;
            }
            myConnection.Close();

            if (kayitKontrol == false)
            {
                //Koşul sağlanmazsa TC Kimlik No Yazısı Kırmızıya Döner.
                //if (maskedTextBox5.Text.Length < 11 || maskedTextBox5.Text == "") alttaki ile aynı görevi gören kod.
                if(maskedTextBox5.MaskCompleted==false)
                {
                    label1.ForeColor = Color.Red;
                }
                else
                    label1.ForeColor = Color.Black;

                //Koşul sağlanmazsa Adı Yazısı Kırmızıya Döner.
                //if (maskedTextBox6.Text.Length < 2 || maskedTextBox5.Text == "") alttaki kod ile aynı görevi gören kod.
                if(maskedTextBox6.MaskCompleted==false)
                {
                    label2.ForeColor = Color.Red;
                }
                else
                    label2.ForeColor = Color.Black;

                //Koşul sağlanmazsa Soyadı Yazısı Kırmızıya Döner.
                //if (maskedTextBox7.Text.Length < 2 || maskedTextBox5.Text == "") alttaki kod ile aynı görevi gören kod.
                if(maskedTextBox7.MaskCompleted==false)
                {
                    label3.ForeColor = Color.Red;
                }
                else
                    label3.ForeColor = Color.Black;

                //Koşul sağlanmazsa Kullanıcı İsmi Yazısı Kırmızıya Döner.
                if (textBox4.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                    label5.ForeColor = Color.Black;

                //Koşul sağlanmazsa Şifre Yazısı Kırmızıya Döner.
                if (textBox5.Text == "")
                {
                    label6.ForeColor = Color.Red;
                }
                else
                    label6.ForeColor = Color.Black;

                if (maskedTextBox5.Text.Length == 11 && maskedTextBox6.Text.Length > 1 && maskedTextBox7.Text.Length > 1 &&
                    maskedTextBox5.Text != "" && maskedTextBox6.Text != "" && maskedTextBox7.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                    if (radioButton1.Checked == true) //Admin seçilmiş ise
                    {
                        kullaniciTuru = "Admin";
                    }
                    else if (radioButton2.Checked == true) //Standart kullanıcı seçilmiş ise
                    {
                        kullaniciTuru = "Standart";
                    }
                    try
                    {
                        myConnection.Open();
                        OleDbCommand yeniEkleme = new OleDbCommand("insert into kullanicilar values ('" + maskedTextBox5.Text + "'," +
                            " '" + maskedTextBox6.Text + "','" + maskedTextBox7.Text + "','" + kullaniciTuru + "','" + textBox5.Text + "'," +
                            " '" + textBox4.Text + "')", myConnection);
                        yeniEkleme.ExecuteNonQuery();
                        myConnection.Close();
                        MessageBox.Show("Yeni Kullanıcı Eklendi", "", MessageBoxButtons.OK);
                        hesapIslemleri_Temizle();
                    }
                    catch (Exception error)
                    {

                        MessageBox.Show(error.Message);
                        myConnection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Girdileri tekrar doldurun.", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Girilen Bilgiler Zaten Sistemde Kayıtlı", "", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e) //GÜNCELLE BUTONU
        {
            string kullaniciTuru = "";

            //Koşul sağlanmazsa TC Kimlik No Yazısı Kırmızıya Döner.
            if (maskedTextBox5.Text.Length < 11 || maskedTextBox5.Text == "")
            {
                label1.ForeColor = Color.Red;
            }
            else
                label1.ForeColor = Color.Black;

            //Koşul sağlanmazsa Adı Yazısı Kırmızıya Döner.
            if (maskedTextBox6.Text.Length < 2 || maskedTextBox5.Text == "")
            {
                label2.ForeColor = Color.Red;
            }
            else
                label2.ForeColor = Color.Black;

            //Koşul sağlanmazsa Soyadı Yazısı Kırmızıya Döner.
            if (maskedTextBox7.Text.Length < 2 || maskedTextBox5.Text == "")
            {
                label3.ForeColor = Color.Red;
            }
            else
                label3.ForeColor = Color.Black;

            //Koşul sağlanmazsa Kullanıcı İsmi Yazısı Kırmızıya Döner.
            if (textBox4.Text == "")
            {
                label5.ForeColor = Color.Red;
            }
            else
                label5.ForeColor = Color.Black;

            //Koşul sağlanmazsa Şifre Yazısı Kırmızıya Döner.
            if (textBox5.Text == "")
            {
                label6.ForeColor = Color.Red;
            }
            else
                label6.ForeColor = Color.Black;

            if (maskedTextBox5.Text.Length == 11 && maskedTextBox6.Text.Length > 1 && maskedTextBox7.Text.Length > 1 &&
                maskedTextBox5.Text != "" && maskedTextBox6.Text != "" && maskedTextBox7.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                if (radioButton1.Checked == true) //Admin seçilmiş ise
                {
                    kullaniciTuru = "Admin";
                }
                else if (radioButton2.Checked == true) //Standart kullanıcı seçilmiş ise
                {
                    kullaniciTuru = "Standart";
                }
                try
                {
                    myConnection.Open();
                    //Access Veritabanındaki bilgileri günceller.
                    OleDbCommand yeniGuncelleme = new OleDbCommand("update kullanicilar set ad='" + maskedTextBox6.Text + "',"
                        + "soyad='" + maskedTextBox7.Text + "',kullaniciTur='" + kullaniciTuru + "',kullaniciAd='" + textBox4.Text + "',"
                        + "sifre='" + textBox5.Text + "' where tcno='"+maskedTextBox5.Text+"'", myConnection);
                       

                    yeniGuncelleme.ExecuteNonQuery();
                    myConnection.Close();
                    MessageBox.Show("Kullanıcı Güncellendi");
                    hesapIslemleri_Temizle();
                    kisileriGoster();
                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);
                    myConnection.Close();
                }
            }
            else
            {
                MessageBox.Show("Girdileri tekrar doldurun.");
            }
        }

        private void button4_Click(object sender, EventArgs e) //KAYDI SİL BUTONU
        {
            if (maskedTextBox5.Text.Length==11)
            {
                bool kayitArama = false;
                myConnection.Open();
                //Kutuya yazılan TC'yi Access Table'daki ile eşleşenle seçim yapar.
                OleDbCommand selectSorgusu = new OleDbCommand("select * from kullanicilar where tcno='" + maskedTextBox5.Text + "'", myConnection);
                OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader(); //sorgu sonucları kayitOkuyucu'ya eklenir.

                while (kayitOkuyucu.Read())
                {
                    kayitArama = true;
                    OleDbCommand deleteSorgusu = new OleDbCommand("delete from kullanicilar where tcno='" + maskedTextBox5.Text + "'", myConnection);
                    deleteSorgusu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silinmiştir.");
                    myConnection.Close();
                    kisileriGoster();
                    hesapIslemleri_Temizle();
                    break;
                }
                if (kayitArama==false) //while döngüsü çalışmamışsa
                {
                    MessageBox.Show("Kayıt bulunamadı.");
                }
                myConnection.Close();
                hesapIslemleri_Temizle();

            }
            else //11 basamaklı TC girilmez ise
            {
                MessageBox.Show("11 basmaklı TC giriniz.");
            }

        }

        private void button5_Click(object sender, EventArgs e) //TEMİZLE BUTONU
        {
            hesapIslemleri_Temizle();
        }

        private void button6_Click(object sender, EventArgs e) //BUL BUTONU 2.SEKME
        {
            bool kayitBulma = false;
            if (maskedTextBox1.Text.Length==11)
            {
                myConnection.Open();
                OleDbCommand selectSorgusu = new OleDbCommand("select * from kuluptekiler where tcno='" + maskedTextBox1.Text + "'", myConnection);
                OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader();

                while (kayitOkuyucu.Read()==true)
                {
                    kayitBulma = true;
                    maskedTextBox2.Text = kayitOkuyucu.GetValue(1).ToString(); //Access Database'deki 1.sütünla doldur.
                    maskedTextBox3.Text = kayitOkuyucu.GetValue(2).ToString();

                    if (kayitOkuyucu.GetValue(3).ToString() == "BAY") //Access DB'deki 3.sütün bay ise
                    {
                        radioButton3.Checked = true;                //bay butonunu tikle
                    }
                    else                                            //değilse
                    {
                        radioButton4.Checked = true;                //bayan radyo butonunu tikle.
                    }

                    dateTimePicker1.Text = kayitOkuyucu.GetValue(4).ToString();
                    dateTimePicker2.Text = kayitOkuyucu.GetValue(5).ToString();
                    comboBox1.Text = kayitOkuyucu.GetValue(6).ToString();
                    comboBox2.Text = kayitOkuyucu.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitOkuyucu.GetValue(8).ToString();
                    break;
                }
                if (kayitBulma==false)
                {
                    MessageBox.Show("Sistemde böyle bir kayıt yok.");
                }
                myConnection.Close();
            }
            else //11 haneli TC yazılmamışsa
            {
                MessageBox.Show("11 haneyi giriniz.");
               
            }
        }

        private void button7_Click(object sender, EventArgs e) //EKLE BUTONU 2.SEKME
        {
            bool kayitKontrol = false;
            string cinsiyet = "";

            myConnection.Open();

            
            OleDbCommand selectSorgusu = new OleDbCommand("select * from kuluptekiler where tcno='" + maskedTextBox1.Text + "'", myConnection);
            OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader(); //selectSorgusu'ndakiler kayitOkuyucu'ya yazılır.

            while (kayitOkuyucu.Read()==true)
            {
                kayitKontrol = true;
                break;
            }
            myConnection.Close();

            if (kayitKontrol==false)
            {
                if (maskedTextBox1.MaskCompleted==false) //TC Kimlik No kısmı önceden belirlenen koşula göre
                {
                    label7.ForeColor = Color.Red;       //doldurulmaz ise kırmızı olur.
                }
                else
                {
                    label7.ForeColor = Color.Black;
                }


                if (maskedTextBox2.MaskCompleted == false) //Adı kısmı için aynı kural.
                {
                    label8.ForeColor = Color.Red;
                }
                else
                {
                    label8.ForeColor = Color.Black;
                }


                if (maskedTextBox3.MaskCompleted == false) //Soyadı kısmı için aynı kural.
                {
                    label9.ForeColor = Color.Red;
                }
                else
                {
                    label9.ForeColor = Color.Black;
                }


                if (comboBox1.Text=="") //Branşı kısmı boş kolursa aynı kural.
                {
                    label13.ForeColor = Color.Red;
                }
                else
                {
                    label13.ForeColor = Color.Black;
                }


                if (comboBox2.Text == "") //Mevki kısmı boş kolursa aynı kural.
                {
                    label14.ForeColor = Color.Red;
                }
                else
                {
                    label14.ForeColor = Color.Black;
                }


                if (maskedTextBox4.MaskCompleted == false) //Aylık kazancı kısmı için aynı kural.
                {
                    label15.ForeColor = Color.Red;
                }
                else
                {
                    label15.ForeColor = Color.Black;
                }

                if (maskedTextBox1.MaskCompleted!=false && maskedTextBox2.MaskCompleted!=false && maskedTextBox3.MaskCompleted!=false
                    && comboBox1.Text!="" &&comboBox2.Text!=""&&maskedTextBox4.MaskCompleted!=false)
                {
                    if (radioButton3.Checked==true)
                    {
                        cinsiyet = "BAY";
                    }
                    else if(radioButton4.Checked==true)
                    {
                        cinsiyet = "BAYAN";
                    }
                    try
                    {
                        myConnection.Open();
                        OleDbCommand ekleme = new OleDbCommand("insert into kuluptekiler values('" + maskedTextBox1.Text + "',"
                            + "'" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + dateTimePicker1.Text + "',"
                            + "'" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text+"','" + maskedTextBox4.Text+"')", myConnection);
                        ekleme.ExecuteNonQuery();
                        myConnection.Close();
                        MessageBox.Show("Başarıyla Eklendi");
                        kuluptekileriGoster();
                        kulupKayitIslemleri_Temizle();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                        myConnection.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Lütfen tekrar deneyiniz.");
                }

            }
            else //kayitkontrol==true ise
            {
                MessageBox.Show("Bu bilgiler zaten kayıtlıdır.");
            } 
        }

        private void button8_Click(object sender, EventArgs e) //GÜNCELLE BUTONU 2.SEKME
        {
            
            string cinsiyet = "";

           

            
                if (maskedTextBox1.MaskCompleted == false) //TC Kimlik No kısmı önceden belirlenen koşula göre
                {
                    label7.ForeColor = Color.Red;       //doldurulmaz ise kırmızı olur.
                }
                else
                {
                    label7.ForeColor = Color.Black;
                }


                if (maskedTextBox2.MaskCompleted == false) //Adı kısmı için aynı kural.
                {
                    label8.ForeColor = Color.Red;
                }
                else
                {
                    label8.ForeColor = Color.Black;
                }


                if (maskedTextBox3.MaskCompleted == false) //Soyadı kısmı için aynı kural.
                {
                    label9.ForeColor = Color.Red;
                }
                else
                {
                    label9.ForeColor = Color.Black;
                }


                if (comboBox1.Text == "") //Branşı kısmı boş kolursa aynı kural.
                {
                    label13.ForeColor = Color.Red;
                }
                else
                {
                    label13.ForeColor = Color.Black;
                }


                if (comboBox2.Text == "") //Mevki kısmı boş kolursa aynı kural.
                {
                    label14.ForeColor = Color.Red;
                }
                else
                {
                    label14.ForeColor = Color.Black;
                }


                if (maskedTextBox4.MaskCompleted == false) //Aylık kazancı kısmı için aynı kural.
                {
                    label15.ForeColor = Color.Red;
                }
                else
                {
                    label15.ForeColor = Color.Black;
                }

                if (maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false
                    && comboBox1.Text != "" && comboBox2.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                    {
                        cinsiyet = "BAY";
                    }
                    else if (radioButton4.Checked == true)
                    {
                        cinsiyet = "BAYAN";
                    }
                    try
                    {
                        myConnection.Open();
                    OleDbCommand guncelleme = new OleDbCommand("update kuluptekiler set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "'," +
                        "cinsiyet='" + cinsiyet + "',dogumTarih='" + dateTimePicker1.Text + "',kulupGirisTarih='" + dateTimePicker2.Text + "',brans='" + comboBox1.Text + "'," +
                        "mevkii='" + comboBox2.Text + "',aylikKazanc='" + maskedTextBox4.Text + "' where tcno='"+maskedTextBox1.Text+"'", myConnection);

                        guncelleme.ExecuteNonQuery();
                        myConnection.Close();
                        MessageBox.Show("Başarıyla Güncellendi.");
                        kuluptekileriGoster();
                        kulupKayitIslemleri_Temizle();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                        myConnection.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Lütfen tekrar deneyiniz.");
                }

            
            
        }

        private void button9_Click(object sender, EventArgs e) //KAYDI SİL BUTONU 2.SEKME
        {
            if (maskedTextBox1.MaskCompleted==true)
            {
                bool kayitBul = false;
                myConnection.Open();
                OleDbCommand arayiciSorgu = new OleDbCommand("select * from kuluptekiler where tcno='" + maskedTextBox1.Text + "'", myConnection);
                OleDbDataReader kayitOkuyucu = arayiciSorgu.ExecuteReader(); //eşleşen kayıt olursa aktarır.
                while (kayitOkuyucu.Read()==true) 
                {
                    kayitBul = true;
                    OleDbCommand silme = new OleDbCommand("delete from kuluptekiler where tcno='" + maskedTextBox1.Text + "'", myConnection);
                    silme.ExecuteNonQuery();
                    break;
                }
                if (kayitBul==false)
                {
                    MessageBox.Show("Kayıt Bulunamadı.");
                }
                myConnection.Close();
                kuluptekileriGoster();
                kulupKayitIslemleri_Temizle();
            }
            else
            {
                MessageBox.Show("11 Haneli TC Giriniz.");
            }
        }

        private void button10_Click(object sender, EventArgs e) //TEMİZLE BUTONU 2.SEKME
        {
            kulupKayitIslemleri_Temizle();
        }

    }
}
