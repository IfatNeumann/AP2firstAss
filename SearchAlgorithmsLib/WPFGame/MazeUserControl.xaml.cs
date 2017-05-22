using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;


namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows.Forms;

    using MazeGeneratorLib;

    using UserControl = System.Windows.Controls.UserControl;

    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private int rectWidth, rectHeight;
        private List<Rectangle> rectList;
        private int index = 0;
        private Rectangle playerRec;
        private Maze myMaze;
        private DFSMazeGenerator mg;

        private DependencyObject window;

        public MazeUserControl()
        {
            this.InitializeComponent();
        }

        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public string Cols
        {
            get { return (string)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public string StringMaze
        {
            get { return (string)GetValue(StringMazeProperty); }
            set { SetValue(StringMazeProperty, value); }
        }

        public string CurrPoint
        {
            get { return (string)GetValue(CurrPointProperty); }
            set
            {
                SetValue(CurrPointProperty, value);
                //this.UpdateMaze();
            }
        }

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty StringMazeProperty =
            DependencyProperty.Register("StringMaze", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty CurrPointProperty =
            DependencyProperty.Register("CurrPoint", typeof(string), typeof(MazeUserControl), new UIPropertyMetadata(ChangePos));

        public static void ChangePos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeUserControl allahWakbar = d as MazeUserControl;
            allahWakbar.UpdateMaze("", e.NewValue.ToString());
        }

        public void Draw()
        {
            int i, j, xLocation, yLocation;
            int rows, cols;
            myMaze = Maze.FromJSON(this.StringMaze);
            Point start = new Point(myMaze.InitialPos.Col, myMaze.InitialPos.Row);
            Point end = new Point(myMaze.GoalPos.Col, myMaze.GoalPos.Row);
            Point curr;
            rows = int.Parse(this.Rows);
            cols = int.Parse(this.Cols);
            rectWidth = (int)this.MyCanvas.Width / cols;
            rectHeight = (int)this.MyCanvas.Height / rows;
            this.rectList = new List<Rectangle>();
            // Create ImageBrushes
            ImageBrush marco = new ImageBrush();
            marco.ImageSource = new BitmapImage(new Uri(@"images/marco.jpg", UriKind.Relative));

            ImageBrush grass = new ImageBrush();
            grass.ImageSource = new BitmapImage(new Uri(@"images/grass.jpg", UriKind.Relative));

            ImageBrush wall = new ImageBrush();
            wall.ImageSource = new BitmapImage(new Uri(@"images/wall.jpg", UriKind.Relative));

            ImageBrush mother = new ImageBrush();
            mother.ImageSource = new BitmapImage(new Uri(@"images/mother.jpg", UriKind.Relative));

            for (i = 0; i < cols; i++)
            {
                for (j = 0; j < rows; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Width = this.rectWidth;
                    rect.Height = this.rectHeight;
                    xLocation = j * this.rectWidth;
                    yLocation = i * this.rectHeight;
                    Canvas.SetLeft(rect, xLocation);
                    Canvas.SetTop(rect, yLocation);
                    // if is not a wall
                    if (myMaze[i, j] == CellType.Free)
                    {
                        rect.Fill = grass;
                    }

                    // if is wall
                    else if (myMaze[i, j] == CellType.Wall)
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = wall;
                    }
                    curr = new Point(i, j);
                    if (curr.Equals(end)) // the current place of the player
                    {
                        rect.Fill = mother;
                    }

                    this.MyCanvas.Children.Add(rect);
                    this.rectList.Add(rect);
                }
            }

            // add the player
            this.playerRec = new Rectangle();
            this.playerRec.Width = this.rectWidth;
            this.playerRec.Height = this.rectHeight;
            Canvas.SetLeft(this.playerRec, start.Y * this.rectWidth);
            Canvas.SetTop(this.playerRec, start.X * this.rectHeight);
            this.playerRec.Fill = marco;
            this.MyCanvas.Children.Add(this.playerRec);
            this.rectList.Add(this.playerRec);
        }

        private void MazeBoard_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Draw();
        }
        
        public void UpdateMaze(string old, string newW)
        {
            if (this.playerRec == null)
            {
                return;
            }
            string[] args = newW.Split(',');
            int x  = int.Parse(args[0]);
            int y = int.Parse(args[1]);
            Canvas.SetLeft(this.playerRec, x * this.rectWidth);
            Canvas.SetTop(this.playerRec, y * this.rectHeight);
        }

        public void SolveMaze(string solution)
        {
            //0 - left, 1- right, 2- up, 3- down
            int i = 0 , length = solution.Length;
            int x = this.myMaze.InitialPos.Col;
            int y = this.myMaze.InitialPos.Row;
            while (i < length)
            {
                switch (solution[i])
                {
                    case '0':
                        {
                            x -= 1;
                            Canvas.SetLeft(this.playerRec, x * this.rectWidth);
                            System.Threading.Thread.Sleep(1000);
                            break; 
                        }
                    case '1':
                        {
                            x += 1;
                            Canvas.SetLeft(this.playerRec, x * this.rectWidth);
                            System.Threading.Thread.Sleep(1000);
                            break;
                        }
                    case '2':
                        {
                            y -= 1;
                            Canvas.SetTop(this.playerRec, y * this.rectHeight);
                            System.Threading.Thread.Sleep(1000);
                            break;
                        }
                    case '3':
                        {
                            y += 1;
                            Canvas.SetTop(this.playerRec, y * this.rectHeight);
                            System.Threading.Thread.Sleep(1000);
                            break;
                        }
                }
                
                i++;
            }
        }
    }

}

