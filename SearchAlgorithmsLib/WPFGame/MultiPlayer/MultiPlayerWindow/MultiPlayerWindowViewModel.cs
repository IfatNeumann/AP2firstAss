using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    class MultiPlayerWindowViewModel : ViewModel
    {

        private IMultiPlayerModel model;

        public delegate void NotifyViewThatPropertyChanged(string reason);
        
        public event NotifyViewThatPropertyChanged ClosingHappend;

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

        // public event PropertyChangedEventHandler PropertyChanged;
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

        public string VmEndPoint
        {
            get
            {
                return this.model.EndPoint.ToString();
            }
        }

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

        public string Vm
        {
            get
            {
                return this.model.CloseReason;
            }

            set
            {
                this.model.CloseReason = value;
                //this.ClosingHappend?.Invoke(value);
            }
        }

        public int KeyPressed(char direction)
        {
            return this.model.KeyPressed(direction);
        }

        public void CloseGame()
        {
            this.model.CloseGame();
        }
    }
}
