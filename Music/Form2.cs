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
        SqlConnection bgln = new SqlConnection("Data Source=DESKTOP-2E6646U;Initial Catalog=MusicProject;Integrated Security=True");
        //sql'e baglanmak için baglantı oluşturuyorum.
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Tekrar Merhaba " + Form1.isim+"!";
            bgln.Open();
            SqlCommand okur = new SqlCommand("select * from kayitlar", bgln);
            SqlDataReader oku = okur.ExecuteReader();
            /*if (oku["Mood Name"].ToString().Trim().ToLower()==uygulama.mood&&oku["Tur Adi"].ToString().Trim().ToLower()==tur_secimi.tur)
            {

            }*/
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
    }
}
