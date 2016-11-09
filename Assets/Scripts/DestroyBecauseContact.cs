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
        switch (other.tag)
        {
            case "Asteroid":
            case "AsteroidBackground":
                break;
            case "Player":
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                DestroyAll(other);
                break;
            case "Bolt":
                int score = (int)Mathf.Abs((transform.localScale.x * scorevalue) - (scorevalue * 2));
                gameController.AddScore(score);
                DestroyAll(other);
                break;
        }
    }

    private void DestroyAll(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
