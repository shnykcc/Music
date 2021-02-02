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
using System.Net.Mail;

namespace Music
{
    public partial class forgot_password : Form
    {
        SqlConnection bgln = new SqlConnection("Data Source = .; Initial Catalog = MusicProject; Integrated Security = True");
        string gonderilecekMailAdresi = "", kisiIsmi = "";
        bool mailKontrol = false;
        int sifre = 0, saniye = 120, ID;
        /*Değişkenlerimi oluşturuyorum ve database'e bağlantı oluşturuyorum.*/
        public forgot_password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from kayitlar", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            while (oku.Read())
            {
                if (oku["EMail"].ToString().Trim().ToLower() == textBox1.Text.ToLower().Trim())
                {
                    ID = Convert.ToInt32(oku["ID"].ToString().Trim());
                    kisiIsmi = oku["Name"].ToString().Trim().ToLower();
                    mailKontrol = true;
                }
            }
            /*Bağlantıyı acıyorum ve okuma yapıyorum. Okurken eğer istenen mail'e gelirse mailKontrolu true yapıp o kullanıcının ıd'sini vs. tutuyorum ki daha sonra güncellemek
             için kullanacağım.*/
            if (mailKontrol == false)
            {
                MessageBox.Show("Bu mail kayıtlı değil= " + textBox1.Text, "Mail Yok", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*Eğer mail kontrol false olarak kalırsa mail yok demektir.*/
            }
            else if (mailKontrol == true)
            {
                gonderilecekMailAdresi = textBox1.Text;
                textBox1.Visible = false;
                label2.Visible = false;
                button1.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                button2.Visible = true;
                label6.Visible = true;
                mailGonder();
                /*Eğer mail varsa bazı componentleri gizleyip bazılarını acıyorum ve mail gönderiyorum.*/
            }
            mailKontrol = false;
            bgln.Close();
            /*En son cıkmadan önce tekrar mailKontrol false yapıyorum ve bağlantıyı kapatıyorum.*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mailGonder();
            button3.Visible = false;
            /*Tekrar mail göndere basılırsa yeniden mail gönderiyorum. Daha sonra buttonu gizliyorum.*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Giris frm = new Giris();
            frm.Show();
            this.Close();
            /*Geri tuşuna basılırsa bu pencereyi kapatıp girişe gönüyorum*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sifre.ToString() == textBox2.Text)
            {
                if (textBox3.Text.Length < 8)
                {
                    MessageBox.Show("Şifre en az 8 haneli olmalı", "Karakter Sayısı Az", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBox3.Text != textBox4.Text)
                {
                    MessageBox.Show("Şifreler Uyuşmuyor", "Şifreler Aynı Değil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bgln.Open();
                    SqlCommand updatePass = new SqlCommand("update kayitlar SET Password='" + textBox3.Text + "' where ID='" + ID + "'",bgln);
                    updatePass.ExecuteNonQuery();
                    bgln.Close();
                    label7.Text = "Şifre Başarıyla Güncellendi.";
                    label7.Visible = true;
                    timer1.Stop();
                    label6.Visible = false;

                }
            }
            /*Şifre ve kodu denetliyorum. Yapılan yanlış olursa hatalar basıyorum doğruysa database'den güncelliyorum.*/
            else
            {
                MessageBox.Show("Kodu yanlış girdiniz!", "Giriş Kodu Yanlış", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*Kod yanlış ise bunu basıyorum.*/
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = saniye.ToString() + " Saniye";
            saniye--;
            if (saniye == -1)
            {
                button3.Visible = true;
            }
            /*Zamanlayıcı sayesinde 120 saniye dolarsa tekrar mail gönderi aktifleştiriyorum.*/
        }

        public int mailGonder()
        {
            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("musicprojectdeneme@hotmail.com", "159*8753*A");
            smtp.Port = 587;
            smtp.Host = "smtp.live.com";
            smtp.EnableSsl = true;
            Random rnd = new Random();
            email.To.Add(gonderilecekMailAdresi);
            email.From = new MailAddress("musicprojectdeneme@hotmail.com");
            email.Subject = "Şifre Sıfırlama";
            sifre = rnd.Next(100000, 999999);
            email.Body = "Merhaba " + kisiIsmi + "!\nTek kullanımlık şifren aşağıda. Eğer bunu sen yapmadıysan lütfen görmezden gel!\n" + "Tel kullanımlık şifren= " + sifre.ToString();
            smtp.Send(email);
            timer1.Start();
            return 1;
            /*MailMessage nesnesi tanımlıyorum. smpt nesnesi tanımlıyorum. Daha sonra bir email ve şifre veriyorum. SMtp portu veriyorum (TR için 587) Host'u veriyorum
             Bu parametreler bana mail gönderebilmem için gerekli. Sonra random nesnesi belirliyorum. Random nesnesi sayesinde kod göndericem. Daha sonra Kişinin mail
            adresine gidecek şablonu yazıyorum ve kodu'da yazarak gönderiyorum.*/
        }
    }
}
