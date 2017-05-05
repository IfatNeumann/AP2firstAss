using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    public class SettingsViewModel : ViewModel
    {
        private ISettingsModel model;

        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        public string ServerIP
        {
            get
            {
                return this.model.ServerIP;
            }

            set
            {
                this.model.ServerIP = value;
                this.NotifyPropertyChanged("ServerIP");
            }
        }

        public int ServerPort
        {
            get
            {
                return this.model.ServerPort;
            }

            set
            {
                this.model.ServerPort = value;
                this.NotifyPropertyChanged("ServerPort");
            }
        }

        public int MazeRows
        {
            get
            {
                return this.model.MazeRows;
            }

            set
            {
                this.model.MazeRows = value;
                this.NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get
            {
                return this.model.MazeCols;
            }

            set
            {
                this.model.MazeCols = value;
                this.NotifyPropertyChanged("MazeCols");
            }
        }

        public int SearchAlgorithm
        {
            get
            {
                return this.model.SearchAlgorithm;
            }

            set
            {
                this.model.SearchAlgorithm = value;
                this.NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        public void SaveSettings()
        {
            this.model.SaveSettings();
        }
    }
}
