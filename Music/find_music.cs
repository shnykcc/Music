using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace Music
{
    public partial class find_music : Form
    {
        SqlConnection bgln = new SqlConnection("Data Source =.; Initial Catalog = MusicProject; Integrated Security = True");
        int kontrolSayac = 0;
        string adres = "";
        string girilecekAdres = "";
        List<string> dosyalar = new List<string>();
        List<string> dosyaYolları = new List<string>();
        /*Burada bir baglantı oluşturuyorum ve değişkenlerimi oluşturdum. İsimlerini acıklayıcı yapmaya calıştım. Daha sonra dosyalarımı ve dosyamın yollarını liste halinde tutmak için listeler oluşturdum.*/
        public find_music()
        {
            InitializeComponent();
        }

        public void find_music_Load(object sender, EventArgs e)
        {
            string dosya_yolu = "";
            bgln.Open();
            SqlCommand dosyaYoluOku = new SqlCommand("select Dosya_Yolu from dosyagoster where ıd=1", bgln);
            SqlDataReader oku = dosyaYoluOku.ExecuteReader();
            while (oku.Read())
            {
                dosya_yolu = oku["Dosya_Yolu"].ToString().Trim();
            }
            bgln.Close();
            /*Sayfa yüklendiğinde dosya yolu adında bir değişken oluşturdum ve baglantıyı acarak Dosya yolunu sqlden cektim ve baglantıyı kapadım.*/
            if (dosya_yolu == "Bos")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                bgln.Open();
                SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='" + fbd.SelectedPath + "' WHERE ıd=1", bgln);
                dosyaYoluDegis.ExecuteNonQuery();
                adres = fbd.SelectedPath;
                bgln.Close();
            }
            else
            {
                bgln.Open();
                SqlCommand dosyaYoluCek = new SqlCommand("select Dosya_Yolu from dosyagoster where ıd=1", bgln);
                SqlDataReader okur = dosyaYoluCek.ExecuteReader();
                while (okur.Read())
                {
                    adres = okur["Dosya_Yolu"].ToString().Trim();
                }
                bgln.Close();
            }
            /*Eğer dosya yolu Bos ise (ilk db'yi oluştururken o şekilde kodladım.) direk dosya yolu secme ekranını acıyor ve secilen dosya yolunu db'ye ekliyor.Aksi halde bos değilse direk içerideki dosyanın yolunu cekiyor.*/
            string[] kopyalanacak = new string[9999];
            kopyalanacak = Directory.GetFiles(adres);
            for (int i = 0; i < kopyalanacak.Length; i++)
            {
                if (kopyalanacak[i] != null && kopyalanacak[i].Substring(kopyalanacak[i].Length - 3, 3) == "mp3")
                {
                    dosyalar.Add(stringReplace(kopyalanacak[i]));
                }
            }
            for (int i = 0; i < dosyalar.Count; i++)
            {
                dosyaYolları.Add(dosyalar[i].Substring(9, dosyalar[i].Length - 13));
            }
            bgln.Close();
            changeSongName();
            /*Kopyalanacak adında bir arry oluşturdum ve 999'luk bir büyüklük sectim. Bunu yapmamın sebebi dosyaların ne kadarlık bir büyüklüğe sahip olacağını bilmememdi. Daha sonra dosyaları bu arry'in içerisine doldurdum. Sonra bu arry'in büyüklüğü kadar dönecek for döngüsü yaptım ve eğer sırası gelen arry boş değilse ve sonu mp3 ile bitiyorsa bunu dosyalar ismindeki listeme ekledim. Daha sonra dosyaların büyüklüğü kadar bir for döngüsü ile dosyayollarını doldurmaya başladım bunu yaparken başta yazan c d gibi ifadeleri ve sondaki .mp3 'ü sildim. En son changeSongName adlı kendi oluşturuduğum fonksiyonu çağırdım. */
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && girilecekAdres != null)
            {
                bgln.Open();
                SqlCommand musicGir = new SqlCommand("insert into music (Name,[Mood Name],[Tur Adi],Artist,kaydeden,yol) values('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','" + textBox2.Text + "','" + Giris.kaydeden + "','" + girilecekAdres + "')", bgln);
                musicGir.ExecuteNonQuery();
                bgln.Close();
            }
            else if (girilecekAdres == "")
            {
                MessageBox.Show("Şarkı yolu yok!", "Şarkı Yolu Bulunamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Boş alan var!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            changeSongName();
            /*Kayıt et buttonuna basınca eğer tüm alanlar doluysa baglantıyı acıyorum db'e kayıt ediyorum ve tekrardan kapatıyorum. EĞer girilecek adresim boş ise (herhangi bir dan dolayı bunu ekrana basıyorum yada alanlardan herhangi biri boş ise bunu doldurunuz diye söylüyorum ve changeSongName isimli fonksiyonu cağırıyorum.)*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            bgln.Open();
            SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='" + fbd.SelectedPath + "' WHERE ıd=1", bgln);
            dosyaYoluDegis.ExecuteNonQuery();
            adres = fbd.SelectedPath;
            bgln.Close();
            string[] kopyalanacak = new string[9999];
            kopyalanacak = Directory.GetFiles(adres);
            dosyalar.Clear();
            for (int i = 0; i < kopyalanacak.Length; i++)
            {
                if (kopyalanacak[i] != null && kopyalanacak[i].Substring(kopyalanacak[i].Length - 3, 3) == "mp3")
                {
                    dosyalar.Add(stringReplace(kopyalanacak[i]));
                }
            }
            for (int i = 0; i < dosyalar.Count; i++)
            {
                dosyaYolları.Add(dosyalar[i].Substring(9, dosyalar[i].Length - 13));
            }
            bgln.Close();
            changeSongName();
            /*Eğer müzik klasörü değiştire basılırsa yine klasör secme ekranını acıyorum klasör secimini db'ye kayıt ediyorum bağlantıyı kapatıyorum ve kopyalanacak adında arry oluşturuyorum (nedenlerini yukarıda acıkladım).dosyaların adını cekiyorum daha önce doluysa diye listeyi temizliyorum. Daha sonra sonu mp3 ise ve boş değilse dosyalar adlı listeme ekliyorum. dosyayollarını c d gibi başta yer alan lokasyonları ve sondaki mp3 yazısını kaldırarak dosyayollarına ekliyorum ve baglantıyı kapatıp changesongname adlı fonksiyonu cağırıyorum*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
            /*Geri tuşuna basılırsa ekranı kapatıp bir önceki formu acıyorum.*/
        }
        public void changeSongName()
        {
            int sayac = 0, girdiSayac = 0;
            bool yolKontrolu = false;
            bgln.Open();
            SqlCommand sqlsayi = new SqlCommand("select count(yol) as counter from music", bgln);
            SqlDataReader oku = sqlsayi.ExecuteReader();
            while (oku.Read())
            {
                sayac = Convert.ToInt32(oku["counter"]);
            }
            bgln.Close();
            /*Fonksiyon çağırılınca değişkenleri oluşturuyorum ve baglantıyı acıp kac adet sql yolunun db'de kayıt edildiğine bakıyorum. ve bunu sayac adlı değişkenime aktarıyorum.*/
            string[] sqlyollarim = new string[sayac];
            bgln.Open();
            SqlCommand sqlyollari = new SqlCommand("select yol from music", bgln);
            SqlDataReader oku2 = sqlyollari.ExecuteReader();
            while (oku2.Read())
            {
                sqlyollarim[girdiSayac] = oku2["yol"].ToString().Trim();
                sqlyollarim[girdiSayac] = stringReplace(sqlyollarim[girdiSayac]);
                girdiSayac++;
            }
            /*sqlyollarım adlı arry oluşturuyorum ve önceden cektiğim sayac adlı int yardımı ile büyüklüğünü belirliyorum. baglantıyı acıyorum ve yolların hepsini kendi arry'ime türkce karakterlerden arındırarak ekliyorum */
            bool SayacKontrolu = false;
            bgln.Close();
            while (SayacKontrolu == false)
            {
                Console.WriteLine("Dosya sayısı= " + dosyalar.Count);
                for (int j = 0; j < sqlyollarim.Length; j++)
                {
                    if (dosyalar[kontrolSayac] == sqlyollarim[j])
                    {
                        kontrolSayac++;
                        j = 0;
                    }
                    if (kontrolSayac >= dosyalar.Count)
                    {
                        MessageBox.Show("Burada eklenmeyen müzik yok lütfen farklı bir klasör sec", "Klasör sec", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.ShowDialog();
                        bgln.Open();
                        SqlCommand dosyaYoluDegis = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='" + fbd.SelectedPath + "' WHERE ıd=1", bgln);
                        dosyaYoluDegis.ExecuteNonQuery();
                        adres = fbd.SelectedPath;
                        bgln.Close();
                        string[] kopyalanacak = new string[9999];
                        kopyalanacak = Directory.GetFiles(adres);
                        dosyalar.Clear();
                        for (int i = 0; i < kopyalanacak.Length; i++)
                        {
                            if (kopyalanacak[i] != null && kopyalanacak[i].Substring(kopyalanacak[i].Length - 3, 3) == "mp3")
                            {
                                dosyalar.Add(stringReplace(kopyalanacak[i]));
                            }
                        }
                        if (dosyalar.Count==0)
                        {
                            bgln.Close();
                            bgln.Open();
                            SqlCommand dosyaYoluDegis2 = new SqlCommand("UPDATE dosyagoster SET Dosya_Yolu='Bos' WHERE ıd=1", bgln);
                            dosyaYoluDegis2.ExecuteNonQuery();
                            bgln.Close();
                            MessageBox.Show("İçeride müzik yok", "Müzik bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        for (int i = 0; i < dosyalar.Count; i++)
                        {
                            dosyaYolları.Add(dosyalar[i].Substring(9, dosyalar[i].Length - 13));
                        }
                        bgln.Close();
                        changeSongName();
                    }
                    /*Yukarıdaki fonksiyon ile eğer eklenmeyen dosya kalmadıysa klasör değişikliği yaptırıyorum.*/
                }
                SayacKontrolu = true;
            }
            textBox1.Text = dosyalar[kontrolSayac].Substring(9, dosyalar[kontrolSayac].Length - 13);
            girilecekAdres = dosyalar[kontrolSayac];
            SayacKontrolu = false;
            yolKontrolu = false;
            kontrolSayac++;
            bgln.Close();
            /*bool bir ifade oluştuyuroum ve eğer o false ise sqlyollarımın hepsini şuanki dosyam ile karşılaştırıyorum eğer eşit ise bu o dosyanın daha önceden eklendiği anlamına gelir. Bu yüzden j'yi 0 lıyorum ve sonraki dosyaya gecmej icin kontrolsayacı arttırıyorum. Eğer sql yollarından hicbiri eşit değişse sayackontrolu true yapıp o döngüden cıkıyorum ve ismini textbox1'e yazarak girilecek adreside atıyorum daha sonra kullanmak için sayackontrolu ve yol kontrolu false'a cekiyorum kontrol sayacımı 1 arttıroyurm ve baglantıyı kapatıyorum.*/
        }
        public string stringReplace(string text)
        {
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            text = text.Replace(" ", "_");
            text = text.Replace("Ã", "U");
            text = text.Replace("?", "_");
            return text;
        }
        /*Yukarıdaki fonksiyon ile Türkce karakter sorununun önüne geciyorum. Bu sayede program hata vermiyor.*/
    }
}
