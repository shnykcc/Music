﻿using System;
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
    public partial class music_save : Form
    {
        public music_save()
        {
            InitializeComponent();
        }
        SqlConnection bgln = new SqlConnection("Data Source = DESKTOP-2E6646U; Initial Catalog = MusicProject; Integrated Security = True");
        //Database'e bağlanmak için bağlantısı oluşturuyorum.
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                //Eğer tüm alnlar doluysa...
                bgln.Open();
                SqlCommand music_save = new SqlCommand("insert into music ([Name],[Mood Name],[Tur Adi],[Artist],[kaydeden],yol) values ('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','" + textBox2.Text + "','" + Giris.kaydeden + "','Kullanıcı kayıt etti')", bgln);
                music_save.ExecuteNonQuery();
                bgln.Close();
                label6.Visible = true;
                label6.Text = textBox1.Text + " Şarkısı kayıt edildi.";
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedItem = "";
                comboBox2.SelectedItem = "";
                //bağlantıyı acıyorum alanlardaki degerleri database'e ekletiyorum ve doğruluğunu kontrol ediyorum. Sorunsuz bir şekilde eklendiyse baglantıyı kapatıyorum. Alanları sıfırlıyorum ve label6'ya başarılı olduğuna dair veri giriyorum.
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Boş alan");
                //Tüm alanlar dolu değilse Tüm alanları doldurunuz hatası veriyorum.
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