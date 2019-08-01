using System.Collections.Generic;

namespace GraphVisualization.Models
{
    public class BccCompact : ILayout
    {
        private readonly GraphContainer graph;
        private readonly List<Component> components = new List<Component>();
        public BccCompact(GraphContainer graph)
        {
            this.graph = graph;
        }

        public void ApplyLayout()
        {
            
        }

        private void MakeComponents()
        {

        }

        private void ArrangeComponents()
        {

        }
    }
}