using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jim.IO;

namespace OfilmAgePlatform
{
    public partial class Form_Set : Form
    {
        static readonly Form_Set instance = new Form_Set();
        public static Form_Set Instance => instance;

        string m_SysParamFilePath = Application.StartupPath + @"\配置参数\SysSetting.Json";

        public SysParams m_SysParam = new SysParams();

        private Form_Set()
        {
            InitializeComponent();
        }

        private void Form_Set_Load(object sender, EventArgs e)
        {
            LoadSysSetting();
        }

        public void LoadSysSetting(bool SkipControl = false)
        {
            try
            {
                m_SysParam = JsonAccess.Read<SysParams>(m_SysParamFilePath);
                if (m_SysParam == null)
                {
                    m_SysParam = new SysParams();
                    SaveSysSetting(true);
                }

                if (!SkipControl)
                    propertyGrid_SysParam.SelectedObject = m_SysParam;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载参数失败，异常信息：\r\n{ex.Message}");
            }
        }

        private void SaveSysSetting(bool SkipControl = false)
        {
            try
            {
                if (!SkipControl)
                    m_SysParam = (SysParams)propertyGrid_SysParam.SelectedObject;
                JsonAccess.Write<SysParams>(m_SysParamFilePath, m_SysParam);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存参数失败，异常信息：\r\n{ex.Message}");
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSysSetting();
        }
    }

    public class SysParams
    {
        [Category("自定义"), DisplayName("设备名称"), Description("设备名称")]
        public string m_MachineName { get; set; } = "摄像头老化设备-从站";

        [Category("自定义"), DisplayName("\t\t\t老化窗体测试起始编号"), Description("每个老化窗体的测试起始编号，如果第一个老化窗体设置为1，则第二个设置为9，第三个为17...以此类推，因为每个老化窗体最多测试8个摄像头，格式：窗体1起始编号,窗体2起始编号,...窗体n起始编号")]
        public int[] m_AgeFormStartTestIndex { get; set; } = new int[] { 1,9};
        [Category("自定义"), DisplayName("\t\t老化平台服务端端口"), Description("老化平台服务端端口")]
        public int m_AgeFormServerPort { get; set; } = 42001;//第一个老化平台的端口号(老化平台是服务端,监听任意IP，不用配置IP地址)

        [Category("自定义"), DisplayName("主控上位机服务端IP"), Description("主控上位机服务端IP地址")]
        public string m_TcpMasterIP { get; set; } = "127.0.0.1";
        [Category("自定义"), DisplayName("主控上位机服务端Port"), Description("主控上位机服务端端口号")]
        public int m_TcpMasterPort { get; set; } = 5000;

        [Category("自定义"), DisplayName("老化测试软件所在目录名称"), Description("老化测试软件所在目录名称")]
        public string m_AgeTestAppFolderName { get; set; } = @"HisFX3AgeTest";

        [Category("自定义"), DisplayName("轮询老化状态间隔时间"), Description("轮询老化状态的间隔时间,单位:毫秒")]
        public int m_QueryAgeStatusElapsedTime { get; set; } = 60 * 1000;

        [Category("自定义"), DisplayName("读取老化结果的时间"), Description("测试结束后多长时间读取老化结果,单位:毫秒")]
        public int m_ReadAgeResultWhatTime { get; set; } = 10 * 1000;
    }

}
