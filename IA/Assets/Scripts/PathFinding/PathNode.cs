using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public class Node
    {
        public string id;
        public string name;
        public Vector3 position;
        public Node parent;
        public List<Node> neighborsNodes;
        public float weight;
        public bool open = true;

        public Node() { }

        public Node(string id, string name, Vector3 pos)
        {
            this.id = id;
            this.name = name;
            this.position = pos;
        }
    }

}
