using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestEscape
{
    public class Tile: GameObject
    {
        private bool _tileup;
       public Tile(int x, int y, bool tileup) : base(x, y)
       {
            _tileup = tileup;

            if(tileup)
            {
                Bitmap = new Bitmap("tileup", "tileup.png");
            }
            else
            {
                Bitmap = new Bitmap("tiledown", "tiledown.png");
            }
       }
    }
}
