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

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        //public string Maze
        //{
        //    get { return MazeProperty.ToString(); }
        //    set { SetValue(MazeProperty, value); }
        //}

        //public string InitialPos
        //{
        //    get { return InitialPosProperty.ToString(); }
        //    set { SetValue(InitialPosProperty, value); }
        //}

        //public string GoalPos
        //{
        //    get { return GoalPosProperty.ToString(); }
        //    set { SetValue(GoalPosProperty, value); }
        //}

        //Using a DependencyProperty as the backing store for Rows.This enables animation, styling,



        //public static readonly DependencyProperty MazeProperty =
        //DependencyProperty.Register("Maze", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty InitialPosProperty =
        //DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty GoalPosProperty =
        //DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeUserControl), null);
        

        public void Draw()
        {
            int i, j, xLocation, yLocation;
            int rows, cols;
            int index0 = 0;
            rows = int.Parse(this.Rows);
            cols = int.Parse(this.Cols);
            string maze = "1,0,0,0,0,0,1,0,1,1,1,1,0,0,0,1,0,0,0,1,1,0,1,1,0";
            int rectWidth = (int)this.MyCanvas.Width / cols;
            int rectHeight = (int)this.MyCanvas.Height / rows;
            string InitialPos = "0,1";
            string GoalPos = "2,3";
            this.rectList = new List<Rectangle>();
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
                    if (maze[index0] == '0')
                    {
                        rect.Stroke = new SolidColorBrush(Colors.White);
                        rect.Fill = new SolidColorBrush(Colors.White);
                    }
                    // if is wall
                    else if (maze[index0] == '1')
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = new SolidColorBrush(Colors.Black);
                    }

                    if (i == (int)(InitialPos[0] - '0')
                        && j == (int)(InitialPos[2] - '0')) // the current place of the player
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Red);
                        rect.Fill = new SolidColorBrush(Colors.Red);
                    }
                    else if (i == (int)(GoalPos[0] - '0')
                             && j == (int)(GoalPos[2] - '0')) // the current place of the player
                    {
                        rect.Stroke = new SolidColorBrush(Colors.BlueViolet);
                        rect.Fill = new SolidColorBrush(Colors.BlueViolet);
                    }

                    this.MyCanvas.Children.Add(rect);
                    this.rectList.Add(rect);
                    index0 = index0 + 2;
                }
            }
        }

        private void MazeBoard_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Draw();
        }
    }
}
