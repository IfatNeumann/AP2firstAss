using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server
{
    class Controller : IController
    {
        private IView view;
        private IModel model;

        public IView View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
            }
        }
        public IModel Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }


        public Controller()
        {

        }

        public string HandleRequest(string data, Socket client)
        {
            string output = this.Model.HandleRequest(data, client);

            return output;
        }
    }
}
