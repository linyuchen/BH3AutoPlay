using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BH3AutoPlay
{

    enum BH3WindowRatio
    {        
        notFound,
        P1080,
        p720,
    }
    class BH3Window
    {
        public Point windowPos = new Point();
        public Point startMarkPos = new Point();
        public Point restartBtnPos1 = new Point();
        public Point restartBtnPos2 = new Point();
        public Point shieldPos = new Point();
        public Point healthPos = new Point();
        private Dm.dmsoft dmsoft = new Dm.dmsoft();
        string HEALTH_COLOR_ORANGE = "FF9141";  // 第4条血橙色，也是最后一条
        string HEALTH_COLOR_GREEN = "1ABC9C";  //  第3条血绿色
        string HEALTH_COLOR_PURPLE = "9B59B6";// 第1条血紫色
        string HEALTH_COLOR_BLUE = "3498DB"; // 第2条血紫色

        public void BH3AutoPlay()
        {

        }

        private bool CheckColor(Point pos, String color)
        {
            String c = dmsoft.GetColor(pos.X, pos.Y);
            return c.ToLower() == color.ToLower();
        }

        public bool CheckBossAlive()
        {
            if (CheckColor(healthPos, HEALTH_COLOR_BLUE))
            {
                return true;
            }
            else if (CheckColor(healthPos, HEALTH_COLOR_GREEN))
            {
                return true;
            }
            else if (CheckColor(healthPos, HEALTH_COLOR_PURPLE))
            {
                return true;
            }
            else if (CheckColor(healthPos, HEALTH_COLOR_ORANGE))
            {
                return true;
            }
            return false;
        }
        public bool CheckWindow()
        {
            return this.DetectWindowRatio() != BH3WindowRatio.notFound;
        }

        public bool IsStart()
        {
            if (this.startMarkPos.X < 0 || this.startMarkPos.Y < 0)
            {
                return false;
            }
            return this.CheckColor(this.startMarkPos, "0060ff");
        }
        //是否破盾
        public bool ShieldIsDestroy()
        {
            return !CheckColor(shieldPos, "FFC741");
        }

        private BH3WindowRatio DetectWindowRatio()
        {
            this.windowPos = new Point();
            this.startMarkPos = new Point();
            IntPtr bh3hwnd = WinApiDll.Window.FindWindow(null, "崩坏3");
            if (bh3hwnd == null)
            {
                return BH3WindowRatio.notFound;
            }
            WinApiDll.WindowRect rect = new WinApiDll.WindowRect();
            //WinApiDll.WindowRect pos = new WinApiDll.WindowRect();
            bool tmp = WinApiDll.Window.GetClientRect(bh3hwnd, out rect);
            WinApiDll.Window.ClientToScreen(bh3hwnd, ref windowPos);
            switch (rect.Bottom)
            {
                case 1080:
                    {
                        startMarkPos.X = windowPos.X + 68;
                        startMarkPos.Y = windowPos.Y + 160;
                        restartBtnPos1.X = windowPos.X + 568;
                        restartBtnPos1.Y = windowPos.Y + 987;
                        restartBtnPos2.X = windowPos.X + 685;
                        restartBtnPos2.Y = windowPos.Y + 755;
                        shieldPos.X = windowPos.X + 558;
                        shieldPos.Y = windowPos.Y + 56;
                        healthPos.X = windowPos.X + 568;
                        healthPos.Y = windowPos.Y + 29;
                        return BH3WindowRatio.P1080;
                    }
                case 720:
                    {
                        startMarkPos.X = windowPos.X + 46;
                        startMarkPos.Y = windowPos.Y + 108;
                        restartBtnPos1.X = windowPos.X + 403;
                        restartBtnPos1.Y = windowPos.Y + 657;
                        restartBtnPos2.X = windowPos.X + 453;
                        restartBtnPos2.Y = windowPos.Y + 508;
                        shieldPos.X = windowPos.X + 373;
                        shieldPos.Y = windowPos.Y + 38;
                        healthPos.X = windowPos.X + 380;
                        healthPos.Y = windowPos.Y + 22;
                        return BH3WindowRatio.p720;
                    }
                default:
                    return BH3WindowRatio.notFound;
            }
        }
    }
}
