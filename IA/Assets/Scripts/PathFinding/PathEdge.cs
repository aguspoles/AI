using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEdge {

	public string id;
	public PathNode origin;
	public PathNode destination;
	public int cost;

	public PathEdge(string id, PathNode origin, PathNode destination, int cost){
		this.id = id;
		this.origin = origin;
		this.destination = destination;
		this.cost = cost;
	}
}
