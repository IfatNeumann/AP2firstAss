using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows;

    public class SinglePlayerWindowViewModel : ViewModel
    {
        private ISinglePlayerModel model;
        //public event PropertyChangedEventHandler PropertyChanged;
        
        public SinglePlayerWindowViewModel(ISinglePlayerModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
                 {
                     this.NotifyPropertyChanged("Vm"+e.PropertyName);
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

        public void SaveSettings()
        {
          //  this.model.SaveSettings();
        }

        public int KeyPressed(char direction)
        {
            return this.model.KeyPressed(direction);
        }

        public void b(Point p)
        {
            this.VmCurrPoint = p.ToString();
        }
        //protected void NotifyPropertyChanged(string name)
            //{
            //    if (this.PropertyChanged != null)
            //    {
            //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            //    }
            //}
        }
}
