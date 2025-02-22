using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SplashKitSDK;
using ForestEscape;
using System.Formats.Asn1;

namespace ForestEscape
{
    public class Player:GameObject
    {
        private Bitmap _spriteSheet;
        private DrawingOptions opt;
        private Animation anm;

        public Player(): base(100, 300)
        {
            _spriteSheet = SplashKit.LoadBitmap("spritesheet", "spritesheet.png");
            _spriteSheet.SetCellDetails(64, 87, 8, 1, 8);
            AnimationScript animationScript = SplashKit.LoadAnimationScript("animationScript", "animationScript.txt");
            anm = animationScript.CreateAnimation("runForward");
            opt = SplashKit.OptionWithAnimation(anm);
            Speed = 0.3;
            Velocity = SplashKit.VectorTo(0, Speed);
        }

        public override void Draw()
        {
            _spriteSheet.Draw(Location.X, Location.Y, opt);
            anm.Update();
        }

        public override void Update(bool gameOver)
        {
            if(Location.Y <= 37 || Location.Y >= 483 || gameOver)
            {
                Velocity = SplashKit.VectorTo(0, 0);
            }

            if(!gameOver)
            {
                SoundEffect sound = SplashKit.LoadSoundEffect("jumping", "jumping.wav");
                if (Location.Y <= 40 && SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Velocity = SplashKit.VectorTo(0, Speed);
                    anm.Assign("runForward");
                    sound.Play();
                }
                else if (Location.Y >= 480 && SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Velocity = SplashKit.VectorTo(0, -Speed);
                    anm.Assign("reverse");
                    sound.Play();
                }
            }          
            Location = SplashKit.PointAt(Location.X + Velocity.X, Location.Y + Velocity.Y);
            Bounds = SplashKit.RectangleFrom(Location.X + 8,Location.Y, 56, 87);
        }

        public override void Restart()
        {
            Location = SplashKit.PointAt(100, 300);
            Velocity = SplashKit.VectorTo(0, Speed);
            anm.Assign("runForward");
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Player");
            base.SaveTo(writer);
            writer.WriteLine(anm.Name);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            string a = reader.ReadLine();
            anm.Assign(a);
        }
    }
}

