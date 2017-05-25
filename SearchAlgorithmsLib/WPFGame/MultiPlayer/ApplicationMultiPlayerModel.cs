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

    /// <summary>
    /// the application multi player model
    /// </summary>
    /// <seealso cref="WPFGame.IMultiPlayerModel" />
    class ApplicationMultiPlayerModel : IMultiPlayerModel
    {
        /// <summary>
        /// not ready to show the screen
        /// </summary>
        private bool notReady;
        
        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// The rows
        /// </summary>
        private int rows;

        /// <summary>
        /// The cols
        /// </summary>
        private int cols;

        /// <summary>
        /// The string maze
        /// </summary>
        private string stringMaze;

        /// <summary>
        /// The solution
        /// </summary>
        private string solution;

        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// The current point
        /// </summary>
        private Point currPoint;
       
        /// <summary>
        /// The second curr point
        /// </summary>
        private Point secondCurrPoint;

        /// <summary>
        /// The end point
        /// </summary>
        private Point endPoint;

        /// <summary>
        /// The line
        /// </summary>
        private string line;

        /// <summary>
        /// The command
        /// </summary>
        private char command;

        /// <summary>
        /// The close reason
        /// </summary>
        private string closeReason;

        /// <summary>
        /// The games list
        /// </summary>
        private ObservableCollection<string> gamesList;

        /// <summary>
        /// Gets or sets the games list.
        /// </summary>
        /// <value>
        /// The games list.
        /// </value>
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

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMultiPlayerModel"/> class.
        /// </summary>
        public ApplicationMultiPlayerModel()
        {
            this.name = Settings.Default.MazeName;
            this.rows = Settings.Default.MazeRows;
            this.cols = Settings.Default.MazeCols;
            this.notReady = true;
            this.command = 'N';
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
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

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
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

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
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

        /// <summary>
        /// Gets or sets the string maze.
        /// </summary>
        /// <value>
        /// The string maze.
        /// </value>
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

        /// <summary>
        /// Gets or sets the current point.
        /// </summary>
        /// <value>
        /// The curr point.
        /// </value>
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

        /// <summary>
        /// Gets or sets the second curr point.
        /// </summary>
        /// <value>
        /// The second curr point.
        /// </value>
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

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
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

        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
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

        /// <summary>
        /// Gets or sets the close reason.
        /// </summary>
        /// <value>
        /// The close reason.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether [not ready].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [not ready]; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Starts the connection.
        /// </summary>
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

        /// <summary>
        /// Evaluates the answer.
        /// </summary>
        /// <param name="commandKey">The command key.</param>
        /// <param name="result">The result.</param>
        private void EvaluateAnswer(string commandKey, string result)
        {//check the command and make her work
            switch (commandKey)
            {
                case "start":
                    {
                        if (result.Length != 0)
                        {
                            //start the game
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
                        //show the list
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
                        //join to anothe game
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
                        //make a move
                        if(result != string.Empty)
                            this.SecPlayerKeyPressed(result[0]);
                        break;
                    }
                case "gotClosed":
                    {
                        //close the game
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

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            this.command = 's';
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        public void JoinGame()
        {
            //MazeName = find the maze we want to join
            this.command = 'j';
        }

        /// <summary>
        /// Keys the pressed.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
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
                if ((this.EndPoint.X == this.CurrPoint.X) && (this.EndPoint.Y == this.CurrPoint.Y))
                {
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// Secs the player key pressed.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
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
                if ((this.EndPoint.X == this.SecondCurrPoint.X) && (this.EndPoint.Y == this.SecondCurrPoint.Y))
                {
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void NotifyPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Sends the massage.
        /// </summary>
        /// <returns></returns>
        public string SendMassage()
        {
            string massage;
            while (this.command == 'N')
            { 
            }
            //set a massege to terminal
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
                        massage = "join "+ this.MazeName;
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
                        massage = "close " + this.MazeName;
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

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            if(this.CloseReason==null)
                this.command = 'c';
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        public void GetList()
        {
            this.command = 'l';
            return;
        }
    }
}
