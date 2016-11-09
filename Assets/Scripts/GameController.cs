using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float startWait;
    public float spawnWait;

    public float minHazardSize;
    public float maxHazardSize;

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

    public int getScore()
    {
        return score;
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
            spawnHazard();
            yield return new WaitForSeconds(spawnWait);
        }

    }

    private void spawnHazard()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject hazard = hazards[Random.Range(0, hazards.Length)];

        float size = Random.Range(minHazardSize, maxHazardSize);
        hazard.transform.localScale = new Vector3(size, size, size);
        
        GameObject gameObejct = (GameObject)Instantiate(hazard, spawnPosition, spawnRotation);
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
