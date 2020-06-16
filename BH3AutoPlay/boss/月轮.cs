using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH3AutoPlay.boss
{
    class AutoPlay山粉蓝vs月轮37760非up31466 : AutoPlayScript
    {
        public AutoPlay山粉蓝vs月轮37760非up31466(BH3Window bh3window) : base(bh3window)
        {
            name = "山粉蓝vs月轮37760(非up31466)";
            description = @"
1p 山吹 超限百手 毕毕牛（无元素词缀）
2p 蓝毛 重剑王蛇 古古牛
3P 3s粉毛 睡美人 卡卡米（有元素词缀，面板1343）
人偶仿犹大
";
            videoUrl = "https://www.bilibili.com/video/BV1mZ4y1W7Ez";
        }


        protected override void OnCheckTime(double runningSecond)
        {
            if (runningSecond > 25)
            {
                Restart();
            }
            else if (22 > runningSecond && runningSecond > 19)
            {
                if (bh3window.CheckBossAlive())
                {
                    Restart();
                }
            }
            else if (12 > runningSecond && runningSecond > 10)
            {
                if (!bh3window.CheckShieldIsDestroy())
                {
                    Restart();
                }
            }
            if (15 > runningSecond && runningSecond > 13)
            {
                Keypress("u");
            }

        }

        public override void Action()
        {
            // 1p 山吹，2p 蓝毛，3p 粉毛
            // 山吹往前走一秒
            Keydown("w");
            Delay(700);

            Keydown("j");
            Delay(1300);
            Keyup("j");
            //KeydownThenUp("k", 300);
            Delay(200);
            //蓄力0.7秒
            Keydown("j");
            Delay(1600);
            Keyup("j");
            Delay(450);
            Keypress("j");
            Delay(600);
            Keyup("w");
            //盾反
            KeydownThenUp("k", 300);
            Delay(500);
            // 平A两下
            Keypress("j");
            Delay(300);
            // 人偶
            Keypress("l");
            Delay(200);
            Keypress("j");
            Delay(600);

            // 武器主动
            Keypress("u");
            Delay(1000);

            // 切蓝毛武器主动
            Keypress("1");
            Delay(300);
            Keypress("u");
            Delay(700);

            // 切粉毛
            Keypress("1");
            Delay(200);
            KeydownThenUp("j", 5500);
            Delay(200);
            Keypress("j");
            Delay(1100);
            Keypress("k");
            Delay(250);
            KeydownThenUp("j", 850);
            Delay(200);
            Keypress("j");
        }
    }

}
