using GraphVisualization.Models.Containers;

namespace GraphVisualization.Models
{
    public class NodeContainer
    {

        string id { get; set; }
        NodeAttributes attributes { get; set; }


        public NodeContainer(string index, int x, int y)
        {
            this.id= index;
            this.attributes = new NodeAttributes(index , x, y);
        }

    }
}