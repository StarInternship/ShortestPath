using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphVisualization.Models
{
    public class Component
    {
        public List<NodeContainer> Nodes { get; } = new List<NodeContainer>();
        public List<EdgeContainer> OuterEdges { get; } = new List<EdgeContainer>();
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Radius { get; set; }

        public void ArrangeNodes()
        {

        }
    }
}