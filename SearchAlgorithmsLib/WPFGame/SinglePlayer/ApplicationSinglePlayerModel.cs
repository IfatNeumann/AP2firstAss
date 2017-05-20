namespace WPFGame
{
    using System.ComponentModel;
    using System.Net.Sockets;
    using System.Configuration;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class ApplicationSinglePlayerModel : ISinglePlayerModel
    {
        private string name;
        private int rows;
        private int cols;

        private int mazeString;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string MazeName
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("MazeName");
                }
            }
        }

        public int MazeRows
        {
            get
            {
                return this.rows;
            }
            set
            {
                if (this.rows != value)
                {
                    this.rows = value;
                    this.OnPropertyChanged("MazeRows");
                }
            }
        }

        public int MazeCols
        {
            get
            {
                return this.cols;
            }
            set
            {
                if (this.cols != value)
                {
                    this.cols = value;
                    this.OnPropertyChanged("MazeCols");
                }
            }
        }

        public int MazeString
        {
            get
            {
                return this.mazeString;
            }

            set
            {
                this.mazeString = value;
            }
        }

        public void StartGame()
        {
            string port = ConfigurationManager.AppSettings["port"];
            string ip = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            // create new TcpClient
            TcpClient client = new TcpClient();
            client.Connect(ipep);
            NetworkStream stream = client.GetStream();

            // Write to server
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write("generate " + this.name + " " + this.MazeRows.ToString() + " " + this.MazeCols.ToString());
            BinaryReader reader = new BinaryReader(stream);
            // Get result from server
            string result = reader.ReadString();
        }

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}