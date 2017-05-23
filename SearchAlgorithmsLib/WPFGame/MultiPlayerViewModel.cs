using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    public class MultiPlayerViewModel
    {
        private IMultiPlayerModel model;

        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
        }
    }
}
