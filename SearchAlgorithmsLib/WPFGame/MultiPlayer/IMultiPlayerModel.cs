using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;

    public interface IMultiPlayerModel : INotifyPropertyChanged
    {
        bool NotReady { get; set; }
        string MazeName { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        string StringMaze { get; set; }

        ObservableCollection<string> GamesList { get; set; }

        Point CurrPoint { get; set; }

        Point SecondCurrPoint { get; set; }

        Point EndPoint { get; }

        string Solution { get; set; }
        string CloseReason { get; set; }

        int KeyPressed(char direction);

        int SecPlayerKeyPressed(char direction);

        void StartConnection();

        void StartGame();

        void JoinGame();

        void CloseGame();

        void GetList();
    }
}
