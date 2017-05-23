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
using System.Windows.Shapes;

namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel vm;

        private IMultiPlayerModel model;

        public MultiPlayerWindow(IMultiPlayerModel model)
        {

            this.model = model;
            this.InitializeComponent();
            this.vm = new MultiPlayerViewModel(model);
            this.DataContext = this.vm;
        }


    }
}
