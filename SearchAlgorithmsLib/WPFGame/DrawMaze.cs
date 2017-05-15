using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPFGame
{
    public class DrawMaze
    {
            /// <summary>
            /// members
            /// </summary>
            private int[] mazeMatrix;
            private int[] solMatrix;
            private int cols;
            private int rows;

            private int xStart;
            private int yStart;
            private int xEnd;
            private int yEnd;

            private Point start;
            private Point end;
            private Point prev;
            private Point current;

            private MazeLogic m;

            private List<Point> ls = new List<Point>();
            private int index = 0;

            private DependencyObject window;

            private Canvas maze;

            /// <summary>
            /// properties
            /// </summary>
            public int[] MazeMatrix
            {
                get
                {
                    return mazeMatrix;
                }

                set
                {
                    mazeMatrix = value;
                }
            }
            public int[] SolMatrix
            {
                get
                {
                    return solMatrix;
                }

                set
                {
                    solMatrix = value;
                }
            }
            public int Cols
            {
                get
                {
                    return cols;
                }

                set
                {
                    cols = value;
                }
            }
            public int Rows
            {
                get
                {
                    return rows;
                }

                set
                {
                    rows = value;
                }
            }
            public int XStart
            {
                get
                {
                    return xStart;
                }

                set
                {
                    xStart = value;
                }
            }
            public int YStart
            {
                get
                {
                    return yStart;
                }

                set
                {
                    yStart = value;
                }
            }
            public int XEnd
            {
                get
                {
                    return xEnd;
                }

                set
                {
                    xEnd = value;
                }
            }
            public int YEnd
            {
                get
                {
                    return yEnd;
                }

                set
                {
                    yEnd = value;
                }
            }
            public Point Start
            {
                get
                {
                    return start;
                }

                set
                {
                    start = value;
                }
            }
            public Point End
            {
                get
                {
                    return end;
                }

                set
                {
                    end = value;
                }
            }
            public Point Prev
            {
                get
                {
                    return prev;
                }

                set
                {
                    prev = value;
                }
            }
            public Point Current
            {
                get
                {
                    return current;
                }

                set
                {
                    current = value;
                }
            }
            public MazeLogic M
            {
                get
                {
                    return m;
                }

                set
                {
                    m = value;
                }
            }
            public List<Point> Ls
            {
                get
                {
                    return ls;
                }

                set
                {
                    ls = value;
                }
            }
            public int Index
            {
                get
                {
                    return index;
                }

                set
                {
                    index = value;
                }
            }
            public Canvas Maze
            {
                get
                {
                    return maze;
                }

                set
                {
                    maze = value;
                }
            }

          
            public DrawMaze(DependencyObject w, int size)
            {
                
                this.Maze = new Canvas();
                Maze.Width = size;
                Maze.Height = size;

                this.window = w;
          
                this.cols = 8;
                this.rows = 8;
            }

            // create the point according the values
            public void CreatePoints()
            {
                this.start = new Point(this.xStart, this.yStart);
                this.end = new Point(this.xEnd, this.yEnd);
                this.prev = new Point(this.xStart, this.yStart);
                this.current = new Point(this.xStart, this.yStart);
            }


            public void Draw()
            {

                int i, j;

                //putting labels to colors inside
                int m = 0;
                for (i = 0; i < this.rows - 1; i++)
                {
                    for (j = 0; j < this.cols - 1; j++)
                    {

                        Rectangle rect = new Rectangle();
                        rect.Width = 200;
                        rect.Height = 200;
                        if (this.mazeMatrix[m] == 0) // if is not a wall
                        {
                            rect.Stroke = new SolidColorBrush(Colors.White);
                            rect.Fill = new SolidColorBrush(Colors.White);
                            this.Maze.Children.Add(rect);
                        }
                        if (this.mazeMatrix[m] == 1) // if is wall
                        {
                            rect.Stroke = new SolidColorBrush(Colors.Black);
                            rect.Fill = new SolidColorBrush(Colors.Black);
                            this.Maze.Children.Add(rect);
                        }

                        if (i == this.current.X && j == this.current.Y) // the current place of the player
                        {
                            rect.Stroke = new SolidColorBrush(Colors.Red);
                            rect.Fill = new SolidColorBrush(Colors.Red);
                            this.Maze.Children.Add(rect);
                        }


                        if (i == this.xEnd && j == this.yEnd) // if is the end of the maze
                        {
                            rect.Stroke = new SolidColorBrush(Colors.Blue);
                            rect.Fill = new SolidColorBrush(Colors.Blue);
                            this.Maze.Children.Add(rect);
                        }
                        m++;
                    }
                }
            }
        }
    }

