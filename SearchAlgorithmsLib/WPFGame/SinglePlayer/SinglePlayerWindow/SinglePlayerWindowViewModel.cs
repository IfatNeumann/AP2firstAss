using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    public class SinglePlayerWindowViewModel : ViewModel
    {
        private ISinglePlayerModel model;

        public SinglePlayerWindowViewModel(ISinglePlayerModel model)
        {
            this.model = model;
        }

        //public int VmMazeRows
        //{
        //    get
        //    {
        //        return this.model.MazeRows;
        //    }

        //    set
        //    {
        //        if (this.model.MazeRows != value)
        //        {
        //            this.model.MazeRows = value;
        //            this.NotifyPropertyChanged("VmMazeRows");
        //        }
        //    }
        //}

        //public int VmMazeCols
        //{
        //    get
        //    {
        //        return this.model.MazeCols;
        //    }

        //    set
        //    {
        //        if (this.model.MazeCols != value)
        //        {
        //            this.model.MazeCols = value;
        //            this.NotifyPropertyChanged("VmMazeCols");
        //        }
        //    }
        //}

        public void SaveSettings()
        {
          //  this.model.SaveSettings();
        }


    }
}
