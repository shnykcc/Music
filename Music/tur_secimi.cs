using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class tur_secimi : Form
    {
        public static string tur;
        //public static olarak bir degişken tanımlıyorum daha sonra baska formlarda kullancağım için.
        public tur_secimi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tur = "Rap";
            gec();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tur = "Pop";
            gec();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tur = "Jazz";
            gec();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tur = "Klasik";
            gec();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tur = "Rock";
            gec();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tur = "Metal";
            gec();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tur = "Chill";
            gec();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tur = "Trap";
            gec();
        }
        //yukarıdaki button tıklamaları sayesinde tur adlı değişkenin içerisini dolduruyorum ve gec adlı methodu cagırıyorum.
        public void gec()
        {
            Form2 gec = new Form2();
            gec.Show();
            this.Close();
        }

        private void tur_secimi_Load(object sender, EventArgs e)
        {

        }
        //bu method sayesinde bu pencereyi tamamen kapatıyorum ve diğer form'u acıyorum.
    }
}
