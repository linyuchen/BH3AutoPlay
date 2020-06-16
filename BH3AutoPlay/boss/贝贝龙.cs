
namespace BH3AutoPlay.boss
{
    class AutoPlay鬼圣迅vs贝贝龙31573up37888 : AutoPlayScript
    {
        public AutoPlay鬼圣迅vs贝贝龙31573up37888(BH3Window bh3window) : base(bh3window)
        {
            name = "鬼圣迅vs贝贝龙31573(up37888)";
            videoUrl = "https://www.bilibili.com/video/BV1Tt4y117fn";
            description = @"
1p 3s鬼铠 重磁暴 叶叶牛
2p 圣女 鬼角 糖泳牛
3p 迅雷 胧光 莫石石 面板1440
冰人偶， 游戏需要调整60帧
";
        }

        private void 迅雷ab()
        {

            Keypress("j");
            Delay(300);
            Keypress("i");
            Delay(300);
        }

        public override void Action()
        {

            Delay(200);
            // 切圣女 往 龙落地方向跑
            Keypress("1");
            KeydownThenUp("s", 800);
            Delay(1000);
            Keydown("d");
            Keypress("k");
            Delay(600);
            Keyup("d");
            Keypress("l"); // 冰人偶
            Delay(500);

            // A三下
            Keypress("j");
            Delay(350);
            Keypress("j");
            Delay(350);
            Keypress("j");
            Delay(400);

            // 开大，闪避取消后摇
            Keypress("i");
            Delay(1900);

            // A三下
            Keypress("j");
            Delay(400);
            Keypress("j");
            Delay(400);
            //Keypress("j");
            //Delay(300);
            Keydown("s");
            Keypress("k");
            Delay(700);
            Keyup("s");
            Keypress("u"); // 发射鬼角延迟泳牛
            Delay(500);

            // 切鬼铠放武器主动
            Keypress("2");
            Delay(200);
            Keypress("u");
            Delay(400);

            // 切迅雷
            Keypress("1");
            Delay(400);
            迅雷ab();
            Delay(100);
            Keydown("w");
            Keypress("u");  // 胧光主动
            Delay(600);
            迅雷ab();

            Keyup("w");
            Keydown("a");
            Keypress("k");
            Delay(200);
            Keyup("a");
            KeydownThenUp("i", 1200);
            Delay(900);
            迅雷ab();
            Delay(200);
            迅雷ab();
            //Delay(200);
            Keypress("k");
            Delay(400);
            Keyup("w");
            迅雷ab();
            Delay(300);
            迅雷ab();
        }

        protected override void OnCheckTime(double runningSecond)
        {
            if (20 > runningSecond && runningSecond > 19)
            {
                if (bh3window.CheckBossAlive())
                {
                    Restart();
                }
            }
            else if (15 > runningSecond && runningSecond >= 14.5)
            {
                // 判断第一波大招能否开出来打成冰龙
                if (!bh3window.CheckColor(bh3window.healthPos, bh3window.HEALTH_COLOR_GREEN))
                {
                    Restart();
                }
            }
        }
    }
}
