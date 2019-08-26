using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z));
    }
}
