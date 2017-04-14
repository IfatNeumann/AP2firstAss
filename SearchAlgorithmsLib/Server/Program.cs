using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientHandler ch = new View();
            MyServer server = new MyServer(1,ch);
            server.StartConnection();
            /*
            Singleton.GetGamesList();
            Singleton.GetMazeLS();
            Singleton.GetModel();
            Singleton.GetSolutionLS();

            // open all the classes of mvp model
            IPresenter presenter = new Controller();
            IModel model = Singleton.GetModel();
            IView view = new View();
            presenter.View = view;
            presenter.Model = model;
            model.Controller = presenter;
            view.Controller = presenter;

            // start conection
            view.MY_PORT_NUMBER = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            view.StartConnection();
            */
        }
    }
}
