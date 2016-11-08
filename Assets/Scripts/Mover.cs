using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }

    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 position = rigidbody.transform.position;
        rigidbody.transform.position = new Vector3(position.x, 0.0f, position.z);
    }
}
