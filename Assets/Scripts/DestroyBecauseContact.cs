using UnityEngine;
using System.Collections;

public class DestroyBecauseContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scorevalue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Error with 'Gamecontroller'");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.AddScore(scorevalue);
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
