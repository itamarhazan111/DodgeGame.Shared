using DodgeGame.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace DodgeGame.Shared
{
    internal class GameManger
    {
        public BoardManager BoardGame { get;private set; }
        private int ENTITY_COUNT = 11;
        public bool Win { get; private set; }
        public bool Lose { get; private set; }
        public bool Running { get; private set; }
        public GameManger(double width,double height) {
            BoardGame = new BoardManager(ENTITY_COUNT, width, height);
            Win = false;
            Lose = false;
        }
        public void Save()
        {
            SaveManager.Save(BoardGame);
        }
        public async void Load(double width, double height)
        {
            BoardGame =await SaveManager.Load(width,height);
        }
        public void Update()
        {
            
            BoardGame.Update();
            GameMode();

        }
        public void Restart()
        {
            InputProcessor.newGame();
            BoardGame.Restart();

        }
        
        public void GameMode()
        {
            if(BoardGame.enemyCollitions< ENTITY_COUNT - 2&&!BoardGame.PlayerIsDead)
            {
                Running = true;
            }
             if (BoardGame.enemyCollitions == ENTITY_COUNT - 2)
            {
               
                Win = true;
                Running = false;
            }
            else if (BoardGame.PlayerIsDead)
            {
                
                Lose= true;
                Running = false;
            }
        }
    }
    
}