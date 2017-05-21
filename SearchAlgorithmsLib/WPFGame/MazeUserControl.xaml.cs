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
    using MazeGeneratorLib;

    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private List<Rectangle> rectList;
        private int index = 0;

        private Maze ma;
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

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty StringMazeProperty =
            DependencyProperty.Register("StringMaze", typeof(string), typeof(MazeUserControl), null);
        
        public void Draw()
        {
            int i, j, xLocation, yLocation;
            int rows, cols;
            Maze myMaze = Maze.FromJSON(this.StringMaze);
            Point start = new Point(myMaze.InitialPos.Row, myMaze.InitialPos.Col);
            Point end = new Point(myMaze.GoalPos.Row, myMaze.GoalPos.Col);
            Point curr;
            rows = int.Parse(this.Rows);
            cols = int.Parse(this.Cols);
            int rectWidth = (int)this.MyCanvas.Width / cols;
            int rectHeight = (int)this.MyCanvas.Height / rows;
            this.rectList = new List<Rectangle>();
            // Create ImageBrushes
            ImageBrush marco = new ImageBrush();
            marco.ImageSource =
                new BitmapImage(new Uri(@"images/marco.jpg", UriKind.Relative));

            ImageBrush grass = new ImageBrush();
            grass.ImageSource =
                new BitmapImage(new Uri(@"images/grass.jpg", UriKind.Relative));

            ImageBrush wall = new ImageBrush();
            wall.ImageSource =
                new BitmapImage(new Uri(@"images/wall.jpg", UriKind.Relative));

            ImageBrush mother = new ImageBrush();
            mother.ImageSource =
                new BitmapImage(new Uri(@"images/mother.jpg", UriKind.Relative));
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Width = rectWidth;
                    rect.Height = rectHeight;
                    xLocation = j * rectWidth;
                    yLocation = i * rectHeight;
                    Canvas.SetLeft(rect, xLocation);
                    Canvas.SetTop(rect, yLocation);
                    // if is not a wall
                    if (myMaze[i, j] == CellType.Free)
                    {
                        rect.Fill = grass;
                        //    rect.Stroke = new SolidColorBrush(Colors.White);
                        //    rect.Fill = new SolidColorBrush(Colors.White);
                    }

                    // if is wall
                    else if (myMaze[i,j] == CellType.Wall)
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = wall;
                    }
                    curr = new Point(i, j);
                    if (curr.Equals(start)) // the current place of the player
                    {
                        rect.Fill = marco;
                    }
                    else if (curr.Equals(end)) // the current place of the player
                    {
                        rect.Fill = mother;
                    }

                    this.MyCanvas.Children.Add(rect);
                    this.rectList.Add(rect);
                }
            }
        }

        private void MazeBoard_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Draw();
        }

        //EventManager.RegisterClassHandler(typeof(MainWindow), UIElement.KeyDownEvent, new KeyEventHandler(KeyDownHandler));
        
        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
        }
    }

}

