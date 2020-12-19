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
        public tur_secimi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tur = "rap";
            gec();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tur = "pop";
            gec();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tur = "jazz";
            gec();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tur = "klasik";
            gec();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tur = "rock";
            gec();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tur = "metal";
            gec();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tur = "chill";
            gec();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tur = "rap";
            gec();
        }
        public void gec()
        {
            Form2 gec = new Form2();
            gec.Show();
            this.Close();
        }
    }
}
