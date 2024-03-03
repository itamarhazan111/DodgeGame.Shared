using DodgeGame.Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Drawing;
using Image = Windows.UI.Xaml.Controls.Image;
using Windows.Media.Core;
using Windows.Media.Playback;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DodgeGame.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GameManger _game;
        private DispatcherTimer _timer;
       // private MediaPlayer music;
        ImageBrush enemyGraphic = new ImageBrush();
        ImageBrush playerGraphic = new ImageBrush();
      




        public MainPage()
        {
            this.InitializeComponent();
           // music=new MediaPlayer();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; ;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp; ;
            MediaManager.CreateMusic();
            _game = new GameManger(GameCanvas.Width, GameCanvas.Height);
            playerGraphic.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/mario.png"));
            enemyGraphic.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/enemy.jpg"));
            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            InputProcessor.OnKeyUp(args.VirtualKey);


        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            InputProcessor.OnKeyDown(args.VirtualKey);
        }

        private void _timer_Tick(object sender, object e)
        {
            _game.Update();
            UpdateUI();
        }
        private void UpdateUI()
        {

            GameCanvas.Children.Clear();
            Score.Text = _game.BoardGame.enemyCollitions.ToString();
            Speed.Text=InputProcessor.count.ToString();
            for (int i = 0; i < _game.BoardGame.Entities.Length; i++)
            {
                BasicEntity ent = _game.BoardGame.Entities[i];
                if (ent == null)
                    continue;
                Ellipse graphic = new Ellipse();
                graphic=CreateGraphic(ent);

                graphic.Width = ent.Size;
                graphic.Height = ent.Size;
                

                Canvas.SetLeft(graphic, ent.Position.X - ent.Size / 2);
                Canvas.SetTop(graphic, ent.Position.Y - ent.Size / 2);

                GameCanvas.Children.Add(graphic);
            }
            if (_game.Win)
            {
                createEnd("ms-appx:///Assets/win.jpg");
                InputProcessor.overGame();
                MediaManager.playWinGame();
            
            }
            else if (_game.Lose)
            {
                createEnd("ms-appx:///Assets/lose.jpg");
                InputProcessor.overGame();
                MediaManager.playLoseGame();
            }

        }
        private void createEnd(string s)
        {
            _timer.Stop();
            GameCanvas.Children.Clear();
            Image graphic = new Image();
            graphic.Source = new BitmapImage(new Uri(s));
            graphic.Width = GameCanvas.Width;
            graphic.Height = GameCanvas.Height;

            Canvas.SetLeft(graphic, 0);
            Canvas.SetTop(graphic, 0);
            GameCanvas.Children.Add(graphic);
        }
        private Ellipse CreateGraphic(BasicEntity ent)
        {
            Ellipse graphic = new Ellipse();
           
            if (ent is PlayerEntity)
            {
                if (View.IsChecked.HasValue && View.IsChecked.Value)
                {
                    ImageBrush brush =playerGraphic;
                    graphic.Fill = brush;
                    return graphic;
                }

                graphic.Fill = new SolidColorBrush(Colors.Red);

                return graphic;
            }
            else
            {
                if (View.IsChecked.HasValue && View.IsChecked.Value)
                {
                    ImageBrush brush = enemyGraphic;
                    graphic.Fill = brush;
                    return graphic;
                }

                graphic.Fill = new SolidColorBrush(Colors.Green);

                return graphic;
            }


        }


        private void Stop1_Click(object sender, RoutedEventArgs e)
        {
            if (_game.Running) { 
                _timer.Stop();
                UpdateUI();
            }
            _timer.Stop();
        }

        private void Resume1_Click(object sender, RoutedEventArgs e)
        {
            if (_game.Running)
            {
                _timer.Start();
               
            }
           
        }

        private void Music1_Click(object sender, RoutedEventArgs e)
        {
            musicBackground();

        }
        private void musicBackground(){
            if (Music1.IsChecked.HasValue && Music1.IsChecked.Value)
            {
                MediaManager.playBackground();

            }
            else
            {
                MediaManager.PauseBackground();

            }
        }

        private void Start1_Click(object sender, RoutedEventArgs e)
        {
           
            _game = new GameManger(GameCanvas.Width, GameCanvas.Height);
            _game.Restart();

            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            _timer.Start();
            musicBackground();
            UpdateUI();

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            GameCanvas.Children.Clear();
            _game = new GameManger(GameCanvas.Width, GameCanvas.Height);
            _timer.Stop();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            _game = new GameManger(GameCanvas.Width, GameCanvas.Height);
            _game.Load(GameCanvas.Width, GameCanvas.Height);
            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            _timer.Start();

            UpdateUI();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(_game.Running)
            _game.Save();
        }

    }
}
