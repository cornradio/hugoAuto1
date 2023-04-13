using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hugoAuto1.Properties;



namespace hugoAuto1
{
    public partial class Form1 : Form
    {
        string str_source = "";
        string str_output = "";
        string str_articles = "";


        public Form1()
        {
            InitializeComponent();
        }



        //保存设置
        private void button11_Click(object sender, EventArgs e)
        {
            Settings.Default.source = textBox1.Text;
            Settings.Default.output = textBox2.Text;
            Settings.Default.articles = textBox3.Text;
            Settings.Default.Save();
            mylog("设置已保存");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Default.source;
            textBox2.Text = Settings.Default.output;
            textBox3.Text = Settings.Default.articles;
        }

        private void runincmd(string yourcommand)
        {
            string strCmdText;
            strCmdText = $"/C {yourcommand}";
            Process process = Process.Start("CMD.exe", strCmdText);
        }
        private void openinbrowser(string link)
        {

            try
            {
                string strCmdText;
                strCmdText = $"{link}";
                Process process = Process.Start(Settings.Default.chromeCommand, strCmdText);
                //Process process = Process.Start($@"C:\Program Files\Google\Chrome\Application\chrome.exe", strCmdText);
            }
            catch (Exception)
            {
                mylog("chrome 路径错误，请自行设置");
                Form a = new Form2();
                a.ShowDialog();
            }

        }



