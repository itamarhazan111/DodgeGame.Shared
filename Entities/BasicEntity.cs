using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media.Imaging;
using Image = Windows.UI.Xaml.Controls.Image;
using Windows.UI.Xaml.Media;

namespace DodgeGame.Shared.Entities
{
    internal abstract class BasicEntity
    {
        public Point Position { get; protected set; }
        public double Speed { get; protected set; }
        public double Size { get; protected set; }
        public BoardManager Game { get; protected set; }

        public BasicEntity(Point position, double speed, double size,BoardManager game)
        {
            Position = position;
            Speed = speed;
            Size = size;
            Game = game;
        }

        public virtual bool Collision(Point position,double size)
        {
            double distance = Math.Sqrt((Math.Pow((this.Position.X- position.X), 2) + Math.Pow((this.Position.Y - position.Y), 2)));
            if (distance < Size/2+size/2 )
            {
                return true;
            }
            return false;
        }
        public virtual void Update()
        {
        }

    }
}
