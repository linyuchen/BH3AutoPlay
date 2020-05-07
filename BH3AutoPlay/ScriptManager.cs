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

        private Form1 form;

        private List<AutoPlayScript> scripts = new List<AutoPlayScript>();

        public AutoPlayScriptManager(Form1 form)
        {
            scripts.Add(new AutoPlay律山紫vs皮皮马31626up37952 (bh3window));
            scripts.Add(new AutoPlay山粉蓝vs月轮37760非up31146(bh3window));
            currentScript = scripts[0];
            this.form = form;
            mainThread = new Thread(new ThreadStart(() =>
            {
                while (this.running)
                {
                    if (!bh3window.CheckWindow())
                    {
                        continue;
                    }
                    bool isStart = bh3window.IsStart();
                    Thread.Sleep(10);
                    if (isStart)
                    {
                        ScriptStartEvent();
                        currentScript.Start();
                        // 阻塞，等待脚本打完
                        while (currentScript.running) ;
                    }
                    else
                    {
                        ScriptStopEvent();
                    }
                }
            }));
            mainThread.Start();
            new Thread(new ThreadStart(CheckRunning)).Start();
        }

        private void CheckRunning()
        {
            lostRuningCount = 0;
            while (running)
            {
                if (!bh3window.IsFighting())
                {
                    if (lostRuningCount >= 2)
                    {
                        currentScript.Stop();
                        currentScript.ReleaseKeyup();
                        Console.WriteLine("丢失");
                        Thread.Sleep(2000);
                    }
                    lostRuningCount += 1;
                }
                else
                {
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

            for(int i=0; i<scripts.Count; i++)
            {
                names[i] = scripts[i].name;
            }
            return names;
        }

        public void Stop()
        {
            running = false;
            currentScript.Stop();
        }
    }
}
