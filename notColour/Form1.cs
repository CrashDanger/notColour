using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notColour
{
    public partial class Form1 : Form
    {
        private Bitmap workingImage;
        public Form1()
        {
            InitializeComponent();
            OpenButton.Click += OpenButton_Click;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    workingImage = new Bitmap(openDialog.FileName);
                    for (int x = 0; x < workingImage.Width; x++)
                    {
                        for (int y = 0; y < workingImage.Height; y++)
                        {
                            var pixel = workingImage.GetPixel(x, y);
                            int average = (int)(pixel.R*0.21 + pixel.G*0.72 + pixel.B*0.07); // weighted average based on human perception
                            Color BWpixel = Color.FromArgb(average, average, average);
                            workingImage.SetPixel(x,y,BWpixel);
                        }
                    }
                    SaveFileDialog tofinish = new SaveFileDialog();
                    tofinish.Filter += "Same as Original | *" + getExtension(openDialog.FileName);
                    if (tofinish.ShowDialog() == DialogResult.OK)
                    {
                        workingImage.Save(tofinish.FileName);
                        CommunicationBox.Text = "success";
                    } else { CommunicationBox.Text = "failure"; }
                }
            }
        }

        private string getExtension(string path) // couldn't find built in method
        {
            List<char> preresult = new List<char>();
            int l = path.Length - 1;
            for (int i = l; i > 0; i--)
            {
                preresult.Insert(0, path[i]);
                if (path[i] == '.') break;
            }
            return new string(preresult.ToArray());
        }
    }
}
