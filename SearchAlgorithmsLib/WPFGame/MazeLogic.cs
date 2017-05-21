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


        public MazeLogic(int[] otherMaze, int[] otherSol, Point otherStart, Point otherEnd)
        {
            this.col = 8;
            this.row = 8;
            this.maze = ConvertTo2D(otherMaze);
           // this.sol = ConvertTo2D(otherSol);
            this.start = otherStart;
            this.end = otherEnd;
            this.prev = otherStart;
        }

        public int[,] ConvertTo2D(int[] arr)
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
