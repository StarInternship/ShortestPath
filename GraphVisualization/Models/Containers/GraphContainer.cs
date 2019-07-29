using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphVisualization.Models
{
    public class GraphContainer
    {
        public List<EdgeContainer> edges { get; } = new List<EdgeContainer>();
        public List<NodeContainer> nodes { get; } = new List<NodeContainer>();

    }
}