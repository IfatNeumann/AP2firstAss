using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class Pos
    {
        private int row;
        private int col; 
        private Pos up = null;
        private Pos down = null;
        private Pos right = null;
        private Pos left = null;


        public int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
            }
        }
        public int Col
        {
            get
            {
                return col;
            }

            set
            {
                col = value;
            }
        }


        public Pos(int x, int y)
        {
            this.Row= row;
            this.Col= col;
        }

        public override bool Equals(object obj)
        {
            if (this.Row == (obj as Pos).Row && this.Col == (obj as Pos).Col)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}

