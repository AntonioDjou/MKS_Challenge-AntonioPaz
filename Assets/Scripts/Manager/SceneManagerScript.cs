using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    public Slider matchSlider;
    public TextMeshProUGUI matchTimer;

    public Slider enemySpawnSlider;
    public TextMeshProUGUI enemySpawnTimer;

    public static float matchDuration;
    public static float enemySpawnDuration;
    
    void Awake()
    {
        //matchSlider = FindObjectOfType<Slider>();
        //matchTimer = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText(matchSlider.value);
        matchSlider.onValueChanged.AddListener(UpdateText);
        UpdateText(enemySpawnSlider.value);
        enemySpawnSlider.onValueChanged.AddListener(UpdateText);
    }

    void UpdateText(float val)
    {
        if(matchSlider.value == 1)
        {
            matchTimer.text = matchSlider.value.ToString() + " minute";
            matchDuration = matchSlider.value * 60;
        }
        else 
        {
            matchTimer.text = matchSlider.value.ToString() + " minutes";
            matchDuration = matchSlider.value * 60;
        }
        enemySpawnTimer.text = enemySpawnSlider.value.ToString() + " seconds";
    }


    public void GoToPlayScene()
    {
        SceneManager.LoadScene("Play");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
