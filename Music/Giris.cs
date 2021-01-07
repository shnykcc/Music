using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.IO;

namespace Music
{
    public partial class Giris : Form
    {
        SqlConnection bgln2 = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True");
        SqlConnection bgln = new SqlConnection("Data Source=.;Initial Catalog=MusicProject;Integrated Security=True");


        //sql'e baglanmak için baglantısı nesnesi oluşturuyorum.
        public Giris()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            label2.Visible = true;
            label1.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            button4.Visible = true;
            checkBox1.Visible = true;
            //Giriş yap buttonuna tıklayınca kullanıcı adı şifre, şifreyi göster ve giriş buttonunu görünür hale getirip diğerlerini görünmez yapıyorum.
        }
        public static string isim = "";
        public static string kaydeden = "";
        public static string kaydeden2 = "";
        //daha sonra kullanmak için 2 adet public ve static değişken oluşturuyorun.
        private void button3_Click(object sender, EventArgs e)
        {
            bgln.Open();
            string denetMail = "";
            string denetSifre = "";
            bool sifreUygunmu = false, kadiUygunmu = false;
            SqlCommand okur = new SqlCommand("select EMail,Password,Name from kayitlar", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            while (oku.Read())
            {
                denetSifre = oku["Password"].ToString().Trim();
                denetMail = oku["EMail"].ToString().Trim().ToLower();
                isim = oku["Name"].ToString().Trim();
                kaydeden = denetMail;
                if (denetMail == textBox1.Text.ToLower().Trim())
                {
                    if (denetSifre == textBox2.Text)
                    {
                        kaydeden2 = textBox1.Text;
                        mood_secimi frm3 = new mood_secimi();
                        frm3.Show();
                        this.Hide();
                        sifreUygunmu = true;
                    }
                    kadiUygunmu = true;
                }
            }
            if (kadiUygunmu == false)
            {
                MessageBox.Show("Böyle bir kullanıcı yok. Lütfen kullanıcı adınızı denetleyin", "Kullanıcı bulunamadı");
            }
            else if (sifreUygunmu == false)
            {
                MessageBox.Show("Şifreniz yanlış.", "Şifre Yanlış");
            }
            bgln.Close();
            /*Sisteme gir'E tıkladığımda mail ve şifre denetlemek için 2 tane değişken oluşturuyorum. Uygunluklarını test etmek içinde 2 tane bool değişken oluşturuyorun
             Daha sonra sql'den veri cekebilmek için bir okuma nesnesi oluşturuyorum. bu nesneleri okurken aynı zamanda mail, şifre ve isimi de kaydediyor. kaydeden adlı parametreye
            denetmail'den yine kayıt yapıyor. Eğer denetmail ile girdiğimiz mail aynıysa girdiğimiz şifre ile sistemdeki şifreyi denetliyor. Eğer mail yoksa kadi false kalıyor.
            Messagebox ile böyle bir kullanıcı yok diyor. Eğer şifre yok ise de şifreuygunmu false kalıyor ve şifreniz yanlış diye ekrana messagebox geliyor.*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kayit_Ol frm2 = new Kayit_Ol();
            frm2.Show();
            this.Hide();
            //Eğer kayit ol'a tılanırsa kayıtolma formuna geciliyor ve etkin olan form gizleniyor.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            label2.Visible = false;
            label1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;
            checkBox1.Visible = false;
            //Eğer geri oka basılırsa eski haline dönmesi için birimler ilk haline dönüyor.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
            //eğer şifreyi göster tiki işaretliyse normal text girer gibi gösteriyor fakat tikli değilse yıldız ile görünüyor ve kullanıcı daha güvende oluyor.
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            bgln2.Open();
            SqlCommand kontrolKomutu = new SqlCommand(@"SELECT Count(name) FROM master.dbo.sysdatabases WHERE name=@prmVeritabani", bgln2);
            kontrolKomutu.Parameters.AddWithValue("@prmVeriTabani", "MusicProject");
            int sonuc = (int)kontrolKomutu.ExecuteScalar();
            if (sonuc == 0)
            {
                SqlCommand olusturma = new SqlCommand(@"create database MusicProject", bgln2);
                olusturma.ExecuteNonQuery();
                string dosya_yolu = @".\sqlOlusturma.sql";
                FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string yazi = sw.ReadToEnd();
                SqlCommand kontrolKomutu2 = new SqlCommand(yazi, bgln2);
                kontrolKomutu2.ExecuteNonQuery();
                bgln.Open();
                SqlCommand verigir = new SqlCommand("insert into kayitlar (Name,Surname,EMail,Password) values ('Admin',' admin',' admin','123456789')", bgln);
                verigir.ExecuteNonQuery();
                SqlCommand verigir2 = new SqlCommand("insert into dosyagoster (ıd,Dosya_Yolu) values ('1',' Bos')", bgln);
                verigir2.ExecuteNonQuery();
                sw.Close();
                fs.Close();
                bgln.Close();
                bgln2.Close();
            }
        }
    }
}
