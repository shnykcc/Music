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
                Console.WriteLine("Mail) " + oku["EMail"].ToString().Trim().ToLower());
                if (oku["EMail"].ToString().Trim().ToLower() == textBox1.Text.ToLower().Trim())
                {
                    ID = Convert.ToInt32(oku["ID"].ToString().Trim());
                    kisiIsmi = oku["Name"].ToString().Trim().ToLower();
                    mailKontrol = true;
                }
            }
            if (mailKontrol == false)
            {
                MessageBox.Show("Bu mail kayıtlı değil= " + textBox1.Text, "Mail Yok", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            mailKontrol = false;
            bgln.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mailGonder();
            button3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Giris frm = new Giris();
            frm.Show();
            this.Close();
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
                    SqlCommand updatePass = new SqlCommand("update kayitlar SET Password='" + textBox3.Text + "' where ID='" + ID + "'");
                    bgln.Close();
                    label7.Text = "Şifre Başarıyla Güncellendi.";
                    label7.Visible = true;
                    timer1.Stop();
                    label6.Visible = false;

                }
            }
            else
            {
                MessageBox.Show("Kodu yanlış girdiniz!", "Giriş Kodu Yanlış", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
    }
}
