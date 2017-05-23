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

    using WPFGame.Properties;

    class ApplicationMultiPlayerModel : IMultiPlayerModel
    {
        private string name;

        private int rows;

        private int cols;

        private string stringMaze;

        private string solution;

        private Maze maze;

        private Point currPoint;

        private Point endPoint;

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationMultiPlayerModel()
        {
            this.rows = Settings.Default.MazeRows;
            this.cols = Settings.Default.MazeCols;
        }

        public string MazeName
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }

        public int MazeRows
        {
            get
            {
                return this.rows;
            }

            set
            {
                if (this.rows != value)
                {
                    this.rows = value;
                }
            }
        }

        public int MazeCols
        {
            get
            {
                return this.cols;
            }

            set
            {
                if (this.cols != value)
                {
                    this.cols = value;
                }
            }
        }

        public string StringMaze
        {
            get
            {
                return this.stringMaze;
            }

            set
            {
                this.stringMaze = value;
            }
        }

        public Point CurrPoint
        {
            get
            {
                return this.currPoint;
            }

            set
            {
                this.currPoint = value;
                this.NotifyPropertyChanged("CurrPoint");
            }
        }

        public Point EndPoint
        {
            get
            {
                int iEnd = this.maze.GoalPos.Row;
                int jEnd = this.maze.GoalPos.Col;
                this.endPoint = new Point(iEnd, jEnd);
                return this.endPoint;
            }
        }

        public string Solution
        {
            get
            {
                return this.solution;
            }

            set
            {
                if (this.solution != value)
                {
                    this.solution = value;
                }
            }
        }

        public void StartGame()
        {

        }

        public int KeyPressed(char direction)
        {
            return 1;
        }

        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
