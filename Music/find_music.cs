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
        SqlConnection bgln = new SqlConnection("Data Source=.;Initial Catalog=MusicProject;Integrated Security=True");
        string[] dosyalar;
        string adres;
        string[] sqlyolları;
        int sayac = 0,nextSong = 0;
        public find_music()
        {
            InitializeComponent();
        }

        private void find_music_Load(object sender, EventArgs e)
        {
            string kontrol = "";
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from dosyagoster where ıd=1", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            if (oku.Read())
            {
                kontrol = oku["Dosya_Yolu"].ToString().Trim();
            }
            bgln.Close();
            bgln.Open();
            SqlCommand okur2 = new SqlCommand("select * from music ", bgln);
            SqlDataReader oku2 = okur2.ExecuteReader();
            while (oku2.Read())
            {
                sayac++;
            }
            sqlyolları = new string[sayac];
            sayac = 0;
            while (oku2.Read())
            {
                sqlyolları[sayac] = oku2["yol"].ToString().Trim();
                sayac++;
            }
            foreach (var i in sqlyolları)
            {
                Console.WriteLine(i);
            }
            bgln.Close();
            if (kontrol == "Bos")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog().ToString();
                adres = fbd.SelectedPath;
                bgln.Open();
                SqlCommand verigir = new SqlCommand("UPDATE dosyagoster SET [Dosya_Yolu]='" + adres.ToString() + "' WHERE ıd=1", bgln);
                verigir.ExecuteNonQuery();
                bgln.Close();
            }
            else
            {
                bgln.Open();
                SqlCommand vericek = new SqlCommand("select Dosya_Yolu from dosyagoster where ıd=1", bgln);
                SqlDataReader vericekoku = vericek.ExecuteReader();
                while (vericekoku.Read())
                {
                    adres = vericekoku["Dosya_Yolu"].ToString().Trim();
                }
                bgln.Close();
            }
            dosyalar = new string[adres.Length];
            dosyalar = Directory.GetFiles(adres);
            sıradakiSarki();
        }
        public void sıradakiSarki()
        {
            for (int i = 0; i < sqlyolları.Length-1; i++)
            {
                if (dosyalar[nextSong].Trim() == sqlyolları[i].Trim())
                {
                    nextSong++;
                    i = 0;
                }
            }  
            textBox1.Text = dosyalar[nextSong].ToString().Substring(9, dosyalar[nextSong].Length - 13);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != null && comboBox1.SelectedItem != null && comboBox1.SelectedItem != null)
            {
                bgln.Open();
                SqlCommand verigirisi = new SqlCommand("insert into music (Name,[Mood Name],[Tur Adi],Artist,kaydeden,yol) values ('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','" + textBox2.Text + "','" + Giris.kaydeden + "','" + dosyalar[nextSong] + "')", bgln);
                verigirisi.ExecuteNonQuery();
                bgln.Close();
                sıradakiSarki();
            }
            else
            {
                MessageBox.Show("Boş Alanları Doldurunuz", "Bos Alan Var", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }
    }
}
