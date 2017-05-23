using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    public class MultiPlayerViewModel : ViewModel
    {
        private IMultiPlayerModel model;

        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
        }

        public string VmName
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

        public int VmRows
        {
            get
            {
                return this.model.MazeRows;
            }

            set
            {
                this.model.MazeRows = value;
            }
        }

        public int VmCols
        {
            get
            {
                return this.model.MazeCols;
            }

            set
            {
                this.model.MazeCols = value;
            }
        }

    }
}
