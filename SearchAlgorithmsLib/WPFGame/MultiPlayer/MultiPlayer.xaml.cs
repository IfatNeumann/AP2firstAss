using System;
using System.Threading.Tasks;
using System.Windows;


namespace WPFGame
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {

        private MultiPlayerViewModel vm;

        private IMultiPlayerModel model;

        private bool surpriseClose;

        public MultiPlayer()
        {
            this.model = new ApplicationMultiPlayerModel();
            this.InitializeComponent();
            this.vm = new MultiPlayerViewModel(this.model);
            this.DataContext = this.vm;
            this.surpriseClose = true;
        }



        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            WaitWindow win = new WaitWindow();
            win.Show();
            this.surpriseClose = false;
            this.Close();
            this.vm.StartGame();
            while (this.vm.NotReady) { }
            MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
            mulWin.Show();
            win.Close();
        }

        private void Join_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.VmGamesList !=null && this.vm.VmGamesList.Count != 0)
            {
                this.vm.JoinGame();
                while (this.vm.NotReady) { }
                MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
                mulWin.Show();
                this.surpriseClose = false;
                this.Close();
            }
        }
        

        private void On_Loaded(object sender, EventArgs e)
        {
            Task task = new Task(() => { this.vm.StartConnection(); });
            task.Start();
        }

        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.vm.VmGetList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.surpriseClose)
            {
                this.vm.CloseConnection();
            }
        }
    }
}
