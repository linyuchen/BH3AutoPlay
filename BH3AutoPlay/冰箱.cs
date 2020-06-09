using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH3AutoPlay
{
    class AutoPlay墨炎真VS冰箱31680up38016 : AutoPlayScript
    {
     
        public AutoPlay墨炎真VS冰箱31680up38016(BH3Window bh3Window): base(bh3Window)
        {
            name = "墨炎真VS冰箱31680(up38016)";
            description = @"
1p 云墨  叶叶牛
2P 真红 劫灭 列泰列
3p 炎八 超限妖刀 奥泳牛，单s真红需要换糖
人偶小苍玄
";
            videoUrl = "https://www.bilibili.com/video/BV175411x7JQ";
        }


        protected override void OnCheckTime(double runningSecond)
        {

            if (15 > runningSecond && runningSecond > 14.5)
            {
                if (bh3window.CheckBossAlive())
                {
                    Restart();
                }
            }
       }

        public override void Action()
        {
            Delay(200);
            // 开局云墨打两阴
            Keypress("i");

            Delay(400);
            Keypress("i");
            Delay(300);

            // 切真红蓄力+大招
            Keypress("1");
            Delay(250);
            KeydownThenUp("j", 1550);
            Keydown("i");
            //Keydown("e");
            Delay(200);
            //Keyup("e");
            Delay(1800 + 300);
            // 切云墨闪避，打三阴上叶牛
            Keypress("2");
            Delay(200);
            Keydown("s");
            Keypress("k");
            Delay(200);
            Delay(700);
            Keypress("k");
            Delay(600);
            Keypress("i");
            Delay(100);
            Keypress("i");
            Delay(300);
            Keyup("s");
            Delay(200);
            Keydown("w");
            Delay(200);
            Keypress("k");
            KeydownThenUp("j", 900);
            Delay(200);

            // 切炎八，武器主动
            Keypress("1");
            Delay(200);
            //Keypress("k");
            //Delay(300);
            Keypress("u");
            Delay(500);
            Keypress("l"); // 人偶
            Delay(800);


            // 真红劫灭发动NB的攻击收尾
            Keypress("1");
            Delay(500); // qte要等500ms
            Keypress("u");
            Keyup("w");
        }

    }
}
