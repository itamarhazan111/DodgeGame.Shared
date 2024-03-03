using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;

namespace DodgeGame.Shared
{
    static class InputProcessor
    {
        public static bool GoingUp { get; private set; }
        public static bool GoingDown { get; private set; }
        public static bool GoingRight { get; private set; }
        public static bool GoingLeft { get; private set; }
        public static bool DoubleSpeed { get; private set; }
        public static int count { get; private set; }
        public static async void OnKeyDown(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Up:
                    GoingUp = true;
                    break;
                case VirtualKey.Down:
                    GoingDown = true;
                    break;
                case VirtualKey.Left:
                    GoingLeft = true;
                    break;
                case VirtualKey.Right:
                    GoingRight = true;
                    break;
                case VirtualKey.W:
                    if (count > 0) {
                        DoubleSpeed = true;
                        MediaManager.speedExtra();
                        count--;
                        await Task.Delay(300);
                        DoubleSpeed = false;
                       
                    }
                    break;
            }
        }
        public static void OnKeyUp(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Up:
                    GoingUp = false;
                    break;
                case VirtualKey.Down:
                    GoingDown = false;
                    break;
                case VirtualKey.Left:
                    GoingLeft = false;
                    break;
                case VirtualKey.Right:
                    GoingRight = false;
                    break;
            }

        }
       
        public static void newGame(){
            count=3;
        }
        public static void overGame()
        {
            count = 0;
        }
        public static void loadGame(int load)
        {
            count = load;
        }
    }
}
