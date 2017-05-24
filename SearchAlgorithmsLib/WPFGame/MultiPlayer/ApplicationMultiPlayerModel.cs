using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows;

    using MazeLib;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

    using WPFGame.Properties;

    class ApplicationMultiPlayerModel : IMultiPlayerModel
    {
        private bool notReady;

        private char direction;
        private string name;

        private int rows;

        private int cols;

        private string stringMaze;

        private string solution;

        private Maze maze;

        private Point currPoint;
        private Point secondCurrPoint;

        private Point endPoint;

        private string line;

        private char command;

        private string closeReason;

        private ObservableCollection<string> gamesList;

        public ObservableCollection<string> GamesList
        {
            get
            {
                return this.gamesList;
            }

            set
            {
                this.gamesList = value;
                this.NotifyPropertyChanged("GamesList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationMultiPlayerModel()
        {
            this.rows = Settings.Default.MazeRows;
            this.cols = Settings.Default.MazeCols;
            this.notReady = true;
            this.command = 'N';
        }

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

        public Point SecondCurrPoint
        {
            get
            {
                return this.secondCurrPoint;
            }

            set
            {
                this.secondCurrPoint = value;
                this.NotifyPropertyChanged("SecondCurrPoint");
            }
        }

        public Point EndPoint
        {
            get
            {
                int iEnd = this.maze.GoalPos.Row;
                int jEnd = this.maze.GoalPos.Col;
                this.endPoint = new Point(iEnd, jEnd);
                return this.endPoint;
            }
        }

        public string Solution
        {
            get
            {
                return this.solution;
            }

            set
            {
                if (this.solution != value)
                {
                    this.solution = value;
                }
            }
        }

        public string CloseReason
        {
            get
            {
                return this.closeReason;
            }

            set
            {
                this.closeReason = value;
                this.NotifyPropertyChanged("CloseReason");
            }
        }
        
        public bool NotReady
        {
            get
            {
                return this.notReady;
            }
            set
            {
                this.notReady = value;
            }
        }

        public void StartConnection()
        {
            IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse(Properties.Settings.Default.ServerIP),
                Properties.Settings.Default.ServerPort);
            TcpClient client = null;
            NetworkStream stream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;
            Task task;

            Action receiveThread = new Action(
                () =>
                    {
                        while (true)
                        {
                            try
                            {
                                // Get result from server
                                string result = reader.ReadString();
                                string commandKey = this.line.Split(' ').First();
                                if (result != string.Empty && !commandKey.Equals("list"))
                                {
                                    JObject dir = JObject.Parse(result);

                                    if (result.Equals("{}"))
                                    {
                                        commandKey = "gotClosed";
                                    }
                                    else if (dir.Last.Path.Equals("Direction"))
                                    {
                                        commandKey = "play";
                                        result = dir.GetValue("Direction").ToString();
                                        //result = dir[1];
                                    }
                                }
                                
                                this.EvaluateAnswer(commandKey,result);

                                // check the commands require  closing the connection 
                                if (commandKey.Equals("generate") || commandKey.Equals("solve")
                                    || (commandKey.Equals("close") && result.Equals(string.Empty)))
                                    /* if there was an invalid input for the close
                                     * command we want to stay connected */
                                {
                                    writer.Dispose();
                                    reader.Dispose();
                                    client.Close();
                                    client = null;
                                    break;
                                }
                            }
                            catch (SocketException)
                            {
                                Console.WriteLine("exception - connection stopped");
                                break;
                            }
                        }
                    });
            while (true)
            {
                try
                {
                    // Send data to server
                    this.line = this.SendMassage();
                    if (client == null)
                    {
                        // create new TcpClient
                        client = new TcpClient();
                        client.Connect(ipep);
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                        task = new Task(receiveThread);
                        task.Start();
                    }

                    writer.Write(this.line);
                }
                catch (SocketException)
                {
                    Console.WriteLine("exception - connection stopped");
                    break;
                }
            }


        }

        private void EvaluateAnswer(string commandKey, string result)
        {
            switch (commandKey)
            {
                case "start":
                    {
                        if (result.Length != 0)
                        {
                            this.StringMaze = result;
                            this.maze = Maze.FromJSON(this.StringMaze);
                            int x = this.maze.InitialPos.Row;
                            int y = this.maze.InitialPos.Col;
                            Point curr = new Point(x, y);

                            this.CurrPoint = curr;
                            this.SecondCurrPoint = curr;
                            this.notReady = false;
                        }
                        break;
                    }
                case "list":
                    {
                        string[] list = JsonConvert.DeserializeObject<string[]>(result);
                        int i,length = list.Count();
                        ObservableCollection<string> someList= new ObservableCollection<string>();
                        for (i = 0; i < length; i++)
                        {
                            someList.Add(list[i]);
                        }
                        this.GamesList = someList;
                        break;
                    }
                case "join":
                    {
                        this.StringMaze = result;
                        this.maze = Maze.FromJSON(this.StringMaze);
                        int x = this.maze.InitialPos.Row;
                        int y = this.maze.InitialPos.Col;
                        Point curr = new Point(x, y);

                        this.CurrPoint = curr;
                        this.SecondCurrPoint = curr;
                        this.notReady = false;
                        break;
                    }
                case "play":
                    {
                        if(result != string.Empty)
                            this.SecPlayerKeyPressed(result[0]);
                        break;
                    }
                case "gotClosed":
                    {
                        if (this.SecondCurrPoint.Equals(this.EndPoint))
                        {
                            this.CloseReason = "lose";

                        }
                        else
                        {
                            this.CloseReason = "technicalWin";
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void StartGame()
        {
            this.command = 's';
        }

        public void JoinGame()
        {
            //MazeName = find the maze we want to join
            this.command = 'j';
        }

        public int KeyPressed(char direction)
        {
            int iLocation = (int)this.CurrPoint.X, jLocation = (int)this.CurrPoint.Y;
            if ((this.EndPoint.X == iLocation) && (this.EndPoint.Y == jLocation))
            {
                return 1;
            }
            else
            {
                switch (direction)
                {
                    case 'l':
                        {
                            if (jLocation - 1 >= 0 && this.maze[iLocation, jLocation - 1] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation, jLocation - 1);
                                this.command = ',';
                            }

                            break;
                        }

                    case 'r':
                        {
                            if (jLocation + 1 < this.MazeCols && this.maze[iLocation, jLocation + 1] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation, jLocation + 1);
                                this.command = '/';
                            }

                            break;
                        }

                    case 'u':
                        {
                            if (iLocation - 1 >= 0 && this.maze[iLocation - 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation - 1, jLocation);
                                this.command = ';';
                            }

                            break;
                        }

                    case 'd':
                        {
                            if (iLocation + 1 < this.MazeRows && this.maze[iLocation + 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation + 1, jLocation);
                                this.command = '.';
                            }

                            break;
                        }
                }
                return 0;
            }
        }

        public int SecPlayerKeyPressed(char direction)
        {
            int iLocation = (int)this.SecondCurrPoint.X, jLocation = (int)this.SecondCurrPoint.Y;
            if ((this.EndPoint.X == iLocation) && (this.EndPoint.Y == jLocation))
            {
                return 1;
            }
            else
            {
                switch (direction)
                {
                    case 'l':
                        {
                            this.SecondCurrPoint = new Point(iLocation, jLocation - 1);
                            break;
                        }

                    case 'r':
                        {
                            this.SecondCurrPoint = new Point(iLocation, jLocation + 1);
                            break;
                        }

                    case 'u':
                        {
                            this.SecondCurrPoint = new Point(iLocation - 1, jLocation);
                            break;
                        }

                    case 'd':
                        {
                            this.SecondCurrPoint = new Point(iLocation + 1, jLocation);
                            break;
                        }
                }
                return 0;
            }
        }

        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string SendMassage()
        {
            string massage;
            while (this.command == 'N')
            { 
            }

            switch (this.command)
            {
                case 's':
                    {
                        this.command = 'N';
                        massage = "start " + this.MazeName + " " + this.MazeRows + " " + this.MazeCols;
                        break;
                    }
                case 'l':
                    {
                        this.command = 'N';
                        massage = "list";
                        break;
                    }
                case 'j':
                    {
                        this.command = 'N';
                        massage = "join ifat";
                        break;
                    }
                case ',':
                    {
                        this.command = 'N';
                        massage = "play left";
                       
                        break;
                    }
                case '/':
                    {
                        this.command = 'N';
                        massage = "play right";

                        break;
                    }
                case ';':
                    {
                        this.command = 'N';
                        massage = "play up";

                        break;
                    }
                case '.':
                    {
                        this.command = 'N';
                        massage = "play down";

                        break;
                    }
                case 'c':
                    {
                        this.command = 'N';
                        massage = "close ifat";
                        break;
                    }
                default:
                    {
                        massage = string.Empty;
                        break;
                    }
            }
            return massage;
        }

        public void CloseGame()
        {
            if(this.CloseReason==null)
                this.command = 'c';
        }

        public void GetList()
        {
            //this.command = 'l';
            return;
        }
    }
}
