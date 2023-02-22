using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] Slider matchSlider;
    [SerializeField] TextMeshProUGUI matchTimer;

    [SerializeField] Slider enemySpawnSlider;
    [SerializeField] TextMeshProUGUI enemySpawnTimer;

    public static int matchDuration;

    
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
        }
        else 
        {
            matchTimer.text = matchSlider.value.ToString() + " minutes";
        }
        enemySpawnTimer.text = enemySpawnSlider.value.ToString() + " seconds";
    }


    public void GoToPlayScene()
    {
        SceneManager.LoadScene("Play");
    }
}