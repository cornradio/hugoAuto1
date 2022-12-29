using hugoAuto1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hugoAuto1
{
    public partial class FormArticles : Form
    {
        public FormArticles()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.BackColor = Color.White;
            //string rawpath = textBox1.Text;
            //string filePath = $@"{rawpath}\content\zh-cn\posts";
            string filePath = Settings.Default.articles;
            listBox1.Items.Clear();
            var files = Directory
              .GetFiles(filePath, "*.md");
            //提取路径地址+/为了在后面把完整路径剔除
            string pathstr = filePath + "\\";
            string pathstr2 = ".md";
            int count = 0;
            foreach (var file in files)
            {
                //逐个把文件名放在combox中
                listBox1.Items.Add(file.ToString().Replace(pathstr, "").Replace(pathstr2, ""));
                count++;
            }
        }
    }
}
