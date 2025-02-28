using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _55_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string filePath;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇文字檔或圖片檔";
            ofd.Filter= "Text Files (*.txt)|*.txt|Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                string extension = Path.GetExtension(filePath).ToLower();
                FileInfo fi = new FileInfo(filePath);
                if (extension == ".txt")
                {
                    panel1.Visible = radioButton1.Visible = radioButton2.Visible = true;
                    textBox1.Text = File.ReadAllText(filePath);
                    button4.Visible = textBox1.Visible = true;
                    label2.Visible = label1.Visible = trackBar1.Visible = true;
                    pictureBox1.Visible = false;
                    panel2.Visible = radioButton3.Visible = radioButton4.Visible = radioButton5.Visible = radioButton6.Visible = radioButton7.Visible = false;
                    label3.Text = $"檔案名稱：{fi.Name}\n字元：{fi.Length} bytes\n檔案的建立時間：{fi.CreationTime}\n檔案的上次修改日期：{fi.LastAccessTime}";
                }
                else if (extension == ".jpg" || extension == ".png" || extension == ".bmp")
                {
                    panel1.Visible = radioButton1.Visible = radioButton2.Visible = false;
                    label1.Visible = trackBar1.Visible = true;
                    pictureBox1.Image = Image.FromFile(filePath);
                    button4.Visible = pictureBox1.Visible = true;
                    textBox1.Visible = false;
                    panel2.Visible = radioButton3.Visible = radioButton4.Visible = radioButton5.Visible = radioButton6.Visible = radioButton7.Visible = true;
                    label3.Text = $"檔案名稱：{fi.Name}\n字元：{fi.Length} bytes\n檔案的建立時間：{fi.CreationTime}\n檔案的上次修改日期：{fi.LastAccessTime}";
                }
                else
                {
                    MessageBox.Show("不支援的檔案格式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("上傳失敗", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Visible = panel1.Visible=radioButton1.Visible = radioButton2.Visible = label1.Visible = trackBar1.Visible = button4.Visible = false;
            pictureBox1.Visible = false;
            textBox1.Visible = false;
            panel2.Visible = radioButton3.Visible = radioButton4.Visible = radioButton5.Visible = radioButton6.Visible = radioButton7.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //150-350
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(trackBar1.Value * 10, trackBar1.Value * 10);
            textBox1.Size = new Size(trackBar1.Value * 10, trackBar1.Value * 10);
            label2.Text = $"({trackBar1.Value * 10},{trackBar1.Value * 10})";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.AppendAllText(filePath,textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText(filePath,textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "選擇文字檔或圖片檔";
            ofd.Filter = textBox1.Visible?"Text Files (*.txt)|*.txt": "Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath2 = ofd.FileName;
                string extension = Path.GetExtension(filePath).ToLower();
                if (extension == ".txt" && textBox1.Visible)
                {
                    if (radioButton1.Checked)
                    {
                        File.WriteAllText(filePath, File.ReadAllText(filePath2));
                    }
                    else
                    {
                        File.AppendAllText(filePath, File.ReadAllText(filePath2));
                    }
                }
                else if (extension == ".jpg" || extension == ".png" || extension == ".bmp" && pictureBox1.Visible)
                {
                    try
                    {
                        if (filePath != filePath2)
                        {
                            File.WriteAllBytes(filePath2, File.ReadAllBytes(filePath));
                        }
                    }
                    catch { MessageBox.Show("檔案讀取錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    MessageBox.Show("不支援的檔案格式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("上傳失敗", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
