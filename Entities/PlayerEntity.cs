using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Image = Windows.UI.Xaml.Controls.Image;

namespace DodgeGame.Shared.Entities
{
    internal class PlayerEntity : BasicEntity
    {

    public PlayerEntity(Point position, double speed, double size, BoardManager game) : base(position, speed, size,game)
        {
            
        }

        public override void Update()
        {
            double x = Position.X;
            double y = Position.Y;

            if(InputProcessor.GoingRight)
            {
                if (x < Game.Width - Size / 2 - 1)
                {
                    if (InputProcessor.DoubleSpeed)
                        x += Speed;
                    x += Speed;
                }
            }
            if (InputProcessor.GoingLeft)
            {
                if (x > 0 + Size / 2 + 1) {
                    if (InputProcessor.DoubleSpeed)
                        x -= Speed;
                    x -= Speed;
                }
            }
            if (InputProcessor.GoingUp)
            {
                if (y > 0 + Size / 2 + 1) {
                    if (InputProcessor.DoubleSpeed)
                        y -= Speed;
                    y -= Speed;
                }
            }
            if (InputProcessor.GoingDown)
            {
                if (y < Game.Height - Size / 2 -1)
                {
                    if (InputProcessor.DoubleSpeed)
                        y += Speed;
                    y += Speed;
                }
            }   
            Position=new Point(x,y);

        }
    }

}

