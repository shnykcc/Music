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
            bgln.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            music_save msc = new music_save();
            msc.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            find_music fnd = new find_music();
            fnd.Show();
            this.Close();
        }
    }
}
