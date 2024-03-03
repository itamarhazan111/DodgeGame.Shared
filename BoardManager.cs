using DodgeGame.Shared.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace DodgeGame.Shared
{
    internal class BoardManager
    {

        private Random _rnd = new Random();

       

        public BasicEntity[] Entities { get; private set; }
        public int enemyCollitions { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public bool PlayerIsDead { get; private set; }


        public BoardManager(int entityCount, double width, double height)
        {
            Entities = new BasicEntity[entityCount];
            Width = width;
            Height = height;

        }

        public void Restart()
        {
            enemyCollitions = 0;
            PlayerIsDead = false;
            double sizePlayer = _rnd.Next(10, 20);
            double x = _rnd.NextDouble() * (Width- sizePlayer) + sizePlayer/2;
            double y = _rnd.NextDouble() * (Height- sizePlayer) + sizePlayer/2;
            Point position = new Point(x, y);
            Entities[0] = new PlayerEntity(position, 2, sizePlayer, this);

            for (int i = 1; i < Entities.Length; i++)
            {
                double enemySize = _rnd.Next(15,50);
                x = _rnd.NextDouble() * (Width- enemySize) + enemySize/2;
                y = _rnd.NextDouble() * (Height- enemySize) + enemySize / 2;
                position = new Point(x, y);
                
                while (!CheckNewPoint(position, enemySize)){
                    x = _rnd.NextDouble() * (Width- enemySize ) + enemySize / 2;
                    y = _rnd.NextDouble() * (Height- enemySize ) + enemySize / 2;
                    position = new Point(x, y);
                }
                Entities[i] = new EnemyEntity(position, 1, enemySize, this);
            }
        }
        public bool CheckNewPoint(Point point,double size)
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] == null)
                    continue;
                if (Entities[i].Collision(point,size))
                {
                    return false;
                }
                
            }
            return true;
        }
        public Point getPlayerPosition()
        {
            return Entities[0].Position;
        }

        public void Update()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                BasicEntity ent = Entities[i];
                if (ent == null)
                    continue;

                ent.Update();
            }
            checkCollinsEnemy();
            checkCollinsPlayer();
            enemyDeadCount();
        }
        public void checkCollinsPlayer()
        {
            BasicEntity player = Entities[0];
            for (int i = 1; i < Entities.Length; i++)
            {
                BasicEntity ent = Entities[i];
                if (player == null|| ent == null)
                    continue;
                if (player.Collision(ent.Position, ent.Size))
                {
                    PlayerIsDead= true;
                    Entities[0] = null;
                }
            }
        }
        public void checkCollinsEnemy()
        {
            for (int i = 1; i < Entities.Length; i++)
            {
                BasicEntity player = Entities[i];
                if (player == null)
                    continue;
                for (int j =i+1 ; j < Entities.Length; j++)
                {

                    BasicEntity ent = Entities[j];
                    if (ent == null)
                        continue;
                    if (player.Collision(ent.Position,ent.Size))
                    {
                        Entities[j] = null;
                        MediaManager.playEnemyDead();
                    }
                }
            }
        }
        public void enemyDeadCount()
        {
            int count = 0;
            for(int i = 1; i<Entities.Length; i++ ){
                if (Entities[i] == null)
                    count++;
            }
            enemyCollitions= count;
        }
        
    }
}
