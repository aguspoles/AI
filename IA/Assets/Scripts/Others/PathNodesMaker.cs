using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PathNodesMaker : MonoBehaviour
    {
        public static List<PathNode> pathNodes = new List<PathNode>();
        public List<int> pathNodes1 = new List<int>();
        private static uint totalNodes = 0;

        private void Start()
        {

        }

        void FixedUpdate()
        {
            RaycastHit hit;

            //Ray ray = new Ray(transform.position)
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                if (hit.collider.tag != "Obstacle")
                {
                    PathNode node = new PathNode();
                    node.id = "Node_id_" + totalNodes;
                    node.name = "Node_" + totalNodes;
                    pathNodes.Add(node);
                    pathNodes1.Add((int)totalNodes);
                    totalNodes++;
                    print("Found an object with tag: " + hit.collider.tag);
                    Debug.DrawLine(transform.position, hit.point);
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff", true);
        }
    }
}
