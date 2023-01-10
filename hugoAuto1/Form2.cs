using hugoAuto1.Properties;
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

namespace hugoAuto1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            reload_textbox();
        }

        private void reload_textbox()
        {
            textBox1.Text = Settings.Default.chromeCommand.ToString();
            textBox2.Text = Settings.Default.typoraCommand.ToString();
            textBox3.Text = Settings.Default.vscCommand.ToString();
            textBox4.Text = Settings.Default.githubCommand.ToString();
            textBox5.Text = Settings.Default.hugotheme.ToString();
            textBox6.Text = Settings.Default.gitpull_cmd.ToString();
            textBox7.Text = Settings.Default.gitpush_cmd.ToString();
            textBox8.Text = Settings.Default.gitproxy_cmd.ToString();
            textBox_username.Text = Settings.Default.username.ToString();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            Settings.Default.chromeCommand = textBox1.Text;
            Settings.Default.typoraCommand = textBox2.Text;
            Settings.Default.vscCommand = textBox3.Text;
            Settings.Default.githubCommand = textBox4.Text;
            Settings.Default.hugotheme = textBox5.Text;
            Settings.Default.gitpull_cmd = textBox6.Text;
            Settings.Default.gitpush_cmd = textBox7.Text;
            Settings.Default.gitproxy_cmd = textBox8.Text;
            Settings.Default.username = textBox_username.Text;
            Settings.Default.Save();
        }
        private void button_username_get_Click(object sender, EventArgs e)
        {
            textBox_username.Text = System.Environment.UserName;
        }
        private void btn_username_replace_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show($"要把所有的“{Settings.Default.username}”都替换成“{textBox_username.Text}”吗？\n ", "请确认", MessageBoxButtons.YesNo);
            if (a == DialogResult.Yes)
            {
                //外面三个写一下
                Settings.Default.articles = Settings.Default.articles.Replace(Settings.Default.username, textBox_username.Text);
                Settings.Default.source = Settings.Default.source.Replace(Settings.Default.username, textBox_username.Text);
                Settings.Default.output = Settings.Default.output.Replace(Settings.Default.username, textBox_username.Text);
                Settings.Default.Save();

                textBox1.Text = textBox1.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox2.Text = textBox2.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox3.Text = textBox3.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox4.Text = textBox4.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox5.Text = textBox5.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox6.Text = textBox6.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox7.Text = textBox7.Text.Replace(Settings.Default.username, textBox_username.Text);
                textBox8.Text = textBox8.Text.Replace(Settings.Default.username, textBox_username.Text);
                button1.PerformClick();//save
            }
        }

        private void button_myconfig_Click(object sender, EventArgs e)
        {
            string mycmd = $@"start C:\Users\{Settings.Default.username}\AppData\Local\hugoAuto1";
            RunCMDCommand_no_rediect_edition(mycmd);
        }
        public void RunCMDCommand_no_rediect_edition(params string[] command)
        {
            using (Process pc = new Process())
            {
                pc.StartInfo.FileName = "cmd.exe";
                pc.StartInfo.CreateNoWindow = false;//隐藏窗口运行
                pc.StartInfo.RedirectStandardError = false;//重定向错误流
                pc.StartInfo.RedirectStandardInput = true;//重定向输入流
                pc.StartInfo.RedirectStandardOutput = false;//重定向输出流
                pc.StartInfo.UseShellExecute = false;
                pc.Start();
                int lenght = command.Length;
                foreach (string com in command)
                {
                    pc.StandardInput.WriteLine(com);//输入CMD命令
                }
                pc.StandardInput.WriteLine("exit");//结束执行，很重要的
                pc.StandardInput.AutoFlush = true;

                pc.WaitForExit();
                pc.Close();
            }
        }


    }
}
