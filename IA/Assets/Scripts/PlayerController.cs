using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    Rigidbody rigidbody;
    Vector3 velocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        velocity = new Vector3(1, 0, 1).normalized * 10;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
    }

    public void MoveToPosition(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(rigidbody.position, pos, 10 * Time.fixedDeltaTime);
    }
}