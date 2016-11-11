using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scorevalue;

    private GameController gameController;
    private PlayerController playerController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Asteroid":
                break;
            case "Enemy":
            case "EnemyBolt":
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
            case "Player":
                playerController.SubstractHeartOrGameOver(gameObject);
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
