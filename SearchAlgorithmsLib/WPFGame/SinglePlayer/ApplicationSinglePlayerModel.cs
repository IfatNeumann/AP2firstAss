using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using MazeLib;
using Newtonsoft.Json.Linq;
using WPFGame.Properties;

namespace WPFGame
{
    /// <summary>
    /// the model of the application sibgle player
    /// </summary>
    /// <seealso cref="WPFGame.ISinglePlayerModel" />
    public class ApplicationSinglePlayerModel : ISinglePlayerModel
    {
        /// <summary>
        /// The name of th game
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
        /// The end point
        /// </summary>
        private Point endPoint;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSinglePlayerModel"/> class.
        /// </summary>
        public ApplicationSinglePlayerModel()
        {
            this.rows = Settings.Default.MazeRows;
            this.cols = Settings.Default.MazeCols;
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
        /// handels Keys the pressed.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public int KeyPressed(char direction)
        {

            int iLocation = (int)this.CurrPoint.X, jLocation = (int)this.CurrPoint.Y;

            // for the case where the mother nad marco are in the same place at initialization
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
                            }

                            break;
                        }

                    case 'r':
                        {
                            if (jLocation + 1 < this.MazeCols && this.maze[iLocation, jLocation + 1] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation, jLocation + 1);
                            }

                            break;
                        }

                    case 'u':
                        {
                            if (iLocation - 1 >= 0 && this.maze[iLocation - 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation - 1, jLocation);
                            }

                            break;
                        }

                    case 'd':
                        {
                            if (iLocation + 1 < this.MazeRows && this.maze[iLocation + 1, jLocation] == CellType.Free)
                            {
                                this.CurrPoint = new Point(iLocation + 1, jLocation);
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
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse(Properties.Settings.Default.ServerIP),
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
            int x = this.maze.InitialPos.Row;
            int y = this.maze.InitialPos.Col;
            Point curr = new Point(x, y);

            this.CurrPoint = curr;

            // solution
            writer.Write("solve " + this.name + " 1");
            JObject jSolution = JObject.Parse(reader.ReadString());
            this.Solution = jSolution["Solution"].ToString();

            // close connection
            writer.Dispose();
            reader.Dispose();
            client.Close();
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
        /// Solves the maze.
        /// </summary>
        public void SolveMaze()
        {
            Task t = new Task(
                () =>
                    {
                        // 0 - left, 1- right, 2- up, 3- down
                        int length = this.Solution.Length, index = length - 1;
                        while (index >= 0)
                        {
                            switch (this.Solution[index])
                            {
                                case '0':
                                    {
                                        this.KeyPressed('l');
                                        break;
                                    }

                                case '1':
                                    {
                                        this.KeyPressed('r');
                                        break;
                                    }

                                case '2':
                                    {
                                        this.KeyPressed('u');
                                        break;
                                    }

                                case '3':
                                    {
                                        this.KeyPressed('d');
                                        break;
                                    }
                            }

                            System.Threading.Thread.Sleep(500);
                            index--;
                        }
                    });
            t.Start();
        }

        /// <summary>
        /// Initializes the start position.
        /// </summary>
        public void InitStartPos()
        {
            this.maze = Maze.FromJSON(this.StringMaze);
            int x = this.maze.InitialPos.Row;
            int y = this.maze.InitialPos.Col;
            this.CurrPoint = new Point(x, y);
        }
    }
}