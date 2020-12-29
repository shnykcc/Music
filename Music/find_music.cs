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
        SqlConnection bgln = new SqlConnection("Data Source =.; Initial Catalog = MusicProject; Integrated Security = True");
        string adres = "";
        string girilecekAdres = "";
        string[] dosyaYolları;
        string[] dosyalar;
        public find_music()
        {
            InitializeComponent();
        }

        private void find_music_Load(object sender, EventArgs e)
        {
            string dosya_yolu = "";
            bgln.Open();
            SqlCommand dosyaYoluOku = new SqlCommand("select Dosya_Yolu from dosyagoster where ıd=1", bgln);
            SqlDataReader oku = dosyaYoluOku.ExecuteReader();
            while (oku.Read())
            {
                dosya_yolu = oku["Dosya_Yolu"].ToString().Trim();
            }
            bgln.Close();
            if (dosya_yolu == "Bos")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                bgln.Open();
                SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='" + fbd.SelectedPath + "' WHERE ıd=1", bgln);
                dosyaYoluDegis.ExecuteNonQuery();
                adres = fbd.SelectedPath;
                bgln.Close();
            }
            else
            {
                bgln.Open();
                SqlCommand dosyaYoluCek = new SqlCommand("select Dosya_Yolu from dosyagoster where ıd=1", bgln);
                SqlDataReader okur = dosyaYoluCek.ExecuteReader();
                while (okur.Read())
                {
                    adres = okur["Dosya_Yolu"].ToString().Trim();
                }
                bgln.Close();
            }
            dosyalar = Directory.GetFiles(adres);
            for (int i = 0; i < dosyalar.Length; i++)
            {
                dosyaYolları[i] = dosyalar[i];
                dosyalar[i] = stringReplace(dosyalar[i].Substring(9, dosyalar[i].Length - 13));
            }
            foreach (var i in dosyalar)
            {
                Console.WriteLine(i);
            }
            bgln.Close();
            changeSongName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && girilecekAdres != null)
            {
                bgln.Open();
                SqlCommand musicGir = new SqlCommand("insert into music (Name,[Mood Name],[Tur Adi],Artist,kaydeden,yol) values('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','" + textBox2.Text + "','" + Giris.kaydeden + "','" + girilecekAdres + "')", bgln);
                musicGir.ExecuteNonQuery();
                bgln.Close();
            }
            else if (girilecekAdres == null)
            {
                MessageBox.Show("Şarkı yolu yok!", "Şarkı Yolu Bulunamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Boş alan var!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            changeSongName();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            bgln.Open();
            SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='" + fbd.SelectedPath + "' WHERE ıd=1", bgln);
            dosyaYoluDegis.ExecuteNonQuery();
            adres = fbd.SelectedPath;
            bgln.Close();
            dosyalar = Directory.GetFiles(adres);
            changeSongName();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }
        public void changeSongName()
        {
            int sayac = 0, girdiSayac = 0, dosyasayac = 0;
            bgln.Open();
            SqlCommand sqlsayi = new SqlCommand("select count(yol) as counter from music", bgln);
            SqlDataReader oku = sqlsayi.ExecuteReader();
            while (oku.Read())
            {
                sayac = Convert.ToInt32(oku["counter"]);
            }
            bgln.Close();
            string[] sqlyollarim = new string[sayac];
            bgln.Open();
            SqlCommand sqlyollari = new SqlCommand("select yol from music", bgln);
            SqlDataReader oku2 = sqlyollari.ExecuteReader();
            while (oku2.Read() && oku2["yol"] != null)
            {
                sqlyollarim[girdiSayac] = oku2["yol"].ToString().Trim();
                sqlyollarim[girdiSayac] = stringReplace(sqlyollarim[girdiSayac]);
                girdiSayac++;
            }
            bool yolKontrol = false;
            for (int i = 0; i < dosyalar.Length; i++)
            {
                for (int j = 0; j < sqlyollarim.Length; j++)
                {
                    if (sqlyollarim[j] == dosyaYolları[i])
                    {
                        yolKontrol = true;
                    }
                }
                if (yolKontrol==false)
                {
                    textBox1.Text = dosyalar[i];
                }
            }
            bgln.Close();
        }
        public string stringReplace(string text)
        {
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            text = text.Replace(" ", "_");
            text = text.Replace("Ã", "U");
            text = text.Replace("?", "_");
            return text;
        }
    }
}
