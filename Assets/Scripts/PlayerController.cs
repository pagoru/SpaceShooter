using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject enemyExplosion;
    public GameObject playerExplosion;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public GameObject shotR;
    public GameObject shotL;
    public Transform shotSpawn;
    public float fireRate;

    public Camera mainCamera;
    public GameObject starfield;
    public GameObject background;

    private float nextFire;
    private GameController gameController;

    private int hearts;

    private Vector3 initialMainCamera;
    private Vector3 initialStarfield;
    private Vector3 initialBackground;

    void Start()
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
        hearts = 3;
        gameController.showHearts(hearts);

        initialMainCamera = mainCamera.transform.position;
        initialStarfield = starfield.transform.position;
        initialBackground = background.transform.position;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            int score = gameController.getScore();

            if (score >= 3000)
            {
                TripleShoot();
            }
            else if (score >= 2000)
            {
                DoubleShoot();
            }
            else if (score >= 1000)
            {
                DoubleSimpleshoot();
            }
            else
            {
                SingleShoot();
            }

            GetComponent<AudioSource>().Play();
        }
    }

    public void SubstractHeart()
    {
        hearts--;
        gameController.showHearts(getHearts());
    }

    public void AddHeart()
    {
        hearts++;
        gameController.showHearts(getHearts());

    }

    public int getHearts()
    {
        return hearts;
    }

    private void SingleShoot()
    {
        Instantiate(shot, shotSpawn.position, Quaternion.Euler(shotSpawn.rotation.z, 0.0f, shotSpawn.rotation.x));
    }
    private void DoubleShoot()
    {
        Vector3 newPosition = new Vector3(shotSpawn.position.x + 0.2f, shotSpawn.position.y, shotSpawn.position.z);
        Instantiate(shot, newPosition, Quaternion.Euler(shotSpawn.rotation.z, 0.0f, shotSpawn.rotation.x));
        newPosition.x -= 0.4f;
        Instantiate(shot, newPosition, Quaternion.Euler(shotSpawn.rotation.z, 0.0f, shotSpawn.rotation.x));
    }
    private void DoubleSimpleshoot()
    {
        Vector3 newPosition = new Vector3(shotSpawn.position.x + 0.2f, shotSpawn.position.y, shotSpawn.position.z);
        Instantiate(shotR, newPosition, Quaternion.Euler(0.0f, 22.5f, 0.0f));
        newPosition.x -= 0.4f;
        Instantiate(shotL, newPosition, Quaternion.Euler(0.0f, -22.5f, 0.0f));
    }
    private void TripleShoot()
    {
        Vector3 newPosition = new Vector3(shotSpawn.position.x + 0.2f, shotSpawn.position.y, shotSpawn.position.z);
        Instantiate(shotR, newPosition, Quaternion.Euler(0.0f, 22.5f, 0.0f));
        newPosition.x -= 0.2f;
        Instantiate(shot, newPosition, shot.transform.rotation);
        newPosition.x -= 0.2f;
        Instantiate(shotL, newPosition, Quaternion.Euler(0.0f, -22.5f, 0.0f));
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        Vector3 position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        rigidbody.position = position;
        mainCamera.transform.position = new Vector3(position.x + initialMainCamera.x, position.y + initialMainCamera.y, position.z + initialMainCamera.z);
        starfield.transform.position = new Vector3(position.x + initialStarfield.x, position.y + initialStarfield.y, position.z + initialStarfield.z);
        background.transform.position = new Vector3(position.x + initialBackground.x, position.y + initialBackground.y, position.z + +initialBackground.z);
        
        rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * tilt, 0.0f, rigidbody.velocity.x * - tilt);

    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
            case "EnemyBolt":
                if (other.tag == "Enemy")
                {
                    SubstractHeart();
                    SubstractHeart();
                    SubstractHeart();
                }
                SubstractHeartOrGameOver(other.gameObject);
                break;
        }
    }

    public void SubstractHeartOrGameOver(GameObject other)
    {
        SubstractHeart();
        if (getHearts() <= 0)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
    }
}
