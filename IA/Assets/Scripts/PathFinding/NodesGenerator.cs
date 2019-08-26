using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class NodesGenerator : MonoBehaviour
    {
        public List<Node> pathNodes = new List<Node>();

        [SerializeField]
        private uint totalNodes = 0;
        public GameObject nodeGizmo;
        public GameObject map;
        public bool nodesVisible = true;
        [Range(0, 10)]
        public float step = 1;
        public float separationX = 1;
        public float separationY = 1;
        public float distanceOfNeighbors = 1;

        private List<GameObject> nodesGizmos = new List<GameObject>();
        private bool showGizmo = true;

        private void Start()
        {
            GenerateNodes();
        }

        void GenerateNodes()
        {
            nodesGizmos.Clear();
            pathNodes.Clear();
            totalNodes = 0;
            showGizmo = true;

            Vector3 rayPos = map.transform.position;

            for (int x = 0; x < 10 * step; x++)
            {
                float sepX = separationX * x;
                for (int y = 0; y < 10 * step; y++)
                {
                    float sepY = separationY * y;
                    rayPos = new Vector3(transform.position.x + sepX, transform.position.y, transform.position.z + sepY);
                    Ray ray1 = new Ray(rayPos, -Vector3.up);
                    List<Vector3> rayOrigins = new List<Vector3> { ray1.origin };

                    RaycastHit hit;

                    if (Physics.Raycast(ray1, out hit))
                    {
                        CheckHit(hit);
                    }

                    Vector3 ray2Pos = new Vector3(-rayPos.x, rayPos.y, -rayPos.z);
                    if (!rayOrigins.Contains(ray2Pos))
                    {
                        Ray ray2 = new Ray(ray2Pos, -Vector3.up);
                        if (Physics.Raycast(ray2, out hit))
                        {
                            CheckHit(hit);
                        }
                        rayOrigins.Add(ray2Pos);
                    }

                    Vector3 ray3Pos = new Vector3(-rayPos.x, rayPos.y, rayPos.z);
                    if (!rayOrigins.Contains(ray3Pos))
                    {
                        Ray ray3 = new Ray(ray3Pos, -Vector3.up);
                        if (Physics.Raycast(ray3, out hit))
                        {
                            CheckHit(hit);
                        }
                        rayOrigins.Add(ray3Pos);
                    }

                    Vector3 ray4Pos = new Vector3(rayPos.x, rayPos.y, -rayPos.z);
                    if (!rayOrigins.Contains(ray4Pos))
                    {
                        Ray ray4 = new Ray(ray4Pos, -Vector3.up);
                        if (Physics.Raycast(ray4, out hit))
                        {
                            CheckHit(hit);
                        }
                        rayOrigins.Add(ray4Pos);
                    }
                }
            }

            SetNeighbors();
        }

        void CheckHit(RaycastHit hit)
        {
            if (hit.collider.tag != "Obstacle")
            {
                Node node = new Node();
                node.id = "Node_id_" + totalNodes;
                node.name = "Node_" + totalNodes;
                pathNodes.Add(node);
                totalNodes++;

                if (nodeGizmo)
                {
                    GameObject go = Instantiate(nodeGizmo, hit.point, hit.transform.rotation, transform);
                    nodesGizmos.Add(go);
                }
            }
        }

        void SetNeighbors()
        {
            foreach(Node node in pathNodes)
            {
                node.neighborsNodes = GetNeighbors(node, distanceOfNeighbors);
            }
        }

        List<Node> GetNeighbors(Node node, float distance)
        {
            List<Node> neighbors = new List<Node>();

            foreach(Node n in pathNodes)
            {
                if(node != n && Vector3.Distance(node.position, n.position) <= distance)
                {
                    neighbors.Add(n);
                }
            }

            return neighbors;
        }

        void Update()
        {
            if (nodesVisible && showGizmo)
            {
                ShowGizmo(true);
            }
            else if (!nodesVisible && !showGizmo)
            {
                ShowGizmo(false);
            }
        }

        void ShowGizmo(bool show)
        {
            foreach (GameObject go in nodesGizmos)
            {
                go.SetActive(show);
            }
            showGizmo = !show;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
    }

}