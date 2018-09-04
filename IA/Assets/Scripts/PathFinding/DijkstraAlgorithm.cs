using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAlgorithm  {

	private List<PathNode> nodes;
    private List<PathEdge> edges;
    private List<PathNode> settledNodes;
    private List<PathNode> unSettledNodes;
    private Dictionary<PathNode, PathNode> predecessors;
    private Dictionary<PathNode, int> distance;

    public DijkstraAlgorithm(PathGraph graph) {
        this.nodes = new List<PathNode>(graph.nodes);
        this.edges = new List<PathEdge>(graph.edges);
    }

	public void execute(PathNode source) {
        settledNodes = new List<PathNode>();
        unSettledNodes = new List<PathNode>();
        distance = new Dictionary<PathNode, int>();
        predecessors = new Dictionary<PathNode, PathNode>();
        distance.Add(source, 0);
        unSettledNodes.Add(source);
        while (unSettledNodes.Count > 0) {
            PathNode node = getMinimum(unSettledNodes);
            settledNodes.Add(node);
            unSettledNodes.Remove(node);
            findMinimalDistances(node);
        }
    }

	private void findMinimalDistances(PathNode node) {
        List<PathNode> adjacentNodes = getNeighbors(node);
        foreach(PathNode target in adjacentNodes) {
            if (getShortestDistance(target) > getShortestDistance(node)
                    + getDistance(node, target)) {
                distance.Add(target, getShortestDistance(node)
                        + getDistance(node, target));
                predecessors.Add(target, node);
                unSettledNodes.Add(target);
            }
        }

    }

    private int getDistance(PathNode node, PathNode target) {
        foreach (PathEdge edge in edges) {
            if (edge.origin == node
                    && edge.destination == target) {
                return edge.cost;
            }
        }
        throw new System.Exception("Should not happen");
    }

	private List<PathNode> getNeighbors(PathNode node) {
        List<PathNode> neighbors = new List<PathNode>();
        foreach (PathEdge edge in edges) {
            if (edge.origin == node
                    && !isSettled(edge.destination)) {
                neighbors.Add(edge.destination);
            }
        }
        return neighbors;
    }

    private PathNode getMinimum(List<PathNode> nodes) {
        PathNode minimum = null;
        foreach (PathNode node in nodes) {
            if (minimum == null) {
                minimum = node;
            } else {
                if (getShortestDistance(node) < getShortestDistance(minimum)) {
                    minimum = node;
                }
            }
        }
        return minimum;
    }

    private bool isSettled(PathNode node) {
        return settledNodes.Contains(node);
    }

    private int getShortestDistance(PathNode destination) {
        int d = distance[destination];
        if (d == null) {
            return int.MaxValue;
        } else {
            return d;
        }
    }

    /*
     * This method returns the path from the source to the selected target and
     * NULL if no path exists
     */
    public List<PathNode> getPath(PathNode target) {
        List<PathNode> path = new List<PathNode>();
        PathNode step = target;
        // check if a path exists
        if (predecessors[step] == null) {
            return null;
        }
        path.Add(step);
        while (predecessors[step] != null) {
            step = predecessors[step];
            path.Add(step);
        }
        // Put it into the correct order
		path.Reverse();
        return path;
    }
}
