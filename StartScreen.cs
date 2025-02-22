using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ForestEscape
{
    public class StartScreen
    {
        private Font _font = new Font("time new roman", "times new roman.otf");
        public void Draw()
        {
            SplashKit.FillRectangle(SplashKitSDK.Color.WhiteSmoke,300,150, 400, 300);
            SplashKit.DrawText("Help Screen", SplashKitSDK.Color.DarkRed, "time new roman", 30, 430, 160);
            SplashKit.DrawText("Welcome to Forest Escape!", SplashKitSDK.Color.OrangeRed, "time new roman", 25, 360, 195);
            SplashKit.DrawText(">>Click the left mouse to jump<<", SplashKitSDK.Color.Black, "time new roman", 20, 350, 255);
            SplashKit.DrawText(">>Press Space to pause/continue the game<<", SplashKitSDK.Color.Black, "time new roman", 20, 310, 280);
            SplashKit.DrawText(">>Press R to replay the game<<", SplashKitSDK.Color.Black, "time new roman", 20, 365, 305);
            SplashKit.DrawText(">>Press S to save the game<<", SplashKitSDK.Color.Black, "time new roman", 20, 365, 330);
            SplashKit.DrawText(">>Press L to load the last saved game<<", SplashKitSDK.Color.Black, "time new roman", 20, 335, 355);

        }
    }
}
