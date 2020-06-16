using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BH3AutoPlay.boss
{

    class AutoPlay鬼圣轮vs皮皮马31733up38080 : AutoPlayScript
    {
        private bool eatSp1 = false;
        private bool eatSp2 = false;
        public AutoPlay鬼圣轮vs皮皮马31733up38080(BH3Window bh3window) : base(bh3window)
        {
            name = "鬼圣轮vs皮皮马31733(up38080)";
            videoUrl = "https://www.bilibili.com/video/BV1Q7411p7mN";
            description = @"
1p 鬼铠：雷切 叶叶牛
2p 2s/3s月轮：胧光 莫迪牛
3p 圣女：鬼角 黑泳贝 或者上位 奥吉尔、特斯拉乐队
人偶仿犹大，徽章万氪章
";
        }

        public override void OnRestart()
        {
            this.eatSp1 = this.eatSp2 = false;
        }

        protected override void OnCheckTime(double runningSecond)
        {
            if (runningSecond > 9 && runningSecond < 10)
            {
                if (!bh3window.CheckShieldIsDestroy())
                {
                    Restart();
                }
            }
            else if (runningSecond > 22)
            {
                if (bh3window.CheckBossAlive())
                {
                    Restart();
                }
            }

            // 血条顶端见蓝血暂停吃sp包
            if (bh3window.CheckColor(bh3window.healthPos, bh3window.HEALTH_COLOR_BLUE) && !eatSp1)
            {
                eatSp1 = true;
                Pause();
            }
            else if (bh3window.CheckColor(bh3window.healthPos, bh3window.HEALTH_COLOR_ORANGE) && !eatSp2)
            {
                eatSp2 = true;
                Pause();
            }
        }

        private void 欧拉(int n)
        {
            Keypress("j");
            Delay(200);
            Keypress("j");
            Delay(200);
            Keypress("j");
            Delay(100);


            Keypress("i");
            Delay(200);
            Keypress("i");
            Delay(200);


            int i = 1;
            while (i <= n)
            {
                Keypress("i");
                Delay(250);
                i++;
            }
        }

        private void 欧拉1()
        {
            欧拉(5);
            Keypress("u");
            Delay(400);

        }

        private void 欧拉2()
        {
            Delay(300);
            欧拉(8);
        }

        public override void Action()
        {
            Delay(300);
            // 鬼凯往前一步到皮皮马出场位置
            Keydown("w");
            Keypress("k");
            Delay(300);

            // 切月轮
            Keypress("1");
            Delay(100);
            Keyup("w");
            // 放大招
            Keydown("i");
            Delay(2000);
            Keyup("i");

            // 切圣女
            Keydown("d");
            Keypress("1");
            Delay(1250);

            Keypress("k");
            Delay(200);

            Keypress("i"); // 放大招
            Delay(700);
            Keypress("l"); // 人偶破盾
            Delay(800);

            Keypress("k");

            Delay(700);
            Keyup("d");
            //Keypress "k", 1// 闪避一下远离皮皮马，免得鬼凯下来砸掉叶牛



            // 切鬼凯
            Keydown("a");

            Keypress("1");
            Delay(400);
            Keyup("a");
            Delay(900); // 等一下人偶下落


            Keypress("u");
            Keypress("j"); // 人偶砸下来之前A一下上牛
            Delay(350);

            Keypress("1");
            Delay(800);
            欧拉1();
            欧拉2();
        }
    }
    class AutoPlay律山紫vs皮皮马31626up37952 : AutoPlayScript
    {

        public AutoPlay律山紫vs皮皮马31626up37952(BH3Window bh3window) : base(bh3window)
        {
            name = "律山紫vs皮皮马31626(up37952)";
            videoUrl = "https://www.bilibili.com/video/BV1JV411d7KW";
            description = @"
1p: 3s女王 灵枪八重 特贝牛
2p: 山吹 超限百手 毕毕牛
3p: 3s紫苑 极夜 猫卡卡
人偶仿犹大
";
        }

        private void 连续平A(int count)
        {
            int tmp = 0;
            while (tmp < count)
            {
                Keypress("j");
                Delay(150);
                tmp += 1;
            }

        }

        public override void Action()
        {
            // 女王往前闪避
            Keydown("w");
            Keypress("k");
            Delay(300);
            Keyup("w");

            // 切紫苑放十字架
            Keypress("2");
            Delay(200);
            Keydown("d");
            Keypress("u");
            Keyup("d");
            Delay(200);

            // A三下后闪避
            Keydown("w");
            连续平A(14);
            Keyup("w");

            Keypress("k");
            Delay(1000);
            Keypress("j");
            Delay(500);
            Keypress("j");
            Delay(500);

            // 切山吹
            Keypress("1");
            Delay(800);
            Keypress("j");
            Delay(700);

            // 盾反
            KeydownThenUp("k", 400);
            Delay(700);
            // A两下放武器
            Keypress("j");
            Delay(1300);
            Keypress("j");
            Keypress("l");
            Delay(200);
            Keypress("u");
            Delay(300);
            // 切女王qte
            Keypress("1");
            Delay(1600);
            // 女王闪避控住皮皮马
            Keydown("a");
            Keypress("k");
            Delay(600);
            Keyup("a");

            // 切紫苑qte
            Keypress("1");
            Delay(400);
            KeydownThenUp("j", 1000);  // 蓄力进入沸血状态

            连续平A(27);
            Delay(200);

            KeydownThenUp("j", 2000);  // 蓄力一击结束游戏

        }

        protected override void OnCheckTime(double runningSecond)
        {
            if (25 > runningSecond && runningSecond > 24)
            {
                if (bh3window.CheckBossAlive())
                {
                    Restart();
                }
            }
            else if (16 > runningSecond && runningSecond >= 15.5)
            {
                // 判断第一波蓄力是否真猫
                if (bh3window.CheckColor(bh3window.healthPos, bh3window.HEALTH_COLOR_PURPLE))
                {
                    Restart();
                }
            }
        }
    }

}
