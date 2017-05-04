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

        public void SaveSettings()
        {
            this.model.SaveSettings();
        }
    }
}
