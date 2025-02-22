using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ForestEscape
{
    public class GameObstacle : GameObject
    {
        private IObstacleDrawingStrategy _drawingStrategy;
        public GameObstacle(int x, int y): base(x, y)
        {
            Speed = -0.35;
            Velocity = SplashKit.VectorTo(Speed, 0);
            
        }
        public GameObstacle() : base(0, 0)
        {
            Speed = -0.35;
            Velocity = SplashKit.VectorTo(Speed, 0);

        }
        public void SetDrawingStrategy(IObstacleDrawingStrategy strategy)
        {
            _drawingStrategy = strategy;
        }

        public override void Draw()
        { 
            if (_drawingStrategy != null)
            {
                _drawingStrategy.Draw(this);
            }
            else
            {
                Console.WriteLine("Invalid");
            }

        }

        public override void Update(bool gameOver)
        {
            Location = SplashKit.PointAt(Location.X + Velocity.X, Location.Y + Velocity.Y);
            if (Location.Y == 37 || Location.Y == 480)
            {
                Bounds = SplashKit.RectangleFrom(Location.X+5,Location.Y+5, 60, 85);
            }
            else
            {
                Bounds = SplashKit.RectangleFrom(Location.X + 12, Location.Y + 11, 40, 25);
            }
            Velocity = SplashKit.VectorTo(Speed, 0);
            if (gameOver)
            {
                Velocity = SplashKit.VectorTo(0, 0);
            }
        }
        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Obstacle");
            writer.WriteLine(_drawingStrategy.Name);
            base.SaveTo(writer);
        }

        public override void LoadFrom(StreamReader reader)
        {  
            string strategy = reader.ReadLine();
            if (strategy == "Ground")
            {
                _drawingStrategy = new GroundObstacleDrawing();
            }
            else
            {
                _drawingStrategy = new FlyingObstacleDrawing();
            }
            base.LoadFrom(reader);

        }

    }
}