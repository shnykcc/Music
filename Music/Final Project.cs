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
        WMPLib.WindowsMediaPlayer muzikcalar = new WMPLib.WindowsMediaPlayer();
        //sql'e baglanmak için baglantı oluşturuyorum. Aynı zaman da Windows media player'ı da bir nesne olarak ekliyorum ki onda da müzik caldırabileyim.
        public Form2()
        {
            InitializeComponent();
        }
        public static string musicUrl;
        private void Form2_Load(object sender, EventArgs e)
        {
            button7.BackColor = Color.Gray;
            button7.ForeColor = Color.Red;
            button7.Enabled = false;
            button8.Enabled = true;
            button8.BackColor = Color.Gray;
            button8.ForeColor = Color.Red;
            button8.Enabled = false;
            button10.BackColor = Color.Gray;
            button10.ForeColor = Color.Red;
            button10.Enabled = false;
            button11.BackColor = Color.Gray;
            button11.ForeColor = Color.Red;
            button11.Enabled = false;
            label1.Text = "Tekrar Merhaba " + Giris.isim + "!";
            sarkiListYenile();
        }
        //form yüklendiği zaman form1'de tanımladığım isim sayesinde bir karşılama yazısı bastırıyorum ve sarkiListYenile fonksiyonu çağırıyorum. Bununla beraber Bir de buttonların enabled'larını ayarlıyorum ve renklerini düzenliyorum.

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
            /*Button2'ye basıldığı zaman find_music adlı yeri acıyorum ve burayı kapatıyorum.*/
        }
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
                if (oku["Mood Name"].ToString().Trim() == mood_secimi.mood.Trim() && oku["Tur Adi"].ToString().Trim() == tur_secimi.tur.Trim() && oku["Mood Name"].ToString().Trim() != null && oku["kaydeden"].ToString().Trim() == Giris.kaydeden.ToString().Trim())
                {
                    listView1.Items.Add(new ListViewItem(new[] { oku["Name"].ToString().Trim(), oku["Artist"].ToString().Trim(), oku["yol"].ToString().Trim() }));
                    eklenmisKontrol++;
                    label2.Text = "Moodunuza Ve Sectiğiniz Türe Uygun Müzikler";
                }
                okumaKontrolcu++;
            }
            /*Dataları okurken eğer secilen mood ile okunan mood aynıysa ve secilen tür ile okunan tür aynıysa ve kaydeden ile giriş yapan aynı kişiyse bunu listview
             'a ekliyorum ve label2'nin text'ini değiştiriyorum. Aynı zamanda eklenmişkontrol'u arttırıyorum.*/
            if (eklenmisKontrol == 0)
            {
                label2.Text = "Seciminize uygun şarkı yok";
                listView1.Items.Add(new ListViewItem(new[] { "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı", "Secilen Kriterlere uygun şarkı bulunamadı" }));
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
            /*Eğer eklenmiş herhangi bir yok ise seciminize uygun bir şarkı yok diyorum. Eğer hicbir müzik eklenmediyse daha fazla müzik ekleyin diyorum.*/
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string adres = "https://www.youtube.com/results?search_query=" + listView1.SelectedItems[0].Text;
            System.Diagnostics.Process.Start(adres);
            /*Youtube'da aramak için adres değişkeni aratıyorum ve browser'da bu değişkenle adrese gidiyorum.*/
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
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
                if (listView1.SelectedItems[0].SubItems[2].Text.ToString() != "Kullanici kayit etti")
                {
                    button8.Enabled = true;
                    button8.BackColor = Color.Purple;
                    button8.ForeColor = Color.Black;
                    button10.Enabled = true;
                    button10.BackColor = Color.Purple;
                    button10.ForeColor = Color.Black;
                    button11.Enabled = true;
                    button11.BackColor = Color.Purple;
                    button11.ForeColor = Color.Black;
                }
                else if (listView1.SelectedItems[0].SubItems[2].Text == "Kullanici kayit etti")
                {
                    button8.BackColor = Color.Gray;
                    button8.ForeColor = Color.Red;
                    button8.Enabled = false;
                    button10.BackColor = Color.Gray;
                    button10.ForeColor = Color.Red;
                    button10.Enabled = false;
                    button11.BackColor = Color.Gray;
                    button11.ForeColor = Color.Red;
                    button11.Enabled = false;
                }
            }
            /*Listview'da secim değişimi yapıldığında parametrelere göre buttonların renklerini ve erişilebilirliklerini değiştiriyorum. */
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bgln.Open();
            SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='Bos' WHERE ıd=1", bgln);
            dosyaYoluDegis.ExecuteNonQuery();
            bgln.Close();
            /*Eğer dosya yolu sıfırlanmak isterse ilgili sql alanına ulaşarak burayı ilk haline sıfırlıyorum.*/
        }

        private void button8_Click(object sender, EventArgs e)
        {

            muzikcalar.URL = stringReplace(listView1.SelectedItems[0].SubItems[2].ToString().Substring(18, listView1.SelectedItems[0].SubItems[2].ToString().Length - 19));
            muzikcalar.controls.play();
            listView1.SelectedItems.Clear();
            /*Bilgisayarda calınmak isterse stringreplace ile _ leri boşluk yapıyorum ve url olarak alıyorum ve musicplayer'da çalıyorum.*/
        }

        private void button11_Click(object sender, EventArgs e)
        {
            muzikcalar.controls.stop();
            muzikcalar.controls.play();
            listView1.SelectedItems.Clear();
            /*Yeniden başlata tıklanırsa durdurup yeniden başlatıyorum.*/
        }

        private void button10_Click(object sender, EventArgs e)
        {
            muzikcalar.controls.pause();
            listView1.SelectedItems.Clear();
            /*Durdur'a basılırsa durduruyorum.*/
        }
        public string stringReplace(string text)
        {
            text = text.Replace("_", " ");
            return text;
            /*_ leri boşluk olarak düzeltiyorum.*/
        }
    }
}
