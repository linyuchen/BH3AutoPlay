using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace WinApiDll
{

    public struct WindowRect  // 窗口坐标
    {
        public uint Left;
        public uint Top;
        public uint Right;
        public uint Bottom;
    }
    public class Window
    {
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

        [DllImport("user32")]
        public static extern bool GetClientRect(IntPtr hwnd,out WindowRect lpRect);

        [DllImport("user32")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "EnumChildWindows")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack lpfn, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmdShow">1 窗口正常大小, 2 最小化， 3 最大化</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        public static extern bool IsWindowVisible(int HWND);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern long GetWindowLong(int HWND, int index);

        public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);




        /// <summary>
        /// 获取窗口文字
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="stringLength"></param>
        /// <returns></returns>
        public static string GetWindowString(IntPtr hwnd, int stringLength=1024)
        {
            StringBuilder s = new StringBuilder(stringLength);
            Window.GetWindowText(hwnd, s, stringLength);
            return s.ToString();
        }



        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        // 遍历线程上的窗口
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        /// <summary>
        /// 通过遍历寻找窗口
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="titleContains">是否模糊查找（包含窗口标题即可）</param>
        /// <returns></returns>
        public static List<IntPtr> FindWindowsByEnum(string title, bool titleContains = false)
        {
            return Window.FindWindowsByEnum(Window.GetDesktopWindow(), title, titleContains);
        }

        public static List<IntPtr> FindWindowsByEnum(IntPtr parent, string title, bool titleContains=false)
        {
            List<IntPtr> windows = new List<IntPtr>();
            Window.EnumChildWindows(parent, (hwnd, l) => {
                String content = Window.GetWindowString(hwnd, 1024);                
                if (!titleContains)
                {                    
                    if (content == title)
                    {
                        windows.Add((IntPtr)hwnd);
                    }
                }
                else
                {                    
                    if (content.Contains(title))
                    {                        
                        windows.Add((IntPtr)hwnd);
                    }
                }
                return true;
            }, IntPtr.Zero);
            return windows;
        }
    }
}

