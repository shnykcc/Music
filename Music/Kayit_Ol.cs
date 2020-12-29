using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Music
{
    public partial class Kayit_Ol : Form
    {
        int onay = 0, maildenetim = 0, sure = 120;
        string gonderilecekMailAdresi = "", kisiIsımi = "";
        SqlConnection bgln = new SqlConnection("Data Source = .; Initial Catalog = MusicProject; Integrated Security = True");
        public Kayit_Ol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool devam = true;
            if (textBox1.Text != null && textBox2.Text != null && textBox3.Text != null && textBox4.Text != null)
            {
                bgln.Open();
                SqlCommand okur = new SqlCommand("select EMail,Password from kayitlar", bgln);
                SqlDataReader oku = okur.ExecuteReader();
                while (oku.Read())
                {
                    if (oku["EMail"].ToString().Trim().ToLower() == textBox3.Text.Trim().ToLower())
                    {
                        MessageBox.Show("Bu Mail kullanımda!", "Mail Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        devam = false;
                    }
                }
                bgln.Close();
                if (devam == true)
                {
                    if (textBox4.Text.Length <= 7)
                    {
                        MessageBox.Show("Parolanızın 8 haneli olmalıdır", "Parola Uygun gereksinimleri karşılamıyor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                    else if (textBox4.Text != textBox5.Text)
                    {
                        MessageBox.Show("Şifreler aynı olmak zorunda!", "Şifre Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                    else
                    {
                        gonderilecekMailAdresi = textBox3.Text.Trim();
                        kisiIsımi = textBox1.Text.Trim();
                        maildenetim = mailGonder();
                        label1.Visible = false;
                        checkBox1.Visible = false;
                        label2.Visible = false;
                        label3.Visible = false;
                        label4.Visible = false;
                        label5.Visible = false;
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        textBox4.Visible = false;
                        textBox5.Visible = false;
                        button1.Visible = false;
                        label7.Visible = true;
                        label6.Visible = true;
                        textBox6.Visible = true;
                        button3.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Boş Alan");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Giris frm1 = new Giris();
            frm1.Show();
            this.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = '\0';
                textBox5.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
                textBox5.PasswordChar = '*';
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
            email.Subject = "Onay Postası";
            onay = rnd.Next(100000, 999999);
            email.Body = "Merhaba " + kisiIsımi + "!\nAramıza Hoşgeldin! Devam etmek için lütfen onay kodunu doğru yere gir!\n" + "Onay kodunuz= " + onay.ToString();
            smtp.Send(email);
            timer1.Start();
            return 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mailGonder();
            sure = 120;
            button4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (onay.ToString() == textBox6.Text)
            {
                progressBar1.Value += 100;
                label8.Text = "Kayıt Başarıyla gercekleşti.\nAna menüye aktarılıyor.";
                System.Threading.Thread.Sleep(1000);
                if (maildenetim == 1)
                {
                    bgln.Open();
                    SqlCommand hesap_ekle = new SqlCommand("insert into kayitlar (Name,Surname,EMail,Password) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')", bgln);
                    hesap_ekle.ExecuteNonQuery();
                    Giris frm1 = new Giris();
                    frm1.Show();
                    bgln.Close();
                    this.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = sure.ToString() + " Saniye";
            sure--;
            if (sure == -1)
            {
                timer1.Stop();
                button4.Visible = true;
            }
        }
    }
}