        #region openfloders
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", textBox2.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", textBox3.Text);
        }
        #endregion

        //hugo编译输出
        private void button6_Click(object sender, EventArgs e)
        {
            string outpath = textBox2.Text;
            string rawpath = textBox1.Text;
            string mycmd =
              $@"hugo -d {outpath} -s {rawpath}";
            RunCMDCommand_no_rediect_edition(mycmd);
            mylog($"hugo编译输出完毕");

        }
        //开启github
        private void button7_Click(object sender, EventArgs e)
        {
            Process process = Process.Start(Settings.Default.githubCommand, "");
            mylog($"已经命令Github启动");
        }
        //新建文章
        private void button8_Click(object sender, EventArgs e)
        {
            string filename = comboBox1.Text;
            string rawpath = textBox1.Text;
            string articles = textBox3.Text;

            string postroot = articles.Replace(rawpath, "").Trim('\\');

            if (filename!=string.Empty){
                string mycmd =
                     $@"hugo new -s {rawpath} -c \content\zh-cn   posts\{filename}.md";
                RunCMDCommand_no_rediect_edition(mycmd);
                mylog($"创建【{filename}.md】");
            }
            else
                comboBox1.BackColor = Color.Red;
        }

        //打开文章
        private void button9_Click(object sender, EventArgs e)
        {
            string filename = comboBox1.Text;
            string rawpath = textBox1.Text;
            string filePath = $@"{rawpath}\content\zh-cn\posts\{filename}.md";
            try
            {
                Process.Start(Settings.Default.typoraCommand.ToString(), filePath);
                mylog($"已经命令typora打开【{filename}.md】");
            }
            catch (Exception)
            {
                mylog("Typora 路径错误，请自行设置：");
                Form a = new Form2();
                a.ShowDialog();
            }
        }
        //打开obsidian
        private void button12_Click(object sender, EventArgs e)
        {
            string filename = comboBox1.Text;
            string rawpath = textBox1.Text;
            string filePath = $@"{rawpath}\content\zh-cn\posts\{filename}.md";
            try
            {
                Process.Start(Settings.Default.vscCommand.ToString(), filePath);
                mylog($"已经命令vsc打开【{filename}.md】");
            }
            catch (Exception)
            {
                mylog("vscode 路径错误，请自行设置：");
                Form a = new Form2();
                a.ShowDialog();
            }
        }
        //刷新combobox
        private void button4_Click(object sender, EventArgs e)
        {

            comboBox1.BackColor = Color.White;
            //string rawpath = textBox1.Text;
            //string filePath = $@"{rawpath}\content\zh-cn\posts";
            string filePath = $@"{textBox3.Text}";
            comboBox1.Items.Clear();
            var files = Directory
              .GetFiles(filePath, "*.md");
            //提取路径地址+/为了在后面把完整路径剔除
            string pathstr = filePath + "\\";
            string pathstr2 = ".md";
            int count = 0;
            foreach (var file in files)
            {
                //逐个把文件名放在combox中
                comboBox1.Items.Add(file.ToString().Replace(pathstr, "").Replace(pathstr2, ""));
                count++;
            }
            mylog($"读取到了{count}个md文章");
        }
        //开启server和浏览器
        private void button5_Click(object sender, EventArgs e)
        {
            string mycmd =
               $@"hugo server -t {Settings.Default.hugotheme} -p 51000 -s {Settings.Default.source}";
            runincmd(mycmd);
            Clipboard.SetText(mycmd);
            mylog("命令已经复制到剪切板，如果有问题就用手工方式打开吧！");
        }
        //本地浏览器打开博客
        private void button10_Click(object sender, EventArgs e)
        {
            openinbrowser("http://localhost:51000/hugo/");
            mylog($"浏览器已启动");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_gitpull_Click(object sender, EventArgs e)
        {
            if (checkBox_use_proxy_for_git.Checked)
            {
                string cmd = Settings.Default.gitproxy_cmd +Environment.NewLine+ Settings.Default.gitpull_cmd;
                RunCMDCommand_no_rediect_edition(cmd.Split('\n'));
            }
            else
                RunCMDCommand_no_rediect_edition(Settings.Default.gitpull_cmd.Split('\n'));
            mylog("git pull 执行完毕");
        }
        private void btn_gitpush_Click(object sender, EventArgs e)
        {
            if (checkBox_use_proxy_for_git.Checked)
            {
                string cmd = Settings.Default.gitproxy_cmd + Environment.NewLine + Settings.Default.gitpush_cmd;
                RunCMDCommand_no_rediect_edition(cmd.Split('\n'));
            }
            else
                RunCMDCommand_no_rediect_edition(Settings.Default.gitpush_cmd.Split('\n'));
            mylog("git add,git push 执行完毕");
        }

        private void mylog(string output)
        {
            textBox_output.Text += $"[{DateTime.Now.ToString("T")}] " + output + Environment.NewLine;
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
                //pc.StandardInput.WriteLine("exit");//结束执行，很重要的
                pc.StandardInput.AutoFlush = true;

                pc.WaitForExit();
                pc.Close();
            }
        }
        public void RunCMDCommand(out string outPut, params string[] command)
        {
            using (Process pc = new Process())
            {
                pc.StartInfo.FileName = "cmd.exe";
                pc.StartInfo.CreateNoWindow = true;//隐藏窗口运行
                pc.StartInfo.RedirectStandardError = true;//重定向错误流
                pc.StartInfo.RedirectStandardInput = true;//重定向输入流
                pc.StartInfo.RedirectStandardOutput = true;//重定向输出流
                pc.StartInfo.UseShellExecute = false;
                pc.Start();
                int lenght = command.Length;
                foreach (string com in command)
                {
                    pc.StandardInput.WriteLine(com);//输入CMD命令
                }
                pc.StandardInput.WriteLine("exit");//结束执行，很重要的
                pc.StandardInput.AutoFlush = true;

                outPut = pc.StandardOutput.ReadToEnd();//读取结果        

                pc.WaitForExit();
                pc.Close();
            }
        }

        private void button_Articles_Click(object sender, EventArgs e)
        {
            Form a = new FormArticles();
            a.ShowDialog();
        }

        #region lables
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //hugo download
            //openinbrowser("https://github.com/gohugoio/hugo/releases");
            MessageBox.Show("接下来会帮助你使用winget下载hugo，下载安装后请重启电脑");
            RunCMDCommand_no_rediect_edition("winget install  Hugo.Hugo");
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //git download
            //openinbrowser("https://git-scm.com/download");
            MessageBox.Show("接下来会帮助你使用winget下载git，下载安装后请重启电脑");
            RunCMDCommand_no_rediect_edition("winget install  Git.Git");

        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //wired command hints

            Form a = new Form_commands();
            a.ShowDialog();
        }


        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //settings
            Form a = new Form2();
            a.ShowDialog();
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //help
            openinbrowser("https://github.com/cornradio/hugoAuto1");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //git hub issues
            openinbrowser("https://github.com/cornradio/hugoAuto1/issues");
        }
        #endregion

        //get a new name for post
        private void button14_Click(object sender, EventArgs e)
        {
            //生成字符串，2023-02-18-python打包成exe ，换成今天的日期
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string newname = today + "-" + "doubleclick_here";
            comboBox1.Text = newname;
        }
    }
}
