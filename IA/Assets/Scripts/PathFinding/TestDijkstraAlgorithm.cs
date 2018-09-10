using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDijkstraAlgorithm {

   /* private List<PathNode> nodes;
    private List<PathEdge> edges;

    public void testExcute() {
        nodes = new List<PathNode>();
        edges = new List<PathEdge>();
        for (int i = 0; i < 11; i++) {
            PathNode location = new PathNode("Node_" + i, "Node_" + i);
            nodes.Add(location);
        }

        addLane("Edge_0", 0, 1, 85);
        addLane("Edge_1", 0, 2, 217);
        addLane("Edge_2", 0, 4, 173);
        addLane("Edge_3", 2, 6, 186);
        addLane("Edge_4", 2, 7, 103);
        addLane("Edge_5", 3, 7, 183);
        addLane("Edge_6", 5, 8, 250);
        addLane("Edge_7", 8, 9, 84);
        addLane("Edge_8", 7, 9, 167);
        addLane("Edge_9", 4, 9, 502);
        addLane("Edge_10", 9, 10, 40);
        addLane("Edge_11", 1, 10, 600);

        // Lets check from location Loc_1 to Loc_10
        PathGraph graph = new PathGraph(nodes, edges);
        DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(graph);
        dijkstra.execute(nodes[0]);
        List<PathNode> path = dijkstra.getPath(nodes[10]);

        foreach (PathNode node in path) {
            Debug.Log(node);
        }

    }

    private void addLane(string laneId, int sourceLocNo, int destLocNo,
            int duration) {
        PathEdge lane = new PathEdge(laneId, nodes[sourceLocNo], nodes[destLocNo], duration );
        edges.Add(lane);
    }*/
}