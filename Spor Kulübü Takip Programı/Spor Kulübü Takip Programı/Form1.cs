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
using System.Data.OleDb; //VERİTABANI KULLANACAĞIMIZ İÇİN BU KÜTÜPHANEYİ EKLEDİK 

namespace Spor_Kulübü_Takip_Programı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Veritabani Sağlayıcısının(Microsoft Access) Ve Kaynağın Belirlenmesi
        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Ace.OleDB.12.0;Data Source=Kisiler.accdb");

        //Bu Değişkenleri Tüm Formlarda Kullanacağız
        public static string ad, soyad, tcNo, kullaniciTur;

        //Bu Değişkenleri Sadece Bu Formda Kullanacağiz 
        bool kontrol = false;   //Veritabanıyla Textbox'a girilen girdileri kontrol ederken kullanılacak.

        int denemeMiktari = 100;  //Textbox'a yanlış girildiği zaman programın error vermesini engellemek 
                                  //için deneme miktarı oluşturdum.

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) //Giriş Yap butonu
        {
            if (denemeMiktari != 0)
            {
                myConnection.Open();
                OleDbCommand selectSorgu = new OleDbCommand("select * from kullanicilar", myConnection); //kullanicilar tablosunda bilgiler getirilir
                OleDbDataReader kayitOkuma = selectSorgu.ExecuteReader(); //Veriler kayitOkuma'ya aktarılır.

                while (kayitOkuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitOkuma["kullaniciAd"].ToString() == textBox1.Text &&
                            kayitOkuma["sifre"].ToString() == textBox2.Text &&
                            kayitOkuma["kullaniciTur"].ToString() == "Admin")
                            {
                                kontrol = true; //başta false olan kontrol değişkenini if değişkeni sağlarsa true'ya çevirir.
                                tcNo = kayitOkuma.GetValue(0).ToString(); //Access DB'deki 0. alanı tcNo değişkenine aktarır.
                                ad = kayitOkuma.GetValue(1).ToString(); //Access DB'deki 1.alanı ad değişkenine aktarır.
                                soyad = kayitOkuma.GetValue(2).ToString(); //Access DB'deki 2.alanı soyad değişkenine aktarır.
                                kullaniciTur = kayitOkuma.GetValue(3).ToString(); //Access DB'deki 3.alanı kullaniciTur değişkenine aktarır.
                                this.Hide();                    //Form 1'i kapatıp
                                Form2 frm2 = new Form2();
                                frm2.Show();                    //Form 2'yi açıyoruz.
                                break;
                            }
                        }

                        if (radioButton2.Checked == true)
                        {
                            if (kayitOkuma["kullaniciAd"].ToString() == textBox1.Text &&
                                kayitOkuma["sifre"].ToString() == textBox2.Text &&
                                kayitOkuma["kullaniciTur"].ToString() == "Standart")
                            {
                                kontrol = true;
                                tcNo = kayitOkuma.GetValue(0).ToString();
                                ad = kayitOkuma.GetValue(1).ToString();
                                soyad = kayitOkuma.GetValue(2).ToString();
                                kullaniciTur = kayitOkuma.GetValue(3).ToString();
                                this.Hide();                    //Form 1'i kapatıp
                                Form3 frm3 = new Form3();
                                frm3.Show();                    //Form 3'ü açıyoruz.
                                break;
                            }
                        }
                    }

                    if (kontrol == false)
                        denemeMiktari--;
                        label4.Text = "Lütfen Tekrar Deneyiniz.";
                        textBox1.Clear();
                        textBox2.Clear();
                        myConnection.Close();
                    }
                }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
    }
}
