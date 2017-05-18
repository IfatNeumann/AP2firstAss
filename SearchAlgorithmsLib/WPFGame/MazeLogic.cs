using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGame
{
    public class MazeLogic
    {
        
        private int[,] maze;
        private int[,] sol;
        private int row;
        private int col;
        private Point start;
        private Point end;
        private Point prev;


        public MazeLogic(int[] Maze, int[] Sol, Point Start, Point End)
        {
            this.col = 8;
            this.row = 8;
            this.maze = ConvertToMatrix(Maze);
            this.sol = ConvertToMatrix(Sol);
            this.start = Start;
            this.end = End;
            this.prev = Start;
        }

        public int[,] ConvertToMatrix(int[] arr)
        {
            int[,] output = new int[this.row * 2 - 1, this.col * 2 - 1];
            int i, j, m = 0;
            for (i = 0; i < this.row * 2 - 1; i++)
            {
                for (j = 0; j < this.col * 2 - 1; j++)
                {
                    output[i, j] = arr[m];
                    m++;
                }
            }
            return output;
        }

    }
}
