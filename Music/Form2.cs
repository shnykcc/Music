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
            label1.Text = "Tekrar Merhaba " + Giris.isim + "!";
            Console.WriteLine("Secilen mood= " + mood_secimi.mood + "\nSecilen Tür=" + tur_secimi.tur);
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from music", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            while (oku.Read())
            {
                if (oku["Mood Name"].ToString().Trim() == mood_secimi.mood && oku["Tur Adi"].ToString().Trim() == tur_secimi.tur)
                {
                    Console.WriteLine("Şarkı isimi= " + oku["Name"].ToString().Trim() + "\nArtist İsmi= " + oku["Artist"].ToString().Trim() + "\nLokasyonu= " + oku["yol"].ToString().Trim());
                    listView1.Items.Add(new ListViewItem(new[] { oku["Name"].ToString().Trim(), oku["Artist"].ToString().Trim(), oku["yol"].ToString().Trim() }));
                    //listView1.Items.Add(oku["Name"].ToString().Trim(), oku["Artist"].ToString().Trim(), oku["yol"].ToString().Trim());
                }
                else
                {
                    label2.Text = "Daha Fazla Müzik Ekleyin";
                    listView1.Items.Add(new ListViewItem(new[] { "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı" }));
                }

            }
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            
            bgln.Close();
        }
        //form yüklendiği zaman form1'de tanımladığım isim sayesinde bir karşılama yazısı bastırıyorum ve database ulaşarak tüm kayıtları okuyorum.

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
            if (sonuc==DialogResult.Yes)
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
    }
}
