using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BH3AutoPlay
{
    class AutoPlayYueLun: AutoPlay
    {

        public void 山蓝粉37760非up31466()
        {
            // 1p 山吹，2p 蓝毛，3p 粉毛
            // 山吹往前走一秒
            Keydown("w");
            Sleep(700);

            Keydown("j");
            Sleep(1300);
            Keyup("j");
            //KeydownThenUp("k", 300);
            Sleep(200);
            //蓄力0.7秒
            Keydown("j");
            Sleep(1600);
            Keyup("j");
            Sleep(450);
            Keypress("j");
            Sleep(600);
            Keyup("w");
            //盾反
            KeydownThenUp("k", 300);
            Sleep(500);
            // 平A两下
            Keypress("j");
            Sleep(300);
            // 人偶
            Keypress("l");
            Sleep(200);
            Keypress("j");
            Sleep(600);

            // 武器主动
            Keypress("u");
            Sleep(1000);

            // 切蓝毛武器主动
            Keypress("1");
            Sleep(300);
            Keypress("u");
            Sleep(700);

            // 切粉毛
            Keypress("1");
            Sleep(200);
            KeydownThenUp("j", 5500);
            Sleep(200);
            Keypress("j");
            Sleep(1100);
            Keypress("k");
            Sleep(250);
            KeydownThenUp("j", 850);
            Sleep(200);
            Keypress("j");
        }
    }
}
