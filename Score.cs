using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using ForestEscape;

namespace ForestEscape
{
    public class Score:GameObject
    {
        private int _point = 0;
        private int _record = 0;
        private Font _font = new Font("time new roman", "times new roman.otf");

        public Score(): base(50, 5) 
        { 

        }

        public int Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public int Record
        {
            get { return _record; }
            set { _record = value; }
        }

        public override void Draw()
        {
            SplashKit.DrawText("Score: " + _point, SplashKitSDK.Color.White, "time new roman", 20, Location.X, Location.Y);
            SplashKit.DrawText("Highest score: " + _record, SplashKitSDK.Color.White, "time new roman", 20, Location.X + 700, Location.Y);
        }
        public override void Update(bool gameOver)
        {
            if (gameOver)
            {
                if(_point > _record)
                {
                    _record = _point;
                }
                _point = 0;
            }
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Score");
            base.SaveTo(writer);
            writer.WriteLine(_point);
            writer.WriteLine(_record);
        }
        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            int score = reader.ReadInteger();
            int record = reader.ReadInteger();
            _record = record;
            _point = score;
        }
    }
}
