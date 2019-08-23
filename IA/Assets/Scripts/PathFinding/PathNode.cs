using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

	public string id;
	public string name;
	public Vector3 position;
	public PathNode parent;
	public List<PathNode> neighborsNodes;
    public bool open = true;

    public PathNode() { }

	public PathNode(string id, string name, Vector3 pos){
		this.id = id;
		this.name = name;
		this.position = pos;
	}
}
