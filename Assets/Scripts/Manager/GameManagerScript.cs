using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    float currentTime = 0;
    public float enemyTimer = 3;
    public TextMeshProUGUI countDownTimer;
    public TextMeshProUGUI enemyCounter;

    public GameObject[] enemyPrefabs;
    
    
    void Start()
    {
        if(SceneManagerScript.matchDuration != 0) currentTime = SceneManagerScript.matchDuration;
        else currentTime = 30;
        if(SceneManagerScript.enemySpawnDuration !=0) enemyTimer = SceneManagerScript.enemySpawnDuration;
        else enemyTimer = 5;

        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        
        if(currentTime <= 0)
        {
            currentTime = 0;
            countDownTimer.color = Color.red;
            SceneManager.LoadScene("GameOver");
        }

        DisplayTime(currentTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if(timeToDisplay > 0) // Show that the player still has time when the timer is smaller than 1 second
        {
            timeToDisplay += 1;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countDownTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);        
    }

    IEnumerator SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        yield return new WaitForSeconds(enemyTimer);
        /*GameObject newEnemy = */Instantiate(enemyPrefabs[randomEnemy], new Vector3(Random.Range(-5, 5), Random.Range(-6,6), 0), Quaternion.identity);
        StartCoroutine(SpawnEnemies());
    }

}
