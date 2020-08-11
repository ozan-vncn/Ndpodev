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
namespace personelTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //Veritabanı doysa yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=Personel.accdb");

        //Formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);// kullanıcılar isimli tabloda ki veriler getirildi.
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//kayıt okuma isimli data reader da saklandı
                while (kayitokuma.Read())//veriye rastlandıysa
                {
                    if (radioButton1.Checked == true)
                    {
                        //Girilen değerler ile veritabanında belirlenen değerleri karşılaştırdık
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();//sıfırıncı alanı tc no değişkenine atandı
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();//form gizlendi
                            Form2 frm2 = new Form2();//form 2 ye geçiş için değişken tanımlandı
                            frm2.Show();
                            break;
                        }
                    }
                        if (radioButton2.Checked == true)
                        {
                            //Girilen değerler ile veritabanında belirlenen değerleri karşılaştırdık
                            if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                            {
                                durum = true;
                                tcno = kayitokuma.GetValue(0).ToString();//sıfırıncı alanı tc no değişkenine atandı
                                adi = kayitokuma.GetValue(1).ToString();
                                soyadi = kayitokuma.GetValue(2).ToString();
                                yetki = kayitokuma.GetValue(3).ToString();
                                this.Hide();//form gizlendi
                                Form3 frm3 = new Form3();//form 2 ye geçiş için değişken tanımlandı
                                frm3.Show();
                                break;
                            }
                        }
                    
                }
                if (durum==false)
                {
                    hak--;
                    baglantim.Close();
                }
                


            }
            label5.Text = Convert.ToString(hak);
            if(hak==0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş hakkı kalmadı!","Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        //yerel değişkenler
        int hak = 3; bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1; this.CancelButton = button2;//Enter ı basıldığında giriş yapsın esc ye basıldığında çıksın
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;//Form ekranın ortasında gelicek
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;//Sadece x butonu oldu
        }
    }
}
