using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BH3AutoPlay
{
    public partial class Form1 : Form
    {

        BH3Window bh3window = new BH3Window();
        AutoPlayYueLun autoPlay= new AutoPlayYueLun();
        DateTime startTime;
        bool running = true;
        bool autoPlayRunning = false;
        Thread listenerThread;
        Thread autoPlayThread = null;
        int lostRuningCount = 0;
        public Form1()
        {
            listenerThread = new Thread(new ThreadStart(this.chekStart));
            InitializeComponent();
        }

        private void RestartBH3()
        {
            this.autoPlayRunning = false;
            Console.WriteLine("重开");
            autoPlayThread.Abort();
            this.autoPlay.Restart(bh3window.restartBtnPos1, bh3window.restartBtnPos2);
            Thread.Sleep(2000);
        }

        // 检测是否还在战场内
        private void CheckRunning()
        {
            lostRuningCount = 0;
            while (this.autoPlayRunning)
            {
                if (!bh3window.IsStart())
                {
                    if (lostRuningCount >= 5)
                    {
                        this.autoPlayRunning = false;
                        autoPlayThread.Abort();
                        autoPlay.ReleaseKeyup();
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
        //检测流程终止或者结束
        private void CheckOver()
        {

            TimeSpan runingTime = DateTime.Now - this.startTime;
            double runingSecond = runingTime.TotalSeconds;
            if (runingTime.TotalSeconds > 25)
            {
                RestartBH3();
            }
            else if (22 > runingSecond && runingSecond > 19)
            {
                if (bh3window.CheckBossAlive())
                {
                    RestartBH3();
                }
            }
            else if (12 > runingSecond && runingSecond > 10)
            {
                if (!bh3window.ShieldIsDestroy())
                {
                    RestartBH3();
                }
            }
            if ( 15 > runingSecond && runingTime.TotalSeconds > 13)
            {
                autoPlay.Keypress("u");
            }
            Thread.Sleep(100);
        }

        private void chekStart()
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
                    startTime = DateTime.Now;
                    this.label2.Invoke(new MethodInvoker(() => {
                        this.label2.Text = "已开始";
                    }));
                    this.autoPlayRunning = true;
                    new Thread(new ThreadStart(CheckRunning)).Start();
                    autoPlayThread = new Thread(new ThreadStart(autoPlay.山蓝粉37760非up31466));
                    autoPlayThread.Start();
                    while (this.autoPlayRunning)
                    {
                        this.CheckOver();
                    }
                }
                else
                {
                    this.label2.Invoke(new MethodInvoker(() => {
                        this.label2.Text = "未开始";
                    }));
                }
            }            
        }
        public void listenerStart()
        {
            
            listenerThread.Start();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.running = true;
            this.listenerStart();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.running = false;
            listenerThread.Abort();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.bilibili.com/video/BV1mZ4y1W7Ez");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/linyuchen/BH3AutoPlay");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://jq.qq.com/?_wv=1027&k=5JUqSjB");
        }
    }
}
