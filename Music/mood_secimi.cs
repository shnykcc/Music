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
    public partial class mood_secimi : Form
    {
        public mood_secimi()
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
            mood = "Sad";
            gecis();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mood = "Happy";
            gecis();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mood = "Work";
            gecis();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mood = "Spor";
            gecis();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mood = "Meditation";
            gecis();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mood = "Shower";
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
