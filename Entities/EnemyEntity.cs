using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

using Image = Windows.UI.Xaml.Controls.Image;

namespace DodgeGame.Shared.Entities
{
    internal class EnemyEntity : BasicEntity
    {
        public EnemyEntity(Point position, double speed, double size, BoardManager game) : base(position, speed, size, game)
        {

        }
        public override void Update()
        {
            Point PositionPlayer=Game.getPlayerPosition();
            double x = Position.X ;
            double y = Position.Y ;
            if (PositionPlayer.X > Position.X ) 
            {
                if (x < Game.Width - Size / 2 - 1)
                    x = Position.X + Speed;
            }
            else if(PositionPlayer.X < Position.X )
            {
                if (x > 0 + Size / 2 +1)
                    x = Position.X - Speed;
            }
            if (PositionPlayer.Y > Position.Y)
            {
                if (y < Game.Height - Size / 2 - 1)
                    y = Position.Y + Speed;
            }
            else if (PositionPlayer.Y < Position.Y)
            {
                if (y > 0 + Size / 2 +1)
                    y = Position.Y - Speed;
            }
            
            Position = new Point(x, y);
        }
    }
}
