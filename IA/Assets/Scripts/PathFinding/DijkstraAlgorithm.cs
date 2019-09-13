using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public class DijkstraAlgorithm
    {
        public List<Node> nodes;
        private List<Node> exploredNodes;
        private List<Node> unexploredNodes;

        private Dictionary<Node, float> distance;

        public DijkstraAlgorithm()
        {
        }

        public List<Node> Execute(Node origin, Node target)
        {
            origin.parent = null;
            unexploredNodes = new List<Node>();
            exploredNodes = new List<Node>();
            distance = new Dictionary<Node, float>();

            distance.Add(origin, 0);
            unexploredNodes.Add(origin);

            while (unexploredNodes.Count > 0)
            {
                Node current = getMinimum(unexploredNodes);
                if (current == target)
                    return GetPath(current);

                exploredNodes.Add(current);
                unexploredNodes.Remove(current);

                findMinimalDistances(current);
            }

            return null;
        }

        private void findMinimalDistances(Node node)
        {
            List<Node> neighbors = node.neighborsNodes;

            foreach (Node neighbor in neighbors)
            {
                if (exploredNodes.Contains(neighbor))
                    continue;

                if (getShortestDistance(neighbor) > getShortestDistance(node)
                        + getDistance(node, neighbor))
                {
                    distance.Add(neighbor, getShortestDistance(node)
                            + getDistance(node, neighbor));

                    if (!unexploredNodes.Contains(neighbor))
                    {
                        unexploredNodes.Add(neighbor);
                        neighbor.parent = node;
                    }
                }
            }

        }

        private float getDistance(Node node, Node target)
        {
            return Mathf.Abs(Vector3.Distance(node.position, target.position));
        }

        private Node getMinimum(List<Node> nodes)
        {
            Node minimum = null;
            foreach (Node node in nodes)
            {
                if (minimum == null)
                {
                    minimum = node;
                }
                else
                {
                    if (getShortestDistance(node) < getShortestDistance(minimum))
                    {
                        minimum = node;
                    }
                }
            }
            return minimum;
        }

        private float getShortestDistance(Node destination)
        {
            //Debug.Log(distance);
            float d = distance[destination];
            if (d == null)
            {
                return float.MaxValue;
            }
            else
            {
                return d;
            }
        }


        public List<Node> GetPath(Node target)
        {
            List<Node> path = new List<Node>();

            path.Add(target);

            Node n = target;
            while(n.parent != null)
            {
                path.Add(n.parent);
                n = n.parent;
            }

            return path;
        }
    }

}
