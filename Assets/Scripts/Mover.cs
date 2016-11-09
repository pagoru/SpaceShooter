using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    private float yPosition;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
        yPosition = rigidbody.position.y;
    }

    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 position = rigidbody.transform.position;
        rigidbody.transform.position = new Vector3(position.x, yPosition, position.z);
    }
}
