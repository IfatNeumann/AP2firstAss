using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    /// <summary>
    /// the main window
    /// </summary>
    /// <seealso cref="WPFGame.ViewModel" />
    class MainWindowVM : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private IMainWindowModel model;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVM"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MainWindowVM(IMainWindowModel model)
        {
            this.model = model;
        }
    }
}
