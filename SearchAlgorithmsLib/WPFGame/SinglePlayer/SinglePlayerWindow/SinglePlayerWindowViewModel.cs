using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.ComponentModel;

    public class SinglePlayerWindowViewModel : ViewModel
    {
        private ISinglePlayerModel model;

        public SinglePlayerWindowViewModel(ISinglePlayerModel model)
        {
            this.model = model;
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
                    this.NotifyPropertyChanged("VmMazeRows");
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
                    this.NotifyPropertyChanged("VmMazeCols");
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
        //protected void NotifyPropertyChanged(string name)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        public void SaveSettings()
        {
          //  this.model.SaveSettings();
        }


    }
}
