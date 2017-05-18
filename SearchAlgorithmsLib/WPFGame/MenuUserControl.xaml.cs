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
    /// Interaction logic for MenuUserControl.xaml
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
        }
        public int Rows
        {
            get { return (int)this.GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public String MyName
        {
            get { return (String)GetValue(MyNameProperty); }
            set { SetValue(MyNameProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyNameProperty =
            DependencyProperty.Register("MyName", typeof(string), typeof(MenuUserControl), new PropertyMetadata(0));

        public static readonly DependencyProperty RowsProperty = 
            DependencyProperty.Register("Rows", typeof(int), typeof(MenuUserControl), new PropertyMetadata(0));

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MenuUserControl), new
                PropertyMetadata(0));

    }
}

