using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GraphVisualization.Controllers
{
    public class MainController
    {
        public static MainController Instance { get; } = new MainController();
        private readonly string graphPath = HostingEnvironment.MapPath("~/../TestFiles");
        private MainController()
        {
        }

        internal string[] GetGraphsList()
        {
            return Directory.GetFiles(graphPath);
        }
    }
}