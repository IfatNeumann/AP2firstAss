using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using MazeLib;

namespace Server
{
    public class Game
    {
        private TcpClient firstPlayer, secondPlayer;
        private Maze maze;
        public TcpClient SecondPlayer{
            get{
                return this.secondPlayer;
            }
            set
            {
                this.secondPlayer = value;
            }
        }
        public TcpClient FirstPlayer
        {
            get
            {
                return this.firstPlayer;
            }
        }
        public Maze MyMaze
        {
            get
            {
                return this.maze;
            }
        }
        public Game(Maze maze,TcpClient firstPlayer){
            this.maze = maze;
            this.firstPlayer = firstPlayer;
        }
    }
}
