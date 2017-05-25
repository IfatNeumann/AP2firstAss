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

        public MultiPlayer()
        {
            this.model = new ApplicationMultiPlayerModel();
            this.InitializeComponent();
            this.vm = new MultiPlayerViewModel(this.model);
            this.DataContext = this.vm;
        }



        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            WaitWindow win = new WaitWindow();
            win.Show();
            this.Close();
            this.vm.StartGame();
            while (this.vm.NotReady) { }
            MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
            mulWin.Show();
            win.Close();
        }

        private void Join_Button_Click(object sender, RoutedEventArgs e)
        {
            this.vm.JoinGame();
            while (this.vm.NotReady) { }
            MultiPlayerWindow mulWin = new MultiPlayerWindow(this.model);
            mulWin.Show();
            this.Close();
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

        private void UserSelectedItem(object sender, EventArgs e)
        {
            //hi
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.vm.CloseConnection();
        }
    }
}
