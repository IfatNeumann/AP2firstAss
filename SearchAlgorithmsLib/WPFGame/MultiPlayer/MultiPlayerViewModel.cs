using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class MultiPlayerViewModel : ViewModel
    {
        private IMultiPlayerModel model;
        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
                {
                    
                        this.NotifyPropertyChanged("Vm" + e.PropertyName);
                    
                };
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

        public ObservableCollection<string> VmGamesList
        {
            get
            {
                return this.model.GamesList;
            }

            set
            {
                this.model.GamesList = value;
                this.NotifyPropertyChanged("VmGamesList");
            }
        }

        public bool NotReady
        {
            get
            {
                return this.model.NotReady;
            }
            set
            {
                this.model.NotReady = value;
            }
        }

        public void VmGetList()
        {
            this.model.GetList();
        }

        public void StartConnection()
        {
            this.model.StartConnection();
        }

        public void StartGame()
        {
            this.model.StartGame();
        }

        public void JoinGame()
        {
            this.model.JoinGame();
        }

        public void CloseConnection()
        {
            this.model.CloseConnection();
        }
    }
}
