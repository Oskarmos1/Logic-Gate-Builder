using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public partial class VideoForm : Form
    {
        private string videoFileName;
        private string binPath;
        public VideoForm(string vFN, string bP)
        {
            videoFileName = vFN;
            binPath = bP;
            InitializeComponent();
            this.Text = videoFileName;
            AxWindowsMediaPlayer videoPlayer = new AxWindowsMediaPlayer();
            ((ISupportInitialize)(videoPlayer)).BeginInit();
            videoPlayer.Dock = DockStyle.None;

            Controls.Add(videoPlayer);
            ((ISupportInitialize)(videoPlayer)).EndInit();

            videoPlayer.settings.setMode("loop", true);
            videoPlayer.stretchToFit = true;
            videoPlayer.uiMode = "full";
            string path = Path.Combine(Application.StartupPath, binPath, videoFileName);
            videoPlayer.URL = path;

            videoPlayer.OpenStateChange += (s, e) => {
                if (videoPlayer.openState == WMPOpenState.wmposMediaOpen)
                {
                    double scaleFactor = 0;
                    while (videoPlayer.currentMedia.imageSourceWidth * scaleFactor < 540 && videoPlayer.currentMedia.imageSourceHeight * scaleFactor < 960)
                    {
                        Debug.WriteLine(scaleFactor);
                        scaleFactor += 0.01;
                    }

                    videoPlayer.Width = Convert.ToInt16(Math.Floor(videoPlayer.currentMedia.imageSourceWidth * scaleFactor));
                    videoPlayer.Height = Convert.ToInt16(Math.Floor(videoPlayer.currentMedia.imageSourceHeight * scaleFactor));
                    

                    this.Width = videoPlayer.Width + 25;
                    this.Height = videoPlayer.Height + 50;
                }
            };
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Controls.Add(videoPlayer);
        }

        public string getVideoFileName() { 
            return videoFileName;
        }
        public string getBinPath() { 
            return binPath;
        }
    }
}
