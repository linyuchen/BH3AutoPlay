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
        public Dm.dmsoft dmsoft = new Dm.dmsoft();
        private bool suspend = false;

        public void SuspendAction()
        {
            this.suspend = true;
        }
        public void ResumeAction()
        {
            this.suspend = false;
        }
        public void Keypress(string key, bool ignoreSuspend=false)
        {
            dmsoft.KeyPressChar(key);
            if (!ignoreSuspend)
            {
                while (suspend) ;
            }
        }
        public void Keyup(string key, bool ignoreSuspend=false)
        {
            dmsoft.KeyUpChar(key);
            if (!ignoreSuspend)
            {
                while (suspend) ;
            }
        }
        public void Keydown(string key, bool ignoreSuspend=false)
        {
            dmsoft.KeyDownChar(key);
            if (!ignoreSuspend)
            {
                while (suspend) ;
            }
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
    class AutoPlayScript : AutoPlay
    {
        public String name;
        public String description;
        public String videoUrl;
        public DateTime startTime;
        public bool running = false;
        private Thread autoPlayThread;
        private Thread checkThread;
        public BH3Window bh3window;
        private static readonly object lockObject = new object();

        public AutoPlayScript(BH3Window bh3window)
        {
            this.bh3window = bh3window;
        }
        public void Pause()
        {
            this.SuspendAction();

            Keypress("esc", true);
            Delay(2000);
            Keypress("~", true);
            Delay(200);
            this.ResumeAction();
        }


        public virtual void OnRestart()
        {

        }
        public void Restart()
        {
            OnRestart();
            Restart(bh3window.restartBtnPos1, bh3window.restartBtnPos2);
            Stop();
            Thread.Sleep(2000);
            Start();
        }

        public void Stop()
        {

            running = false;
            if (autoPlayThread != null)
            {
                autoPlayThread.Abort();
            }
            if (checkThread != null)
            {
                checkThread.Abort();
            }
        }

        public void Start()
        {
            running = true;
            startTime = DateTime.Now;
            autoPlayThread = new Thread(new ThreadStart(
                () =>
                {
                    Delay(200);
                    Action();
                }
            ));
            checkThread = new Thread(new ThreadStart(
                () =>
                {

                    while (running)
                    {

                        TimeSpan runingTime = DateTime.Now - this.startTime;
                        double runningSecond = runingTime.TotalSeconds;
                        OnCheckTime(runningSecond);
                        Thread.Sleep(100);
                    }

                }
            ));
            checkThread.Start();
            autoPlayThread.Start();
        }

        protected virtual void OnCheckTime(double runningSecond)
        {

        }

        public virtual void Action()
        {

        }

    }

}
