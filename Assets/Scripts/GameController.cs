using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float startWait;
    public float spawnWait;

    public Canvas canvasGUI;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        Transform transform = canvasGUI.transform;
        transform.Find("RestartText").GetComponent<Text>().text = 
            transform.Find("GameOverText").GetComponent<Text>().text = "";

        gameOver = restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("Main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (gameOver)
            {
                canvasGUI.transform.Find("RestartText").GetComponent<Text>().text = "Press 'R' for Restart";
                restart = true;
            }
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void SubstractScore(int newScoreValue)
    {
        score -= newScoreValue;
        if (score < 0)
        {
            score = 0;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        canvasGUI.transform.Find("ScoreText").GetComponent<Text>().text = "SCORE " + score;
    }

    public void GameOver()
    {
        canvasGUI.transform.Find("GameOverText").GetComponent<Text>().text = "GAME OVER";
        gameOver = true;
    }

}
