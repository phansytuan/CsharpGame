using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestEscape
{
    public abstract class GameObject
    {
        private Bitmap _bitmap;
        private Point2D _location;
        private Vector2D _velocity;
        private Rectangle _bounds;
        private double _speed;
        public GameObject(int x, int y)
        {
            Location = SplashKit.PointAt(x, y);
        }
        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }

        }
        public Point2D Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Vector2D Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Rectangle Bounds
        { 
            get { return _bounds; } 
            set { _bounds = value; }
        }

        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void Draw()
        {
            Bitmap.Draw(Location.X, Location.Y);
        }
        public virtual void Update(bool gameOver)
        {
            Location = SplashKit.PointAt(Location.X + Velocity.X, Location.Y + Velocity.Y);
        }
        public virtual void Restart() { }
        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteLine(Velocity.X);
            writer.WriteLine(Velocity.Y);
            writer.WriteLine(Location.X);
            writer.WriteLine(Location.Y);
        }
        public virtual void LoadFrom(StreamReader reader)
        {
            double x = reader.ReadSingle();
            double y = reader.ReadSingle();
            Velocity = SplashKit.VectorTo(x, y);
            double a = reader.ReadSingle();
            double b = reader.ReadSingle();
            Location = SplashKit.PointAt(a, b);
        }
    }
}
