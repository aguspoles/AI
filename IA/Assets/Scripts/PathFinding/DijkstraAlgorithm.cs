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

        public void Execute(Node origin)
        {
            unexploredNodes = new List<Node>();
            exploredNodes = new List<Node>();
            distance = new Dictionary<Node, float>();

            distance.Add(origin, 0);
            unexploredNodes.Add(origin);
            while (unexploredNodes.Count > 0)
            {
                Node node = getMinimum(unexploredNodes);
                exploredNodes.Add(node);
                unexploredNodes.Remove(node);
                findMinimalDistances(node);
            }
        }

        private void findMinimalDistances(Node node)
        {
            List<Node> neighbors = node.neighborsNodes;
            foreach (Node target in neighbors)
            {
                if (getShortestDistance(target) > getShortestDistance(node)
                        + getDistance(node, target))
                {
                    distance.Add(target, getShortestDistance(node)
                            + getDistance(node, target));

                    unexploredNodes.Add(target);
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


        public List<Node> getPath(Node target)
        {
            List<Node> path = new List<Node>();

            return path;
        }
    }

}
