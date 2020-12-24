using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace Music
{
    public partial class find_music : Form
    {
        SqlConnection bgln = new SqlConnection("Data Source=DESKTOP-2E6646U;Initial Catalog=MusicProject;Integrated Security=True");
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        FileInfo[] Files;
        bool kontrolcu = false;
        string[] dosyalar;
        string[] yollar;
        int sayac = 0,yolsayac=0;
        public find_music()
        {
            InitializeComponent();
        }

        private void find_music_Load(object sender, EventArgs e)
        {
            
            string kontrol = "";
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from dosyagoster", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            if (oku.Read())
            {
                kontrol = oku["Dosya_Yolu"].ToString().Trim();
            }
            if (kontrol == "Bos")
            {
                string adres = @"D:\Music";
                dosyalar = Directory.GetFiles(adres);
                textBox1.Text = dosyalar[0].ToString().Substring(9, dosyalar[0].Length - 13);
                /*if (dosyalar[sayac].Substring(dosyalar[sayac].Length - 3, 3).ToString() == "mp3")
                {
                    textBox1.Text = dosyalar[sayac].ToString().Substring(9, dosyalar[sayac].Length - 13);
                }*/
            }
            bgln.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != null && textBox2.Text != null && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                bgln.Open();
                SqlCommand gir = new SqlCommand("insert into music (Name,[Mood Name],[Tur Adi],Artist,kaydeden,yol) values ('"+textBox1.Text+"','"+comboBox1.SelectedItem+"','"+comboBox2.SelectedItem+"','"+textBox2.Text+"','"+Form1.kaydeden+"','"+dosyalar[sayac]+"')", bgln);
                gir.ExecuteNonQuery();
                SqlCommand okur = new SqlCommand("select * from music", bgln);
                SqlDataReader oku = okur.ExecuteReader();
                while (oku.Read())
                {
                    yollar[yolsayac] = oku["yol"].ToString().Trim().ToLower();
                    yolsayac++;
                }
            }
            sayac++;
            if (sayac<=dosyalar.Length)
            {
                for (int i = 0; i < yollar.Length; i++)
                {
                    if (yollar[i]==dosyalar[sayac])
                    {
                        kontrolcu = true;
                    }
                }
                if (kontrolcu==false)
                {
                    textBox1.Text = dosyalar[sayac].ToString().Substring(9, dosyalar[sayac].Length - 13);
                }
                else
                {
                    sayac++;
                }
            }
            else
            {
                label6.Visible = true;
                label6.Text = "Başka Şarkı kalmadı!";
                Thread.Sleep(1000);
                button2_Click(sender, e);
            }
            bgln.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }
    }
}
