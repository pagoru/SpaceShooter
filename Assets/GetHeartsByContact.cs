using UnityEngine;
using System.Collections;

public class GetHeartsByContact : MonoBehaviour
{

    public GameObject explosion;
    public int substractedScoreDestroyedMedicalBox;

    private PlayerController playerController;
    private GameController gameController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
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
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                gameController.SubstractScore(substractedScoreDestroyedMedicalBox);
                break;
            case "Player":
                playerController.AddHeart();
                Destroy(gameObject);
                break;
        }
    }
}
