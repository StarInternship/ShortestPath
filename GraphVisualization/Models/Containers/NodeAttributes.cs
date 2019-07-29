using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphVisualization.Models.Containers
{
    public class NodeAttributes
    {
        
        string text { get; set; }
        int x { get; set; }
        int y { get; set; }


        public NodeAttributes(string index ,int x, int y)
        {
            text = index;
            this.x = x;
            this.y = y;
        }

    }
}