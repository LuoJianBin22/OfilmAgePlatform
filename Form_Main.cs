using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Jim.IO;
using Jim.CommunicationCtrls;

namespace OfilmAgePlatform
{
    public partial class Form_Main : Form
    {
        enum emCmdType
        {
            Start,
            Stop,
            Query,
        }

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //主窗体最大化
            this.WindowState = FormWindowState.Maximized;

            //先加载参数
            Form_Set.Instance.LoadSysSetting();

            //显示设备名称
            this.Text = Form_Set.Instance.m_SysParam.m_MachineName;

            //初始化通讯窗体
            Form_Communication.Instance.TopLevel = false;
            Form_Communication.Instance.Dock = DockStyle.Fill;
            Form_Communication.Instance.Parent = tabPage_通讯页面;
            Form_Communication.Instance.Show();


            //打开所有测试窗体,并设置每个测试窗体的父窗体
            OpenAllAgeForm();

            Initial();

            UpdateControlsSize();
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAllAgeForm();
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            UpdateControlsSize();
        }

        public void Initial()
        {
            
            //创建与老化平台对应的TCP客户端控件
            Form_Communication.Instance.tcpClientCtrl_ageForm.RemoteIP = "127.0.0.1";
            Form_Communication.Instance.tcpClientCtrl_ageForm.RemotePort = Form_Set.Instance.m_SysParam.m_AgeFormServerPort;
            Form_Communication.Instance.tcpClientCtrl_ageForm.SendText = new string[] { "RLG START", "RLG STOP", "RLG QSTATUS", "" };
            Form_Communication.Instance.tcpClientCtrl_ageForm.ReceiveDataCallback += new TcpClientCtrl.ReceiveDataEventHandler(TcpClientRecvData);
            Form_Communication.Instance.tcpClientCtrl_ageForm.Connect(Form_Communication.Instance.tcpClientCtrl_ageForm.RemoteIP, 
                Form_Communication.Instance.tcpClientCtrl_ageForm.RemotePort);//开始连接服务端

            
            //设置与主控上位机服务端通讯的TCP客户端控件参数
            Form_Communication.Instance.tcpClientCtrl_Slave.RemoteIP = Form_Set.Instance.m_SysParam.m_TcpMasterIP;
            Form_Communication.Instance.tcpClientCtrl_Slave.RemotePort = Form_Set.Instance.m_SysParam.m_TcpMasterPort;
            Form_Communication.Instance.tcpClientCtrl_Slave.SendText = new string[] { "RLG START", "RLG STOP", "RLG QSTATUS", "" };
            Form_Communication.Instance.tcpClientCtrl_Slave.ReceiveDataCallback += new TcpClientCtrl.ReceiveDataEventHandler(TcpSlaveRecvData);
            Form_Communication.Instance.tcpClientCtrl_Slave.Connect(Form_Communication.Instance.tcpClientCtrl_Slave.RemoteIP, Form_Communication.Instance.tcpClientCtrl_Slave.RemotePort);//开始连接服务端
        }

        private void toolStripButton_设置_Click(object sender, EventArgs e)
        {
            Form_Set.Instance.ShowDialog();
        }

        private void UpdateControlsSize()
        {
            OpenThirdExe.SetWindowLocation(pnl_container);
        }

        private void OpenAllAgeForm()
        {
            string filePath = Path.GetFullPath(Form_Set.Instance.m_SysParam.m_AgeTestAppFolderName + $@"\HisCCMAgeTest.exe");
            if (File.Exists(filePath))
            {
                OpenThirdExe.OpenAndSetWindow(filePath, pnl_container);
            }
            else
            {
                MessageBox.Show($"老化窗体路径不存在，请检查\r\n{filePath}");
            }
        }

        private void CloseAllAgeForm()
        {
            try
            {
                System.Diagnostics.Process[] app = System.Diagnostics.Process.GetProcessesByName($"HisCCMAgeTest");
                foreach (var p in app)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 刷新布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateControlsSize();
        }


        
        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadResult();

            timer1.Enabled = false;
        }

