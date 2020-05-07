using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BH3AutoPlay
{
    class AutoPlayInfo
    {
        public String name;
        public String description;
        public String videoUrl;
    }
    class AutoPlay
    {
        private Dm.dmsoft dmsoft = new Dm.dmsoft();
        private bool suspend = false;

        public void Keypress(string key)
        {
            dmsoft.KeyPressChar(key);
            while (suspend) ;
        }
        public void Keyup(string key)
        {
            dmsoft.KeyUpChar(key);
            while (suspend) ;
        }
        public void Keydown(string key)
        {
            dmsoft.KeyDownChar(key);
            while (suspend) ;
        }
        public void KeydownThenUp(string key, int millisecond)
        {
            Keydown(key);
            Delay(millisecond);
            Keyup(key);
        }
        public void Delay(int millisecond)
        {
            Thread.Sleep(millisecond);
        }
        public void ReleaseKeyup()
        {

            dmsoft.KeyUpChar("w");
            dmsoft.KeyUpChar("a");
            dmsoft.KeyUpChar("s");
            dmsoft.KeyUpChar("d");
        }
        // 需要管理员权限
        public void Restart(Point btnPos1, Point btnPos2)
        {

            ReleaseKeyup();
            dmsoft.KeyPressChar("ESC");
            Thread.Sleep(1000);
            dmsoft.MoveTo(btnPos1.X, btnPos1.Y);
            Thread.Sleep(500);
            dmsoft.LeftClick();
            Thread.Sleep(500);
            dmsoft.MoveTo(btnPos2.X, btnPos2.Y);
            Thread.Sleep(500);
            dmsoft.LeftClick();
        }
    }
}
