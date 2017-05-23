using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    public interface IMultiPlayerModel : INotifyPropertyChanged
    {
        bool NotReady { get; set; }
        string MazeName { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        string StringMaze { get; set; }

        List<string> List { get; }

        Point CurrPoint { get; set; }

        Point SecondCurrPoint { get; set; }

        Point EndPoint { get; }

        string Solution { get; set; }

        int KeyPressed(char direction);

        void StartConnection();

        void StartGame();

        void JoinGame();

        List<string> GetList();
    }
}