        //接收老化平台数据
        object objClientRecvLock = new object();
        private void TcpClientRecvData(object sender, TCPClient.ReceiveDataEventArgs e)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(() => TcpClientRecvData(sender, e)));
            }
            else
            {
                lock (objClientRecvLock)
                {
                    string data = e.DataConvertedByBytesReceived;
                    data = data.Replace("\0", "");
                    data = data.Replace("\r", "");
                    data = data.Replace("\n", "");

                    if (data == "RLG START")
                    {
                        UpdateControlsSize();
                        Form_Communication.Instance.tcpClientCtrl_Slave.Send(data);
                    }
                    else if (data == "RLG STOP")
                    {
                        Form_Communication.Instance.tcpClientCtrl_Slave.Send(data);
                    }
                    else if (data == "RLG QSTATUS NOTEST")
                    {
                        //老化平台软件打开，但还没开始测试时，处于NOTEST状态

                    }
                    else if (data == "RLG QSTATUS TESTING")
                    {
                        
                    }
                    else if (data == "RLG QSTATUS STOP")
                    {
                        if (!timer1.Enabled)
                        {
                            timer1.Interval = Form_Set.Instance.m_SysParam.m_ReadAgeResultWhatTime;
                            timer1.Enabled = true;
                        }
                    }
                }
            }
        }

        private void ReadResult()
        {
            //老化停止，获取当前测试盒的测试结果数据,得到的结果已经去掉名称行
            SortedDictionary<int, List<string>> dicResultLines = ReadResultFolderData();
            if (dicResultLines.Count == 0) return;

            string allCamResults = "";

            //遍历参与测试的摄像头数据
            foreach (int camNo in dicResultLines.Keys)
            {
                //测试结果格式：
                //测试OK: TRUE,正确帧，错误帧，帧率，最大电流
                //测试NG: FALSE,错误描述

                //发送给主控上位机的测试结果格式：
                //摄像头1编号,TRUE,正确帧，错误帧，帧率，最大电流 ; 摄像头1编号,TRUE,正确帧，错误帧，帧率，最大电流 或者 摄像头1编号,FALSE,失败描述


               // MessageBox.Show($"{camNo}=> + {dicResultLines[camNo].ToString()}");


                if (camNo >= Form_Set.Instance.m_SysParam.m_AgeFormStartTestIndex.Length)
                    continue;

                if (!dicResultLines.ContainsKey(camNo) || dicResultLines[camNo].Count == 0)
                    continue;

                string[] results = dicResultLines[camNo][0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (results.Length == 0) continue;

                TestResult testRes = new TestResult();
                testRes.CamNo = Form_Set.Instance.m_SysParam.m_AgeFormStartTestIndex[camNo];

                if (results.Length == 5)//测试成功
                {
                    try
                    {
                        testRes.Result = results[0];
                        testRes.CorretFrame = Convert.ToInt16(results[1]);
                        testRes.ErrorFrame = Convert.ToInt16(results[2]);
                        testRes.FrameRatio = Convert.ToDouble(results[3]);
                        testRes.MaxCurrent = Convert.ToDouble(results[4]);

                        string resultSendToMaster = $"{testRes.CamNo},{testRes.Result},{testRes.CorretFrame},{testRes.ErrorFrame},{testRes.FrameRatio},{testRes.MaxCurrent};";
                        allCamResults += resultSendToMaster;
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex.Message);
                    }
                }
                else if (results.Length == 2)//测试失败
                {
                    testRes.Result = results[0];
                    testRes.FailDescription = results[1];

                    string resultSendToMaster = $"{testRes.CamNo},{testRes.Result},{testRes.FailDescription};";
                    allCamResults += resultSendToMaster;


                }
            }

            if (allCamResults != "")
            {
                allCamResults = allCamResults.Substring(0, allCamResults.LastIndexOf(';'));
                Form_Communication.Instance.tcpClientCtrl_Slave.Send(allCamResults);
                System.Threading.Thread.Sleep(20);
            }

            //老化平台测试完成
            Form_Communication.Instance.tcpClientCtrl_Slave.Send("AgeStop");
            timer_queryAgeStatus.Enabled = false;

            timer1.Enabled = false;
        }

        //接收主控服务端数据
        object objSlaveRecvLock = new object();
        private void TcpSlaveRecvData(object sender, TCPClient.ReceiveDataEventArgs e)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(() => TcpSlaveRecvData(sender, e)));
            }
            else
            {
                lock (objSlaveRecvLock)
                {
                    string data = e.DataConvertedByBytesReceived;
                    data = data.Replace("\0", "");
                    data = data.Replace("\r", "");
                    data = data.Replace("\n", "");

                    if (data == "RLG START" || data == "RLG STOP" || data == "RLG QSTATUS")
                    {
                        emCmdType cmdType = emCmdType.Stop;
                        if (data == "RLG STOP")
                            cmdType = emCmdType.Stop;
                        else if (data == "RLG START")
                            cmdType = emCmdType.Start;
                        else if (data == "RLG QSTATUS")
                            cmdType = emCmdType.Query;

                        DoTest(cmdType);
                    }
                    else if(data=="Online")
                    {
                        Form_Communication.Instance.tcpClientCtrl_Slave.Send("Online");
                    }
                }
            }
        }


        object objSendLock = new object();
        private void DoTest(emCmdType cmdType)
        {
            lock (objSendLock)
            {
                string cmd = "";
                if (cmdType == emCmdType.Stop)
                {
                    cmd = "RLG STOP";
                    if (timer_queryAgeStatus.Enabled)
                        timer_queryAgeStatus.Enabled = false;
                }
                else if (cmdType == emCmdType.Start)
                {
                    cmd = "RLG START";
                    if (!timer_queryAgeStatus.Enabled)
                    {
                        timer_queryAgeStatus.Interval = Form_Set.Instance.m_SysParam.m_QueryAgeStatusElapsedTime;
                        timer_queryAgeStatus.Enabled = true;
                    }
                    
                    //开始测试时，先把结果文件移到备份文件夹
                    MoveResultFolderFiles();
                }
                else if (cmdType == emCmdType.Query)
                {
                    cmd = "RLG QSTATUS";
                }

                Form_Communication.Instance.tcpClientCtrl_ageForm.Send(cmd);
                
            }
        }

        //按文件夹名称顺序排序
        private void SortAsFolderName(ref DirectoryInfo[] dirs)
        {
            Array.Sort(dirs, delegate (DirectoryInfo x, DirectoryInfo y) { return x.Name.CompareTo(y.Name); });
        }

        //按文件夹名称倒序排序
        private void SortAsFolderName2(ref DirectoryInfo[] dirs)
        {
            Array.Sort(dirs, delegate (DirectoryInfo x, DirectoryInfo y) { return x.Name.CompareTo(x.Name); });
        }

        //读取指定测试盒的参与测试摄像头的测试结果
        private SortedDictionary<int, List<string>> ReadResultFolderData()
        {
            //结果文件夹
            string folder = Path.GetFullPath(Form_Set.Instance.m_SysParam.m_AgeTestAppFolderName + $"\\ReportData");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            //获取结果目录
            string[] subFolders = Directory.GetDirectories(folder);

            //在结果文件夹提取参与测试的每个摄像头的测试结果(不在备份文件夹提取，是因为备份文件夹可能有之前的结果文件)
            SortedDictionary<int, List<string>> dicResultLines = new SortedDictionary<int, List<string>>();

            //获取所有目录里的测试结果文件
            for (int i = 0; i < subFolders.Length; i++)
            {
                //获取结果文件
                string[] files = Directory.GetFiles(subFolders[i]);

                if (files.Length > 0)
                {
                    //提取摄像头编号 Cam_0.csv、Cam_1.csv
                    int lastIndex = subFolders[i].LastIndexOf("\\");
                    string fileName = subFolders[i].Substring(lastIndex + 1);
                    string[] camInfo = fileName.Split('_');
                    int camNo;
                    if(camInfo.Length==2)
                    {
                        camNo = Convert.ToInt16(camInfo[1]);

                        //读取测试结果
                        List<string> resultLines = Csv.Read2(files[0]);
                        //第一行是 名称行  Result	CorrectFrame	ErrorFrame	FrameRate	MaxCurrent
                        //第二行是 结果
                        if (resultLines.Count > 0)
                        {
                            resultLines.RemoveAt(0);//去除 第一行 名称行

                            if (!dicResultLines.ContainsKey(camNo))
                                dicResultLines.Add(camNo, resultLines);
                            else
                                dicResultLines[camNo] = resultLines;
                        }

                        //移动指定测试盒的测试结果文件复制到备份文件夹
                        //MoveResultFolderFiles();
                    }
                }
            }
            return dicResultLines;
        }

        //移动指定测试盒的测试结果文件复制到备份文件夹
        private void MoveResultFolderFiles()
        {
            try
            {
                CopyResultFolderFiles();
                DeleteResultFolderFiles();
            }
            catch { }
        }

        //移动指定测试盒的测试结果文件复制到备份文件夹
        private void CopyResultFolderFiles()
        {
            try
            {
                //创建备份文件夹
                string folderBackup = Path.GetFullPath(Form_Set.Instance.m_SysParam.m_AgeTestAppFolderName + $"\\ReportDataBackup");
                if (!Directory.Exists(folderBackup))
                    Directory.CreateDirectory(folderBackup);

                //结果文件夹
                string folder = Path.GetFullPath(Form_Set.Instance.m_SysParam.m_AgeTestAppFolderName + $"\\ReportData");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                //获取结果目录
                string[] subFolders = Directory.GetDirectories(folder);
                for (int i = 0; i < subFolders.Length; i++)
                {
                    int lastIndex = subFolders[i].LastIndexOf("\\");
                    string folderName = subFolders[i].Substring(lastIndex + 1);

                    //创建备份子文件夹
                    string backupFolder = folderBackup + "\\" + folderName;
                    if (!Directory.Exists(backupFolder))
                        Directory.CreateDirectory(backupFolder);

                    //获取结果文件
                    string[] files = Directory.GetFiles(subFolders[i]);

                    //把结果文件复制到备份文件夹
                    for (int j = 0; j < files.Length; j++)
                    {
                        //把该次测试结果文件移动到备份的文件夹备份
                        lastIndex = files[j].LastIndexOf("\\");
                        string fileName = files[j].Substring(lastIndex + 1);
                        File.Copy(subFolders[i] + "\\" + fileName, backupFolder + "\\" + fileName, true);
                    }
                }
            }
            catch (Exception exxx)
            {
                MessageBox.Show(exxx.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //删除指定摄像头的测试结果文件
        private void DeleteResultFolderFiles()
        {
            try
            {
                //结果文件夹
                string folder = Path.GetFullPath(Form_Set.Instance.m_SysParam.m_AgeTestAppFolderName + $"\\ReportData");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);


                DelateFile(folder);


                //获取结果目录
                //string[] subFolders = Directory.GetDirectories(folder);

                ////删除所有目录里的测试结果文件
                //for (int i = 0; i < subFolders.Length; i++)
                //{
                //    //获取结果文件
                //    string[] files = Directory.GetFiles(subFolders[i]);


                //    //删除所有测试结果文件
                //    for (int j = 0; j < files.Length; j++)
                //    {
                //        File.Delete(files[j]);
                //    }
                //    //删除目录
                //    Directory.Delete(subFolders[i]);
                //}
            }
            catch (Exception exxx)
            {
                MessageBox.Show("自动删除文件失败！ 请手动删除测试结果文件", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public  void  DelateFile(string folder)
        {
           // 获取结果目录
            string[] subFolders = Directory.GetDirectories(folder);

            //删除所有目录里的测试结果文件
            for (int i = 0; i < subFolders.Length; i++)
            {
                int cout = Directory.GetDirectories(subFolders[i]).Count();
                if (cout!=0)
                {
                    DelateFile(subFolders[i]);
                }

                //获取结果文件
                string[] files = Directory.GetFiles(subFolders[i]);

                //删除所有测试结果文件
                for (int j = 0; j < files.Length; j++)
                {
                    File.Delete(files[j]);
                }
                //删除目录
                Directory.Delete(subFolders[i]);
            }
        }

        //定时询问老化状态
        private void timer_queryAgeStatus_Tick(object sender, EventArgs e)
        {
            DoTest(emCmdType.Query);
        }

    }

    public class TestResult
    {
        public int CamNo { get; set; }//测试摄像头编号
        public string Result { get; set; }//测试结果：TRUE / FALSE
        public string FailDescription { get; set; }//测试失败时失败描述
        public int CorretFrame { get; set; }//正确帧
        public int ErrorFrame { get; set; }//错误帧
        public double FrameRatio { get; set;}//帧率
        public double MaxCurrent { get; set; }//最大电流
    }
}
