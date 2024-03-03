using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace DodgeGame.Shared
{
    internal static class MediaManager
    {
        static MediaPlayer background = new MediaPlayer();
        static MediaPlayer enemyDead = new MediaPlayer();
        static MediaPlayer win = new MediaPlayer();
        static MediaPlayer lose = new MediaPlayer();
        static MediaPlayer speed = new MediaPlayer();
        public static void CreateMusic()
        {
            background = new MediaPlayer();
            background.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Cipher2.mp3"));
            enemyDead = new MediaPlayer();
            enemyDead.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/deadEnemy.wav"));
            win = new MediaPlayer();
            win.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/win.mp3"));
            lose = new MediaPlayer();
            lose.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/lose.mp3"));
            speed = new MediaPlayer();
            speed.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/speed.mp3"));
        }
        public static void playEnemyDead()
        {

            enemyDead.Play();
            enemyDead = new MediaPlayer();
            enemyDead.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/deadEnemy.wav"));
        }
        public static void playBackground()
        {

            background.Play();
        }
        public static void PauseBackground()
        {
            background.Pause();
        }
        public static void playWinGame()
        {
            background.Pause();
            win.Play();

        }
        public static void playLoseGame()
        {
            background.Pause();
            lose.Play();

        }
        public async static void speedExtra()
        {

            speed.Play();
            speed = new MediaPlayer();
            speed.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/speed.mp3"));

        }
    }
}
