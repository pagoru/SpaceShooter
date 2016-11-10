using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public float startWait;
    public float spawnWait;

    public float minHazardSize;
    public float maxHazardSize;

    public GameObject medicalBox;
    public int timeEveryMedicalBox;

    public Canvas canvasGUI;

    private bool gameOver;
    private bool restart;
    private int score;

    private long lastTimeMedicalBox;

    void Start()
    {
        Transform transform = canvasGUI.transform;
        transform.Find("RestartText").GetComponent<Text>().text = 
            transform.Find("GameOverText").GetComponent<Text>().text = "";

        gameOver = restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        lastTimeMedicalBox = Utils.getTimestamp();
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
                restart = true;
            }
            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            spawnGameObejct(hazard);

            long currentTime = Utils.getTimestamp();
            if(lastTimeMedicalBox + timeEveryMedicalBox <= currentTime)
            {
                spawnGameObejct(medicalBox);
                lastTimeMedicalBox = currentTime;
            }
            yield return new WaitForSeconds(spawnWait);
        }

    }

    private void spawnGameObejct(GameObject gObject)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;

        if (gObject.tag == "Asteroid")
        {
            float size = Random.Range(minHazardSize, maxHazardSize);
            gObject.transform.localScale = new Vector3(size, size, size);
        }
        
        Instantiate(gObject, spawnPosition, spawnRotation);
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
        canvasGUI.transform.Find("RestartText").GetComponent<Text>().text = "Press 'R' for Restart";
        gameOver = true;
    }

    public void showHearts(int number)
    {
        string heartsText = "";
        
        if (number > 0)
        {
            for (int i = 0; i < number; i++)
            {
                heartsText += "❤ ";
            }
            heartsText = heartsText.Substring(0, heartsText.Length - 1);
        }
        canvasGUI.transform.Find("HeartsText").GetComponent<Text>().text = heartsText;
    }

}
