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
    public partial class Form2 : Form
    {
        SqlConnection bgln = new SqlConnection("Data Source=.;Initial Catalog=MusicProject;Integrated Security=True");
        //sql'e baglanmak için baglantı oluşturuyorum.
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button7.BackColor = Color.Gray;
            button7.ForeColor = Color.Red;
            button7.Enabled = false;
            label1.Text = "Tekrar Merhaba " + Giris.isim + "!";
            sarkiListYenile();
        }
        //form yüklendiği zaman form1'de tanımladığım isim sayesinde bir karşılama yazısı bastırıyorum ve sarkiListYenile fonksiyonu çağırıyorum.

        private void button1_Click(object sender, EventArgs e)
        {
            music_save msc = new music_save();
            msc.Show();
            this.Close();
            //button 1'e basıldığı zaman bu pencereyi tamamen kapatıp mzüik kaydetme yerini acıyorum.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            find_music fnd = new find_music();
            fnd.Show();
            this.Close();
        }
        //button 2'e basıldığı zaman bu pencereyi tamamen kapatıp mzüik bulma yerini acıyorum.
        private void button3_Click(object sender, EventArgs e)
        {
            mood_secimi frm = new mood_secimi();
            frm.Show();
            this.Close();
        }
        /*Button 3'e basınca mood secime geri dönüyorum.*/
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Kullanıcı Değiştirmek İstediğinize Emin misiniz?", "Kullanıcı Değiştir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (sonuc == DialogResult.Yes)
            {
                Giris frm = new Giris();
                frm.Show();
                this.Close();
            }
        }
        /*Button 4'e basınca Kullanıcı değiştirmek için giriş ekranına geri dönüyorum.*/
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Uygulamayı Kapatmak İstediğinize Emin misiniz?", "Uygulamayı Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (sonuc == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
        /*Button5'e tıklayınca application'ı direk kapatıyorum.*/
        private void button6_Click(object sender, EventArgs e)
        {
            sarkiListYenile();
        }
        public void sarkiListYenile()
        {
            listView1.Items.Clear();
            //İlk başta eğer item varsa üst üste binmemesi için 0'lıyorum.
            int okumaKontrolcu = 0, eklenmisKontrol = 0;
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from music", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            while (oku.Read())
            {
                if (oku["Mood Name"].ToString().Trim() == mood_secimi.mood && oku["Tur Adi"].ToString().Trim() == tur_secimi.tur && oku["Mood Name"].ToString().Trim() != null && oku["kaydeden"].ToString().Trim() == Giris.kaydeden2.ToString())
                {
                    listView1.Items.Add(new ListViewItem(new[] { oku["Name"].ToString().Trim(), oku["Artist"].ToString().Trim(), oku["yol"].ToString().Trim() }));
                    eklenmisKontrol++;
                    label2.Text = "Moodunuza Ve Sectiğiniz Türe Uygun Müzikler";
                }
                else if (eklenmisKontrol == 0)
                {
                    label2.Text = "Seciminize uygun şarkı yok";
                    listView1.Items.Add(new ListViewItem(new[] { "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı" }));
                }
                okumaKontrolcu++;
            }
            if (okumaKontrolcu == 0 && eklenmisKontrol == 0)
            {
                label2.Text = "Daha Fazla Müzik Ekleyin";
                listView1.Items.Add(new ListViewItem(new[] { "EKLENEN HERHANGİ BİR ŞARKI YOK.", "EKLENEN HERHANGİ BİR ŞARKI YOK.", "EKLENEN HERHANGİ BİR ŞARKI YOK." }));
            }
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            bgln.Close();
        }
        //fonskiyon cağrıldığı zaman database ulaşarak tüm kayıtları okuyorum. Eğer kayıt yoksa ceşitli yazılarla row ekliyorum.

        private void button7_Click(object sender, EventArgs e)
        {
            string adres = "https://www.youtube.com/results?search_query=" + listView1.SelectedItems[0].Text;
            System.Diagnostics.Process.Start(adres);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                button7.Enabled = true;
                button7.BackColor = Color.Purple;
                button7.ForeColor = Color.Black;
            }
            else
            {
                button7.BackColor = Color.Gray;
                button7.ForeColor = Color.Red;
                button7.Enabled = false;
            }
        }
    }
}
