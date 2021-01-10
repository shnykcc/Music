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
    public partial class MusicPlayer : Form
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void MusicPlayer_Load(object sender, EventArgs e)
        {
            WMPLib.WindowsMediaPlayer muzikcalar = new WMPLib.WindowsMediaPlayer();
            muzikcalar.URL = Form2.musicUrl;
            muzikcalar.controls.play();
        }
    }
}
