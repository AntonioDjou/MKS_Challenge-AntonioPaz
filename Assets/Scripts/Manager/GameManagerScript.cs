using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 90f;
    public TextMeshProUGUI countDownTimer;
    public SceneManagerScript sceneManagerScript;
    void Start()
    {
        currentTime = SceneManagerScript.matchDuration;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        //countDownTimer.text = currentTime.ToString("0");

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
}
