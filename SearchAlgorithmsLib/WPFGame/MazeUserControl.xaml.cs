using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeLib;


namespace WPFGame
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Windows.Threading;

    using MazeGeneratorLib;

    using UserControl = System.Windows.Controls.UserControl;

    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeUserControl : UserControl
    {
        /// <summary>
        /// The rectangle width
        /// </summary>
        private int rectWidth, rectHeight;
        
        /// <summary>
        /// The rectangle list
        /// </summary>
        private List<Rectangle> rectList;
        
        /// <summary>
        /// The player record
        /// </summary>
        private Rectangle playerRec;
        
        /// <summary>
        /// My maze
        /// </summary>
        private Maze myMaze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeUserControl"/> class.
        /// </summary>
        public MazeUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public string Cols
        {
            get { return (string)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the string maze.
        /// </summary>
        /// <value>
        /// The string maze.
        /// </value>
        public string StringMaze
        {
            get { return (string)GetValue(StringMazeProperty); }
            set { SetValue(StringMazeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the curr point.
        /// </summary>
        /// <value>
        /// The curr point.
        /// </value>
        public string CurrPoint
        {
            get { return (string)GetValue(CurrPointProperty); }
            set
            {
                SetValue(CurrPointProperty, value);
                //this.UpdateMaze();
            }
        }

        /// <summary>
        /// Gets or sets the second curr point.
        /// </summary>
        /// <value>
        /// The second curr point.
        /// </value>
        public string SecondCurrPoint
        {
            get { return (string)GetValue(SecondCurrPointProperty); }
            set
            {
                SetValue(SecondCurrPointProperty, value);
                //this.UpdateMaze();
            }
        }

        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        /// <summary>
        /// The string maze property
        /// </summary>
        public static readonly DependencyProperty StringMazeProperty =
            DependencyProperty.Register("StringMaze", typeof(string), typeof(MazeUserControl), null);

        /// <summary>
        /// The curr point property
        /// </summary>
        public static readonly DependencyProperty CurrPointProperty =
            DependencyProperty.Register("CurrPoint", typeof(string), typeof(MazeUserControl), new UIPropertyMetadata(ChangePos));

        /// <summary>
        /// The second curr point property
        /// </summary>
        public static readonly DependencyProperty SecondCurrPointProperty =
            DependencyProperty.Register("SecondCurrPoint", typeof(string), typeof(MazeUserControl), new UIPropertyMetadata(ChangeSecPos));

        /// <summary>
        /// Changes the position.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void ChangePos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeUserControl userControl = d as MazeUserControl;
            userControl.UpdateMaze(e.NewValue.ToString());
        }

        /// <summary>
        /// Changes the sec position.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void ChangeSecPos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeUserControl userControl = d as MazeUserControl;
            userControl.UpdateMaze(e.NewValue.ToString());
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            int i, j, iLocation, jLocation;
            int rows = int.Parse(this.Rows), cols = int.Parse(this.Cols);
            this.myMaze = Maze.FromJSON(this.StringMaze);
            //set the start point
            int iStart = this.myMaze.InitialPos.Row;
            int jStart = this.myMaze.InitialPos.Col;
            //set the end point
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
                    //create rectangle
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
            //place the rectangle
            Canvas.SetTop(this.playerRec, iStart * this.rectHeight);
            Canvas.SetLeft(this.playerRec, jStart * this.rectWidth);
            this.playerRec.Fill = marco;
            this.MyCanvas.Children.Add(this.playerRec);
            this.rectList.Add(this.playerRec);
        }

        /// <summary>
        /// Handles the OnLoaded event of the MazeBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeBoard_OnLoaded(object sender, RoutedEventArgs e)
        {
            //fraw the maze
            this.Draw();
        }

        /// <summary>
        /// Updates the maze.
        /// </summary>
        /// <param name="newW">The new w.</param>
        public void UpdateMaze(string newW)
        {
            //move the player acording to the key pressed
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
    }
}

