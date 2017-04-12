using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server
{
    public class Controller : IController
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

        public string HandleRequest(string option, Socket client)
        {
            Dictionary<string, ICommand> commandDic = new Dictionary<string, ICommand>()
            {
                {"Generate", new Generate()},
                {"Solve", new Solve()},
                {"Start", new Start()},
                {"List", new List()},
                {"Play", new Play()},
                {"Close", new Close()}
            };

            char[] whitespace = new char[] { ' ', '\t' };
            string[] split = option.Split(whitespace);
            option = split[0];

            ICommand value;
            string output = "";
            string param1 = "", param2 = "";
            if (split.Length == 2)
            {
                param1 = split[1];
            }
            else if (split.Length == 3)
            {
                param1 = split[1];
                param2 = split[2];
            }

            Params c = new Params(param1, param2, client);

            if (commandDic.ContainsKey(option))
            {
                output = commandDic[option].doMission(c);
            }

            return output;
        }
    }
}
