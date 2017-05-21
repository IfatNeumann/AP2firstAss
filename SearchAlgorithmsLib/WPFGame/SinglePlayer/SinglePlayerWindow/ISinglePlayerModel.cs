using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    using MazeLib;

    public interface ISinglePlayerModel : INotifyPropertyChanged
    {
        string MazeName { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        event ApplicationSinglePlayerModel.test hip ;

        string StringMaze { get; set; }

        Point CurrPoint { get; set; }

        string Solution { get; set; }

        void KeyPressed(char direction);
        void StartGame();
    }
}