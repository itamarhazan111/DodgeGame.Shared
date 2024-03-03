using DodgeGame.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace DodgeGame.Shared
{
    static class SaveManager
    {
        const string FILE_NAME = "save.txt";

        public static async void Save(BoardManager toSave)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);

            string data = InputProcessor.count.ToString()+"-";
            for (int i = 0; i < toSave.Entities.Length; i++)
            {
                if (i == 0)
                {
                    PlayerEntity p = (PlayerEntity)toSave.Entities[i];
                    if (p!=null)
                        data += $"{p.Position.X};{p.Position.Y};{p.Speed};{p.Size}|";
                    else
                    {
                        data += $"|";
                    }
                }
                else
                {
                        EnemyEntity e = (EnemyEntity)toSave.Entities[i];
                    if (e!=null)
                        data += $"{e.Position.X};{e.Position.Y};{e.Speed};{e.Size}|";
                    else
                        data += $"|";
                }
            }

            await FileIO.AppendTextAsync(file, data);
        }

        public static async Task<BoardManager> Load(double width, double height)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(FILE_NAME);

            string raw = await FileIO.ReadTextAsync(file);
            string[] rawGame = raw.Split('-');
            string countInputProccesor=rawGame[0];
            InputProcessor.loadGame(int.Parse(countInputProccesor));
            string[] rawEntity = rawGame[1].Split('|');
            BoardManager manager = new BoardManager(rawEntity.Length-1, width, height);

            for (int i = 0; i < rawEntity.Length - 1; i++)
            {
                if (rawEntity[i]=="")
                    continue;
                string[] rawPersonData = rawEntity[i].Split(';');
                BasicEntity ent;
                if (i == 0)
                {
                    ent = new PlayerEntity(new Point(double.Parse(rawPersonData[0]), double.Parse(rawPersonData[1])), double.Parse(rawPersonData[2]), double.Parse(rawPersonData[3]), manager);
                }
                else 
                {
                    ent = new EnemyEntity(new Point(double.Parse(rawPersonData[0]), double.Parse(rawPersonData[1])), double.Parse(rawPersonData[2]), double.Parse(rawPersonData[3]), manager);
                }
                manager.Entities[i] = ent;
            }

            return manager;
        }
    }
}
