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


namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        
        public MazeUserControl()
        {
            InitializeComponent();
        }

        public string Rows
        {
            get { return RowsProperty.ToString(); }
            set { SetValue(RowsProperty, value); }
        }

        public string Cols
        {
            get { return ColsProperty.ToString(); }
            set { SetValue(ColsProperty, value); }
        }

        public string Maze
        {
            get { return MazeProperty.ToString(); }
            set { SetValue(MazeProperty, value); }
        }

        public string InitialPos
        {
            get { return InitialPosProperty.ToString(); }
            set { SetValue(InitialPosProperty, value); }
        }

        public string GoalPos
        {
            get { return GoalPosProperty.ToString(); }
            set { SetValue(GoalPosProperty, value); }
        }

        //public int Cols
        //{
        //    get { return (int)GetValue(ColsProperty); }
        //    set { SetValue(ColsProperty, value); }
        //}
        // Using a DependencyProperty as the backing store for Rows. This enables animation, styling,

        public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register("Rows", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty ColsProperty =
        DependencyProperty.Register("Cols", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty MazeProperty =
        DependencyProperty.Register("Maze", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty InitialPosProperty =
        DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeUserControl), null);

        public static readonly DependencyProperty GoalPosProperty =
        DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeUserControl), null);
    }
}
