using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

	public string id;
	public string name;
	public PathNode parent;
	public List<PathNode> adyacentsNodes;

	public PathNode(string id, string name){
		this.id = id;
		this.name = name;
	}
}
