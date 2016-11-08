using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{

    public int negativeScoreForOutFire;

    private GameController gameController;

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
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);

        if(other.tag == "Bolt")
        {
            gameController.SubstractScore(negativeScoreForOutFire);
        }
    }
}
