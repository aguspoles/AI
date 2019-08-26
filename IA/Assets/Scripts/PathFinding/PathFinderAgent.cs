using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {

    public class PathFinderAgent : MonoBehaviour
    {
        DijkstraAlgorithm dijkstra;
        PlayerController controller;
        List<Node> currentPath;

        // Start is called before the first frame update
        void Start()
        {
            dijkstra = new DijkstraAlgorithm();
            controller = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.tag == "Floor")
                    {
                        Node node = GetNearestNode(hit.point);
                        dijkstra.Execute(node);
                        currentPath = dijkstra.getPath(node);
                    }
                }
            }

            GoToPoint(currentPath);
        }

        Node GetNearestNode(Vector3 pos)
        {
            Node retNode = null;
            float minDistance = Mathf.Abs(Vector3.Distance(dijkstra.nodes[0].position, pos));
            foreach(Node node in dijkstra.nodes)
            {
                float dist = Mathf.Abs(Vector3.Distance(node.position, pos));
                if (dist <= minDistance)
                {
                    minDistance = dist;
                    retNode = node;
                }
            }

            return retNode;
        }

        void GoToPoint(List<Node> path)
        {
            int i = 0;
            while(i < path.Count)
            {
                if (path[i].position != transform.position)
                {
                    controller.MoveToPosition(path[i].position);
                }
                else
                {
                    i++;
                }
            }
        }
    }

}
