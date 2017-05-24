using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// the view model of the multiplayer window
    /// </summary>
    /// <seealso cref="WPFGame.ViewModel" />
    class MultiPlayerWindowViewModel : ViewModel
    {

        /// <summary>
        /// The model
        /// </summary>
        private IMultiPlayerModel model;

        /// <summary>
        /// type that represents references to the method
        /// </summary>
        /// <param name="reason">The reason.</param>
        public delegate void NotifyViewThatPropertyChanged(string reason);

        /// <summary>
        /// Occurs when [closing happend].
        /// </summary>
        public event NotifyViewThatPropertyChanged ClosingHappend;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindowViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerWindowViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName.Equals("CloseReason"))
                    {
                        this.ClosingHappend?.Invoke(this.VmCloseReason);
                    }
                    else
                    {
                        this.NotifyPropertyChanged("Vm" + e.PropertyName);
                    }
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
        /// Gets or sets the vm second curr point.
        /// </summary>
        /// <value>
        /// The vm second curr point.
        /// </value>
        public string VmSecondCurrPoint
        {
            get
            {
                return this.model.SecondCurrPoint.ToString();
            }

            set
            {
                this.model.SecondCurrPoint = Point.Parse(value);
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
        /// Gets or sets the vm close reason.
        /// </summary>
        /// <value>
        /// The vm close reason.
        /// </value>
        public string VmCloseReason
        {
            get
            {
                return this.model.CloseReason;
            }

            set
            {
                this.model.CloseReason = value;
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
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            this.model.CloseGame();
        }
    }
}
