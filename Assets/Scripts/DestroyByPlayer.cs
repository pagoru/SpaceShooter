using UnityEngine;
using System.Collections;

public class DestroyByPlayer : MonoBehaviour {

    public GameObject enemyExplosion;
    public int scoreValue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bolt":
                Instantiate(enemyExplosion, transform.position, transform.rotation);
                gameController.AddScore(scoreValue);
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
        }
    }

}
