namespace WPFGame
{
    public class ApplicationSettingsModel : ISettingsModel
    {
        public string ServerIP
        {
            get
            {
                return Properties.Settings.Default.ServerIP;
            }
            set
            {
                Properties.Settings.Default.ServerIP = value;
            }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}