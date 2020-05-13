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
using System.Data.OleDb;

namespace Spor_Kulübü_Takip_Programı
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }



        private void label11_Click(object sender, EventArgs e)
        {

        }

        OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.Ace.OleDB.12.0;Data Source=Kisiler.accdb");

        private void kuluptekileriGoster()
        {
            try
            {
                myConnection.Open();
                OleDbDataAdapter kuluptekileri_listele = new OleDbDataAdapter
                    //Access Tablosunda Sütün İsimlerini Köşeli Parantez İçindekilerle Değiştirdim.
                    ("select tcno AS[TC Kimlik No], ad AS[Adı], soyad AS[Soyadı], cinsiyet AS[Cinsiyeti]," +
                    "dogumTarih AS[Doğum Tarihi], kulupGirisTarih AS[Kulübe Giriş Tarihi], brans AS[Branşı]," +
                    "mevkii AS[Mevkiisi], aylikKazanc AS[Aylık Kazancı] from kuluptekiler Order By ad ASC", myConnection);

                DataSet dataSetUstundekiler = new DataSet();  
                kuluptekileri_listele.Fill(dataSetUstundekiler);   //DataAdapter'deki verileri DataSet'e aktardım.
                dataGridView1.DataSource = dataSetUstundekiler.Tables[0];  //DataGridView1'in kaynağını datasettekiler olarak belirledim.
                myConnection.Close();

            }
            catch (Exception errorMessage)
            {
                MessageBox.Show(errorMessage.Message, "HATA", MessageBoxButtons.OK); //Select sorgusunda yanlış bir ifade olursa
                myConnection.Close();                                                //hatayı kontrol etmemiz için bunu oluşturdum.
                
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            kuluptekileriGoster();
            maskedTextBox1.Mask="00000000000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitBul = false;
            if (maskedTextBox1.Text.Length==11)
            {
                myConnection.Open();
                OleDbCommand selectSorgusu = new OleDbCommand("select * from kuluptekiler where tcno='" + maskedTextBox1.Text + "'", myConnection);
                OleDbDataReader kayitOkuyucu = selectSorgusu.ExecuteReader(); //kayitOkuyucu'ya selectsorgu sonuçlarını aktarıyoruz.
                while (kayitOkuyucu.Read()==true)
                {
                    kayitBul = true;
                    label10.Text = kayitOkuyucu.GetValue(1).ToString(); //ad
                    label11.Text=  kayitOkuyucu.GetValue(2).ToString(); //soyad
                        
                    if (kayitOkuyucu.GetValue(3).ToString()=="BAY")     //cinsiyet
                    {
                        label12.Text = "Bay";
                    }
                    else
                    {
                        label12.Text = "Bayan";
                    }

                    label13.Text = kayitOkuyucu.GetValue(4).ToString(); //dogumTarih
                    label14.Text = kayitOkuyucu.GetValue(5).ToString(); //kulupGirisTarih
                    label15.Text = kayitOkuyucu.GetValue(6).ToString(); //brans
                    label16.Text = kayitOkuyucu.GetValue(7).ToString(); //mevkii
                    label17.Text = kayitOkuyucu.GetValue(8).ToString(); //aylikKazanc
                    break;
                }
                if (kayitBul==false)
                {
                    MessageBox.Show("Sistemde kayıt bulunamadı.");
                }
                myConnection.Close();
            }

            else //TCNO 11 haneli değilse.
            {
                MessageBox.Show("Lütfen 11 hane giriniz.");
            }
           
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length < 11)  //11 Haneden az miktar girilirse uyarı verecek.
            {
                errorProvider1.SetError(maskedTextBox1, "TC Kimlik No 11 karakter olmalıdır.");
            }
            else
                errorProvider1.Clear();
            kuluptekileriGoster();
        }
    }
}
