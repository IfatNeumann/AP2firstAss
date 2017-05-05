using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    class MainWindowVM : ViewModel
    {
        private IMainWindowModel model;


        public MainWindowVM(IMainWindowModel model)
        {
            this.model = model;
        }
    }
}
