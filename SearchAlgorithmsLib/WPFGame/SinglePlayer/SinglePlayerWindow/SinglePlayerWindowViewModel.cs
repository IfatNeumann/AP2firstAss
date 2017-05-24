using System;
using System.ComponentModel;
using System.Windows;

namespace WPFGame
{
    /// <summary>
    /// the view model of the application
    /// </summary>
    /// <seealso cref="WPFGame.ViewModel" />
    public class SinglePlayerWindowViewModel : ViewModel
    {
        /// <summary>
        /// The model of the single player
        /// </summary>
        private ISinglePlayerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindowViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerWindowViewModel(ISinglePlayerModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
                {
                    this.NotifyPropertyChanged("Vm" + e.PropertyName);
                };
        }

        /// <summary>
        /// Gets or sets the name of the vm maze.
        /// </summary>
        /// <value>
        /// The name of the vm maze.
        /// </value>
        public string VmMazeName
        {
            get
            {
                return this.model.MazeName;
            }

            set
            {
                this.model.MazeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public string VmMazeRows
        {
            get
            {
                return this.model.MazeRows.ToString();
            }

            set
            {
                if (this.model.MazeRows.ToString() != value)
                {
                    this.model.MazeRows = int.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the vm maze cols.
        /// </summary>
        /// <value>
        /// The vm maze cols.
        /// </value>
        public string VmMazeCols
        {
            get
            {
                return this.model.MazeCols.ToString();
            }

            set
            {
                if (this.model.MazeCols.ToString() != value)
                {
                    this.model.MazeCols = int.Parse(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the vm string maze.
        /// </summary>
        /// <value>
        /// The vm string maze.
        /// </value>
        public string VmStringMaze
        {
            get
            {
                return this.model.StringMaze;
            }

            set
            {
                this.model.StringMaze = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm curr point.
        /// </summary>
        /// <value>
        /// The vm curr point.
        /// </value>
        public string VmCurrPoint
        {
            get
            {
                return this.model.CurrPoint.ToString();
            }

            set
            {
                this.model.CurrPoint = Point.Parse(value);
            }
        }

        /// <summary>
        /// Gets the vm end point.
        /// </summary>
        /// <value>
        /// The vm end point.
        /// </value>
        public string VmEndPoint
        {
            get
            {
                return this.model.EndPoint.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the vm solution.
        /// </summary>
        /// <value>
        /// The vm solution.
        /// </value>
        public string VmSolution
        {
            get
            {
                return this.model.Solution;
            }

            set
            {
                if (this.model.Solution != value)
                {
                    this.model.Solution = value;
                }
            }
        }

        /// <summary>
        /// Keys the pressed.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public int KeyPressed(char direction)
        {
            return this.model.KeyPressed(direction);
        }

        /// <summary>
        /// Initializes the start position.
        /// </summary>
        public void InitStartPos()
        {
            this.model.InitStartPos();
        }
    }
}
