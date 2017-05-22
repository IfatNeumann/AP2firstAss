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
            int i, j, iLocation, jLocation;
            int rows = int.Parse(this.Rows), cols = int.Parse(this.Cols);
            this.myMaze = Maze.FromJSON(this.StringMaze);

            int iStart = this.myMaze.InitialPos.Row;
            int jStart = this.myMaze.InitialPos.Col;
            int iEnd = this.myMaze.GoalPos.Row;
            int jEnd = this.myMaze.GoalPos.Col;
            this.rectWidth = (int)this.MyCanvas.Width / cols;
            this.rectHeight = (int)this.MyCanvas.Height / rows;
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

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Width = this.rectWidth;
                    rect.Height = this.rectHeight;
                    iLocation = i * this.rectHeight;
                    jLocation = j * this.rectWidth;
                    Canvas.SetLeft(rect, jLocation);
                    Canvas.SetTop(rect, iLocation);

                    // if is not a wall
                    if (this.myMaze[i, j] == CellType.Free)
                    {
                        rect.Fill = grass;
                    }

                    // if is wall
                    else if (this.myMaze[i, j] == CellType.Wall)
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = wall;
                    }
                    
                    if (i == iEnd && j == jEnd)
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
            Canvas.SetTop(this.playerRec, iStart * this.rectHeight);
            Canvas.SetLeft(this.playerRec, jStart * this.rectWidth);
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
            int i  = int.Parse(args[0]);
            int j = int.Parse(args[1]);
            Canvas.SetTop(this.playerRec, i * this.rectHeight);
            Canvas.SetLeft(this.playerRec, j * this.rectWidth);
        }

        public void SolveMaze(string solution)
        {
            // 0 - left, 1- right, 2- up, 3- down
            int index = 0 , length = solution.Length;
            int j = this.myMaze.InitialPos.Col;
            int i = this.myMaze.InitialPos.Row;
            Canvas.SetTop(this.playerRec, i * this.rectHeight);
            Canvas.SetLeft(this.playerRec, j * this.rectWidth);
            while (index < length)
            {
                switch (solution[index])
                {
                    case '0':
                        {
                            j -= 1;
                            break; 
                        }
                    case '1':
                        {
                            j += 1;
                            break;
                        }
                    case '2':
                        {
                            i -= 1;
                            break;
                        }
                    case '3':
                        {
                            i += 1;
                            
                            break;
                        }

                }
                this.MyCanvas.Children.Remove(this.playerRec);
                Canvas.SetLeft(this.playerRec, j * this.rectWidth);
                Canvas.SetTop(this.playerRec, i * this.rectHeight);
                this.MyCanvas.Children.Add(this.playerRec);
                System.Threading.Thread.Sleep(100);
                index++;
            }
        }
    }

}

