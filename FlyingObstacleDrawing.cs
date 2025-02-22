using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestEscape
{
    public class FlyingObstacleDrawing: IObstacleDrawingStrategy
    {
        private string _name = "Fly";

        public void Draw(GameObstacle obstacle)
        {
            obstacle.Bitmap = new Bitmap("bat", "Bat.png");
            obstacle.Bitmap.Draw(obstacle.Location.X, obstacle.Location.Y);
        }
        public string Name { get { return _name; } }
    }
}
