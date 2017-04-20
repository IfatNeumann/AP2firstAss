using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using ConsoleApp1;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Server
{
    public class Model : IModel
    {
        private IController con;
        public Model(IController con)
        {
            this.con = con;
        }
        private Dictionary<string, Maze> mazes = new Dictionary<string, Maze>();
        private Dictionary<string, Game> games = new Dictionary<string, Game>();
        private Dictionary<string, Game> gamesPlaying = new Dictionary<string, Game>();
        private Dictionary<TcpClient, string> playing = new Dictionary<TcpClient, string>();
        public Dictionary<string, Maze> Mazes
        {
            get
            {
                return this.mazes;
            }
        }
        public Dictionary<string, Game> Games
        {
            get
            {
                return this.games;
            }
        }
        public Dictionary<string, Game> GamesPlaying
        {
            get
            {
                return this.gamesPlaying;
            }
        }
        private Dictionary<Maze,Solution<Position>> solutions = new Dictionary<Maze , Solution<Position>>();
        public Model()
        {

        }
        public Maze GenerateMaze( string name, int rows, int cols)
        {
            //create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze myMaze = myMazeGen.Generate(rows, cols);
            myMaze.Name = name;
            mazes.Add(name,myMaze);
            return mazes[name];//hi
        }
        public string SolveMaze (string name, ISearcher<Position> algorithm)
        {
            ObjectAdapter mazeAdapter = new ObjectAdapter(mazes[name]);
            solutions.Add(mazes[name], algorithm.search(mazeAdapter));
            Solution<Position> sol = solutions[mazes[name]];
            StringBuilder way = new StringBuilder("");
            State<Position> first, second;
            for (int i=0;i<sol.Trace.Count()-1;++i)
            {
                first = sol.Trace.ElementAt(i);
                second = sol.Trace.ElementAt(i+1);
                //left
                if (first.myState.Col < second.myState.Col)
                    way.Append(0);
                //right
                else if (first.myState.Col > second.myState.Col)
                    way.Append(1);
                //up
                else if (first.myState.Row < second.myState.Row)
                    way.Append(2);
                //down
                else if (first.myState.Row > second.myState.Row)
                    way.Append(3);
            }
            string ret = way.ToString();
            return ret;
        }
        //hi
        public void StartMaze(string name, int rows, int cols, TcpClient client)
        {
            //create maze
            DFSMazeGenerator myMazeGen = new DFSMazeGenerator();
            Maze myMaze = myMazeGen.Generate(rows, cols);
            myMaze.Name = name;
            //create game
            Game myGame = new Game(myMaze, client);
            games.Add(name, myGame);
        }
        public string ListMaze()
        {
            JArray Jlist = new JArray(games.Keys);
            return Jlist.ToString();
        }
        public Maze JoinMaze(string name,TcpClient client)
        {
            Game game = games[name];
            game.SecondPlayer = client;
            gamesPlaying.Add(name, game);
            playing.Add(client, name);
            playing.Add(game.FirstPlayer, name);
            Games.Remove(name);

            //print to the first player
            //send massage to server
            NetworkStream stream = game.FirstPlayer.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            
            writer.Write(gamesPlaying[name].MyMaze.ToJSON());
            return gamesPlaying[name].MyMaze;
        }
        public void PlayMaze(string move, TcpClient client) {
            //find the game
            Game game = GamesPlaying[playing[client]];
            //send massage to server
            TcpClient secondClient;
            if (game.SecondPlayer.Equals(client))
                secondClient = game.FirstPlayer;
            else
                secondClient = game.SecondPlayer;
            NetworkStream stream = secondClient.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            
            JObject mazeObj = new JObject();
            mazeObj["Name"] = playing[client];
            mazeObj["Direction"] = move;
            writer.Write(mazeObj.ToString());
        }
        public void CloseMaze(string name, TcpClient client)
        {
            //find the game
            Game game = GamesPlaying[playing[client]];
            //find the second client
            TcpClient secondClient;
            if (game.SecondPlayer.Equals(client))
                secondClient = game.FirstPlayer;
            else
                secondClient = game.SecondPlayer;
            //pull the game from all the dictionaries
            gamesPlaying.Remove(name);
            playing.Remove(client);
            playing.Remove(secondClient);
            //print massage to the second client
            NetworkStream stream = secondClient.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            JObject empty = new JObject();
            writer.Write(empty.ToString());
        }
    }
}
