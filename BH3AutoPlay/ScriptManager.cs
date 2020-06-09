using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BH3AutoPlay
{
    class AutoPlayScriptManager
    {
        private BH3Window bh3window = new BH3Window();
        public bool running = true;
        private int lostRuningCount = 0;

        public AutoPlayScript currentScript;

        public delegate void ScriptStartStopHandler();

        public event ScriptStartStopHandler ScriptStartEvent;
        public event ScriptStartStopHandler ScriptStopEvent;

        private Thread mainThread;
        private Thread checkRunThread;
        private Form1 form;
        private List<AutoPlayScript> scripts = new List<AutoPlayScript>();
        private static readonly object lockObject = new object();

        public AutoPlayScriptManager(Form1 form)
        {
            scripts.Add(new AutoPlay墨炎真VS冰箱31680up38016(bh3window));
            scripts.Add(new AutoPlay鬼圣迅vs贝贝龙31573up37888(bh3window));
            scripts.Add(new AutoPlay律山紫vs皮皮马31626up37952(bh3window));
            scripts.Add(new AutoPlay山粉蓝vs月轮37760非up31466(bh3window));
            currentScript = scripts[0];
            this.form = form;
            mainThread = new Thread(new ThreadStart(() =>
            {
                lock (lockObject)
                {
                    while (running)
                    {
                        if (!bh3window.CheckWindow())
                        {
                            continue;
                        }
                        bool isStart = bh3window.IsStart();
                        Thread.Sleep(10);
                        if (isStart)
                        {
                            Console.WriteLine("start");
                            ScriptStartEvent();
                            // 阻塞，等待脚本打完
                            currentScript.Start();
                            while (currentScript.running) 
                            {
                                Thread.Sleep(100);    
                            }
                        }
                        else
                        {
                            ScriptStopEvent();
                        }
                    }
                }
            }));
            mainThread.Start();
            checkRunThread = new Thread(new ThreadStart(CheckRunning)); 
            checkRunThread.Start();
        }

        private void CheckRunning()
        {
            lostRuningCount = 0;
            while (running)
            {
                //Console.WriteLine("check run");
                if (!bh3window.IsFighting())
                {
                    //Console.WriteLine("not run");
                    if (lostRuningCount >= 1)
                    {
                        currentScript.Stop();
                        currentScript.ReleaseKeyup();
                        //Console.WriteLine("丢失");
                        ScriptStopEvent();
                        //while (!currentScript.running) ;
                    }
                    lostRuningCount += 1;
                }
                else
                {
                    //Console.WriteLine("run");
                    lostRuningCount = 0;
                }
                Thread.Sleep(1000);
            }
        }

        public void SetScript(int index)
        {
            currentScript = scripts[index];
        }

        public String[] GetScriptNames()
        {
            String[] names = new String[scripts.Count];

            for (int i = 0; i < scripts.Count; i++)
            {
                names[i] = scripts[i].name;
            }
            return names;
        }

        public void Stop()
        {
            running = false;
            currentScript.Stop();
            mainThread.Abort();
            checkRunThread.Abort();
        }
    }
}
