using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestEscape
{
    public class GroundObstacleDrawing: IObstacleDrawingStrategy
    {
        private string _name = "Ground";
        public void Draw(GameObstacle obstacle)
        {
            if (obstacle.Location.Y == 37)
            {
                obstacle.Bitmap = new Bitmap("obstacledown", "man_eating_plant_rotate.png");
            }
            if (obstacle.Location.Y == 480)
            {
                obstacle.Bitmap = new Bitmap("obstacleup", "man_eating_plant.png");
            }
            obstacle.Bitmap.Draw(obstacle.Location.X, obstacle.Location.Y);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}