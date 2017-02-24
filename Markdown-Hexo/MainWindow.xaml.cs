using Markdown_Hexo.ModelView;
using Markdown_Hexo_Worker;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Markdown_Hexo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        private const string notInstallMsg = "不是内部或外部命令";
        ModelLogData md = new ModelLogData();
        public MainWindow()
        {
            InitializeComponent();
            //异步执行数据
            worker.WorkerReportsProgress = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //初始化绑定数据
            this.txtLog.DataContext = md;
            this.MBSS.DataContext = md;
            this.WorkersStatus.DataContext = md;
            this.execProgress.DataContext = md;
            this.promptMsg.DataContext = md;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            md.RemoveWorker(sender.GetHashCode());
        }

        /// <summary>
        /// 进度返回处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            md.Log= e.UserState.ToString() + md.Log;
            WorkStatus ws = md.findById(sender.GetHashCode());
            ws.Status = e.ProgressPercentage;
            md.Work = ws;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
           //返回进度
            string log = exec("git --version");
            if (log.Contains(notInstallMsg))
            {
                MessageBox.Show("当前系统未配置GIT请安装GIT BASE后再使用！");
            }
            worker.ReportProgress(33, log);
             log = exec("npm version");
            if (log.Contains(notInstallMsg))
            {
                MessageBox.Show("当前系统未配置NodeJs请安装NodeJs后再使用！");
            }
            worker.ReportProgress(66, log);
            log = exec("hexo version");
            if (log.Contains(notInstallMsg))
            {
                log = "Hexo配置失败！请先配置！";
            }
            worker.ReportProgress(100, log);
        }
       
        private static string exec(string str)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");
            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令
            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return output;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
            md.AddWorker(worker.GetHashCode(), "初始化任务", 0);
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
        }

        private void ConfigHexo_Click(object sender, RoutedEventArgs e)
        {
            //md.Msg = "sadasdas";
            //var result = DialogHost.Show(MBSS);
            //md.Log += exec(" npm install hexo - cli - g");
            promptMsg.IsActive = !promptMsg.IsActive;
        }

        private void WorkersStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           md.Work = ((WorkStatus)e.AddedItems[0]);
            
        }
    }
}
