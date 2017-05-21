namespace WPFGame
{
    using System.ComponentModel;
    using System.Net.Sockets;
    using System.Configuration;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows;

    using MazeLib;

    using Newtonsoft.Json;

    public class ApplicationSinglePlayerModel : ISinglePlayerModel
    {
        private string name;
        private int rows;
        private int cols;
        private string stringMaze;

        private Maze maze;
        private Point currPoint;

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
                }
            }
        }

        public string StringMaze
        {
            get
            {
                return this.stringMaze;
            }

            set
            {
                this.stringMaze = value;
            }
        }

        public Point CurrPoint
        {
            get
            {
                return this.currPoint;
            }
            set
            {
                    this.currPoint = value;
                    this.NotifyPropertyChanged("CurrPoint");
            }
        }

        public void KeyPressed(char direction)
        {
            int xLocation = (int)this.CurrPoint.X, yLocation = (int)this.CurrPoint.Y;
            if (direction == 'l' && xLocation - 1 >= 0)
            {
                this.CurrPoint = new Point(xLocation - 1, yLocation);
            }
            if (direction == 'r' && xLocation + 1 < this.MazeCols)
            {
                this.CurrPoint = new Point(xLocation + 1, yLocation);
            }
            if (direction == 'u' && yLocation - 1 >= 0)
            {
                this.CurrPoint = new Point(xLocation, yLocation - 1);
            }
            if (direction == 'd' && yLocation + 1 < this.MazeRows)
            {
                this.CurrPoint = new Point(xLocation, yLocation + 1);
            }
            return;
        }

        public void StartGame()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.ServerIP),
                Properties.Settings.Default.ServerPort);
            // create new TcpClient
            TcpClient client = new TcpClient();
            client.Connect(ipep);
            NetworkStream stream = client.GetStream();

            // Write to server
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write("generate " + this.name + " " + this.MazeRows.ToString() + " " + this.MazeCols.ToString());
            BinaryReader reader = new BinaryReader(stream);

            // Get result from server
            this.StringMaze = reader.ReadString();
            this.maze = Maze.FromJSON(this.StringMaze);
            // close connection
            writer.Dispose();
            reader.Dispose();
            client.Close();

        }

        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}