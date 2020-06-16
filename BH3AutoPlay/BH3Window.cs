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
        public Point windowPos = new Point();  // 游戏窗口左上角坐标
        public Point startMarkPos = new Point(); // 左上角得分图标坐标，用于判断是否开始了战场
        public Point restartBtnPos1 = new Point();
        public Point restartBtnPos2 = new Point();  
        public Point shieldPos = new Point();  // boss盾条坐标（左边开头）
        public Point healthPos = new Point();  // boss血条坐标（左边开头）
        public Point fightingPos = new Point(); // 左上方的暂停按钮，取黄色来判断按钮是否存在，存在意味着是战斗中
        private Dm.dmsoft dmsoft = new Dm.dmsoft();
        public string HEALTH_COLOR_PURPLE = "9B59B6";// boss第1条血紫色
        public string HEALTH_COLOR_BLUE = "3498DB"; // 第2条血蓝色
        public string HEALTH_COLOR_GREEN = "1ABC9C";  //  第3条血绿色
        public string HEALTH_COLOR_ORANGE = "FF9141";  // 第4条血橙色，一般情况也是最后一条

        public void BH3AutoPlay()
        {

        }

        public bool CheckColor(Point pos, String color, double sim=1)
        {
            if (sim == 1)
            {

                String c = dmsoft.GetColor(pos.X, pos.Y);
                return c.ToLower() == color.ToLower();
            }
            else
            {

                int result = dmsoft.CmpColor(pos.X, pos.Y, color, sim);
                return result == 0;
            }
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

        public bool IsFighting()
        {
            if (this.fightingPos.X <= 0 || this.fightingPos.Y <= 0)
            {
                return false;
            }
            return this.CheckColor(this.fightingPos, "FEDF4C");
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
        public bool CheckShieldIsDestroy()
        {
            return !CheckColor(shieldPos, "FFC741", 0.9);
        }

        private BH3WindowRatio CalcPos(uint windowLength)
        {
            switch (windowLength)
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
                        fightingPos.X = windowPos.X + 52;
                        fightingPos.Y = windowPos.Y + 60;

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
                        fightingPos.X = windowPos.X + 68;
                        fightingPos.Y = windowPos.Y + 24;
                        return BH3WindowRatio.p720;
                    }
                default:
                    return BH3WindowRatio.notFound;
            }

        }

        protected BH3WindowRatio DetectWindowRatio()
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
            return this.CalcPos(rect.Bottom);
        }
    }
}
