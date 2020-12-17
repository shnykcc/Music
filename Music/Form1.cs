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

namespace Music
{
    public partial class Form1 : Form
    {
        SqlConnection bgln = new SqlConnection("Data Source = DESKTOP-2E6646U; Initial Catalog = MusicProject; Integrated Security = True");
        public Form1()
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bgln.Open();
            string denetMail = "";
            string denetSifre = "";
            SqlCommand okur = new SqlCommand("select EMail,Password from kayitlar",bgln);
            SqlDataReader oku = okur.ExecuteReader();
            while (oku.Read())
            {
                denetMail = oku["EMail"].ToString().Trim().ToLower();
                if (denetMail == textBox1.Text.ToLower().Trim())
                {
                    denetSifre = oku["Password"].ToString().Trim();
                    if (denetSifre==textBox2.Text)
                    {
                        uygulama frm3 = new uygulama();
                        frm3.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Şifreyi yanlış girdiniz", "Şifre Yanlış", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Maili yanlış girdiniz", "Mail Yanlış", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            bgln.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kayit_Ol frm2 = new Kayit_Ol();
            frm2.Show();
            this.Hide();
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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
