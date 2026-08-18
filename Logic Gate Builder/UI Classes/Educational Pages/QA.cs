using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Gate_Builder.UI_Classes.Educational_Pages
{
    public class QA
    {
        private PictureBox questionImage;
        private PictureBox answerImage;
        private VideoForm videoForm = null;
        private string name = "";
        public QA(int marks, int qN, int screenWidth) {
            string videoPath = "R/Q/" + marks.ToString() + "M/" + qN.ToString();
            string mainPath = videoPath + "/";
            string qPath = mainPath + "Q.png";
            string aPath = mainPath + "A.png";
            
            name = "qN:" + qN.ToString() + ";marks" + marks.ToString();
            RandomQuestion rq = new RandomQuestion();
            questionImage = new PictureBox
            {

                Image = Image.FromFile(qPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = Image.FromFile(aPath).Width,
                Height = Image.FromFile(aPath).Height,
                Margin = new Padding(10)
            };
            rq.scaleImage(ref questionImage, screenWidth);
            answerImage = new PictureBox
            {

                Image = Image.FromFile(aPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = Image.FromFile(aPath).Width,
                Height = Image.FromFile(aPath).Height,
                Margin = new Padding(10),
                Visible = false
            };
            Debug.WriteLine(aPath);
            rq.scaleImage(ref answerImage, screenWidth);
            if (marks >= 4)
            {
                videoForm = new VideoForm("WS.mp4", videoPath);  
            }
        }
        public PictureBox getQuestion() {
            return questionImage;
        }

        public PictureBox getAnswer() { 
            return answerImage;
        }

        public VideoForm getVideo() {
            return videoForm;
        }

        public string getName() {
            return name;
        }

    }
}
