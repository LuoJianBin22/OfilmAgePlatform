using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace OfilmAgePlatform
{
    public class OpenThirdExe
    {
        [DllImport("User32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern int SetWindowText(int hwnd, string lpString);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int WS_THICKFRAME = 262144;
        const int WS_BORDER = 8388608;
        const int GWL_STYLE = -16;

        static IntPtr m_ageFormHandler = IntPtr.Zero;

        public static void OpenAndSetWindow(String fileName, Control container)
        {
            if (fileName.LastIndexOf(".exe") > -1)
            {
                int index = fileName.LastIndexOf("\\");
                string folder = fileName.Substring(0, index);
                string exeName = fileName.Substring(index + 1);

                Process[] processes = Process.GetProcessesByName(exeName);
                if (processes.Length > 0)
                {
                    return;
                }

                Process process = new Process();
                process.StartInfo.FileName = fileName;//要绝对路径
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.WorkingDirectory = folder;
                process.Start();
                process.WaitForInputIdle();

                while (process.MainWindowHandle == IntPtr.Zero)
                {
                    Thread.Sleep(100);
                    process.Refresh();
                }

                IntPtr wnd = process.MainWindowHandle;
                m_ageFormHandler = wnd;

                //去边框
                Int32 wndStyle = GetWindowLong(wnd, GWL_STYLE);
                wndStyle &= ~WS_BORDER;
                wndStyle &= ~WS_THICKFRAME;
                SetWindowLong(wnd, GWL_STYLE, wndStyle);

                SetParent(wnd, container.Handle);
                ShowWindow(wnd, (int)ProcessWindowStyle.Maximized);
            }
            else
            {
                MessageBox.Show("不是有效的exe文件:\r\n" + fileName);
            }
        }

        public static void SetWindowLocation(Control container)
        {
            if (m_ageFormHandler != IntPtr.Zero)
            {
                MoveWindow(m_ageFormHandler, -15, 0, container.Width + 10, container.Height + 40, true);
                ShowWindow(m_ageFormHandler, (int)ProcessWindowStyle.Maximized);
            }
        }
    }
}
