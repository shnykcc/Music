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
    public partial class uygulama : Form
    {
        public uygulama()
        {
            InitializeComponent();
        }
        public static string mood;
        private void uygulama_Load(object sender, EventArgs e)
        {
            label1.Text = "Hoşgeldiniz " + Giris.isim;
            //Uygulama acıldığı zaman form1'de Database'den cektiğim isimle birlikte bir karşılama yazısı yazıyorum.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mood = "huzunlu";
            gecis();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mood = "mutlu";
            gecis();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mood = "calisma";
            gecis();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mood = "spor";
            gecis();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mood = "meditasyon";
            gecis();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mood = "dus";
            gecis();
        }
        //Yukarıdaki fonksiyonlar sayesinde uygulamanın içerisine girdiğim zaman hangi tür müziğin ön plana cıkacağını ayarlama parametrelerini alıyorum. gecis adlı fonksiyonu da her birinde kullanıyorum.
        public void gecis()
        {
            tur_secimi gec = new tur_secimi();
            gec.Show();
            this.Close();
        }
        //Gecis fonksiyonu tanımlıyorum ve diğer forma gecmeyi yapnış oluyorum. Fonksiyon tanımlayarakda fazladan kod satırı olmasını engelliyorum.
    }
}
