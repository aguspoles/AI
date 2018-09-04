using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGraph {

	public List<PathNode> nodes;
	public List<PathEdge> edges;

	public PathGraph(List<PathNode> nodes, List<PathEdge> edges) {
        this.nodes = nodes;
        this.edges = edges;
    }

}
