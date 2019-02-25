using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPostions : MonoBehaviour {

	private List<PathNode> nodes;
    private List<PathEdge> edges;
	private List<Pair<PathNode, PathNode>> originAndTargetNodes;

	public List<PairOfTransform> positions;

	// Use this for initialization
	void Start () {
		nodes = new List<PathNode>();
        edges = new List<PathEdge>();
		originAndTargetNodes = new List<Pair<PathNode, PathNode>>();
		
		SetNodes(positions);

        for (int i = 0; i < originAndTargetNodes.Count; i++) {
			PathNode origin = originAndTargetNodes[i].First;
			PathNode target = originAndTargetNodes[i].Second;
            nodes.Add(origin);
			//check to not add duplicates
			nodes.Add(target);

			Vector3 originPos = origin.position;
			Vector3 targetPos = target.position;

			float cost = Vector3.Distance(originPos, targetPos);

			addLane("Edge_" + i, origin, target, (int)cost);
        }

        PathGraph graph = new PathGraph(nodes, edges);
        DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(graph);
        dijkstra.execute(originAndTargetNodes[0].First);
        /*List<PathNode> path = dijkstra.getPath(originAndTargetNodes[2].First);

        foreach (PathNode node in path) {
            Debug.Log(node);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void addLane(string laneId, PathNode source, PathNode dest,
            float duration) {
        PathEdge lane = new PathEdge(laneId, source, dest, (int)duration);
        edges.Add(lane);
    }

	private void SetNodes(List<PairOfVector3> positions){
		 for (int i = 0; i < positions.Count; i++) {
			 PathNode origin = new PathNode("Node_o_" + i, "Node_o_" + i, positions[i].First);
			 PathNode destination = new PathNode("Node_t_" + i, "Node_t_" + i, positions[i].Second);
			 originAndTargetNodes.Add(new Pair<PathNode, PathNode>(origin, destination));
		 }
	}

	private void SetNodes(List<PairOfTransform> positions){
		 for (int i = 0; i < positions.Count; i++) {
			 PathNode origin = new PathNode("Node_o_" + i, "Node_o_" + i, positions[i].origin.position);
			 PathNode destination = new PathNode("Node_t_" + i, "Node_t_" + i, positions[i].target.position);
			 originAndTargetNodes.Add(new Pair<PathNode, PathNode>(origin, destination));
		 }
	}


	[System.Serializable]
	public class Pair<T, U> {
		public Pair () { }

		public Pair (T first, U second) {
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }
		public U Second { get; set; }
	};

	[System.Serializable]
	public class PairOfVector3 : Pair<Vector3, Vector3>{
		public Vector3 origin;
		public Vector3 target;
		public PairOfVector3(Vector3 origin, Vector3 target){
			this.origin = origin;
			this.target = target;
		}
	}

	[System.Serializable]
	public class PairOfTransform : Pair<Transform, Transform>{
		public Transform origin;
		public Transform target;
		public PairOfTransform(Transform origin, Transform target){
			this.origin = origin;
			this.target = target;
		}
	}
}
