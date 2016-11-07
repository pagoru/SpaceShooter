using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public int negativeScoreForEveryFire;

    private float nextFire;
    private GameController gameController;

    void Update()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Error with 'Gamecontroller'");
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Eliminada la dirección y de rotation debido al movimiento hacia adelante y atrás de la nave.
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(shotSpawn.rotation.z, 0.0f, shotSpawn.rotation.x));

            GetComponent<AudioSource>().Play();
            gameController.AddScore(negativeScoreForEveryFire);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * tilt, 0.0f, rigidbody.velocity.x * - tilt);
    }

}
