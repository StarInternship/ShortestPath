using System.Collections.Generic;

namespace ShortestPath.models
{
    public class PathFinder
    {
        private Node source;
        private Node target;
        private bool findAllPaths;
        private double maxDistance;
        private List<Path> result = new List<Path>();

        public PathFinder(Node source, Node target, bool findAllPaths, double maxDistance)
        {
            this.source = source;
            this.target = target;
            this.findAllPaths = findAllPaths;
            this.maxDistance = maxDistance;
        }

        public List<Path> Find()
        {
            return result;
        }

        private void Explore(Path currentPath, Node currentNode)
        {
            if (currentNode.Equals(target))
            {
                result.Add(new Path(currentPath));
            }

            currentNode.Visited = true;
            currentNode.Outs.ForEach(edge => {

                if (!edge.To.Visited)
                {
                    currentPath.Push(edge);
                    Explore( currentPath, edge.To);
                }
            });
            currentNode.Visited = false;
        }
    }
}
