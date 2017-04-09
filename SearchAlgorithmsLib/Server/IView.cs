using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IView
    {
        void StartConnection();
        int MY_PORT_NUMBER
        {
            get;
            set;
        }
    }
}
