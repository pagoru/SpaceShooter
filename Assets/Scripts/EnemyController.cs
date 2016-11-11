using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class EnemyController : MonoBehaviour
{
    public GameObject shot;
    public float tilt;
    public float timeBoltFired;

    private GameObject player;

    private long lastTimeBoltFired;
    private float originalTimeboltFired;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        lastTimeBoltFired = Utils.getTimestamp();

        originalTimeboltFired = timeBoltFired;
    }

    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (player == null)
        {
            rigidbody.velocity = new Vector3(0f, 0f, -50f);
            return;
        }

        Vector3 currentPlayerPosition = player.transform.position;
        Vector3 currentEnemyPosition = this.transform.position;
        float xVelocity = 2f;

        if (currentPlayerPosition.z + 50f < currentEnemyPosition.z)
        {
            rigidbody.velocity = new Vector3(0f, 0f, -10f);
            timeBoltFired = originalTimeboltFired;
        }
        else
        {
            rigidbody.velocity = new Vector3(0f, 0f, -25f);
            timeBoltFired = originalTimeboltFired/8;
            xVelocity *= 2;
        }

        if (currentPlayerPosition.x - 2f > currentEnemyPosition.x)
        {
            rigidbody.velocity = new Vector3(xVelocity, 0f, rigidbody.velocity.z);
        }
        else if (currentPlayerPosition.x + 2f < currentEnemyPosition.x)
        {
            rigidbody.velocity = new Vector3(-xVelocity, 0f, rigidbody.velocity.z);
        }

        if(lastTimeBoltFired + timeBoltFired < Utils.getTimestamp())
        {
            Vector3 pos = transform.position;
            Instantiate(shot, new Vector3(pos.x, 0.0f, pos.z - 0.8f), shot.transform.rotation);

            lastTimeBoltFired = Utils.getTimestamp();
        }
    }
}
