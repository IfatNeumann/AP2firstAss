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
        private int rows, cols;
        private List<Rectangle> rectList;
        private int index = 0;

        private Maze ma;
        private DFSMazeGenerator mg;

        private DependencyObject window;

        public MazeUserControl()
        {
            this.InitializeComponent();
        }

        //public string Rows
        //{
        //    get
        //    {
        //        return RowsProperty.ToString();
        //    }

        //    set
        //    {
        //        this.SetValue(RowsProperty, value);
        //        this.rows = int.Parse(RowsProperty.ToString());
        //    }
        //}

        //public string Cols
        //{
        //    get
        //    {
        //        return RowsProperty.ToString();
        //    }

        //    set
        //    {
        //        this.SetValue(ColsProperty, value);
        //        this.cols = int.Parse(ColsProperty.ToString());
        //    }
        //}

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

        //public int Cols
        //{
        //    get { return (int)GetValue(ColsProperty); }
        //    set { SetValue(ColsProperty, value); }
        //}
        //Using a DependencyProperty as the backing store for Rows.This enables animation, styling,

        //public static readonly DependencyProperty RowsProperty =
        //DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty ColsProperty =
        //DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty MazeProperty =
        //DependencyProperty.Register("Maze", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty InitialPosProperty =
        //DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeUserControl), null);

        //public static readonly DependencyProperty GoalPosProperty =
        //DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeUserControl), null);

        //public void CreatePoints()
        //{
        //    this.start = new Point(this.xStart, this.yStart);
        //    this.end = new Point(this.xEnd, this.yEnd);
        //    this.prev = new Point(this.xStart, this.yStart);
        //    this.current = new Point(this.xStart, this.yStart);
        //}

        public void Draw()
        {
            // Rows="5" Cols="5"
            //Maze = "1,0,1,0,0,0,1,0,1,1,1,1,1,0,0,1,0,0,0,1,1,0,1,1,0" InitialPos = "0,1"
            //GoalPos = "2,3"
            int i, j;

            //putting labels to colors inside
            int index0 = 0;
            
            this.rows = 5;
            this.cols = 5;
            string maze = "1,0,1,0,0,0,1,0,1,1,1,1,1,0,0,1,0,0,0,1,1,0,1,1,0";
            int rectWidth = (int)this.MyCanvas.Width / this.cols;
            int rectHeight = (int)this.MyCanvas.Height / this.rows;
            string InitialPos = "0,1";
            string GoalPos = "2,3";
            this.rectList = new List<Rectangle>();
            for (i = 0; i < this.rows; i++)
            {
                for (j = 0; j < this.cols; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Width = rectWidth;
                    rect.Height = rectHeight;
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.Fill = new SolidColorBrush(Colors.White);
                    int w = j * rectWidth;
                    int h = i * rectHeight;
                    Canvas.SetLeft(rect, w);
                    Canvas.SetTop(rect, h);
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

                    if (i == (int)(InitialPos[0]-'0') && j == (int)(InitialPos[2] - '0')) // the current place of the player
                    {
                        rect.Stroke = new SolidColorBrush(Colors.Red);
                        rect.Fill = new SolidColorBrush(Colors.Red);
                    }
                    else if (i == (int)(GoalPos[0]-'0') && j == (int)(GoalPos[2]-'0')) // the current place of the player
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

        private void MazeUserControl_OnInitialized(object sender, EventArgs e)
        {
            //this.CreatePoints();
            this.Draw();
        }
    }
}
