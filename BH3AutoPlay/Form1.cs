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

        AutoPlayScriptManager scriptManager;
        public Form1()
        {
            InitializeComponent();
            scriptManager = new AutoPlayScriptManager(this);
            scriptManager.ScriptStartEvent += () => {

                try
                {
                    this.label2.Invoke(new MethodInvoker(() =>
                    {
                        this.label2.Text = "已开始";
                    }));
                }
                catch { }
            };
            scriptManager.ScriptStopEvent += () => {

                try
                {
                    this.label2.Invoke(new MethodInvoker(() =>
                    {
                        this.label2.Text = "未开始";
                    }));
                }
                catch { }
            };
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.AddRange(scriptManager.GetScriptNames());
            this.comboBox1.SelectedIndex = 0;
            this.Text = "BH3AutoPlay V" + Application.ProductVersion;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            scriptManager.Stop();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(scriptManager.currentScript.videoUrl);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/linyuchen/BH3AutoPlay");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://jq.qq.com/?_wv=1027&k=5JUqSjB");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            scriptManager.SetScript(this.comboBox1.SelectedIndex);
            this.labelScriptDesc.Text = this.scriptManager.currentScript.description;
        }
    }
}
