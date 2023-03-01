using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownTimer;
    float currentTime = 0;
    private IEnumerator countUp;

    [Space(10)]
    public Button zButton;
    public Slider zSlider;
    public Image zCooldown;

    [Space(10)]
    public Button xButton;
    public Slider xSlider;
    public Image xCooldown;

    [Space(10)]
    public Button cButton;
    public Slider cSlider;
    public Image cCooldown;

    void Start()
    {
        if(SceneManagerScript.matchDuration != 0) currentTime = SceneManagerScript.matchDuration;
        else currentTime = 50;
    }

    void Update()
    {
        xCooldown.fillAmount = PlayerControllerScript.lastShot;
        zCooldown.fillAmount = PlayerControllerScript.lastLateralShot;
        cCooldown.fillAmount = PlayerControllerScript.lastLateralShot;
        
        if(zCooldown.fillAmount >= 1) EnableZButton();
        else DisableZButton();

        if(xCooldown.fillAmount >= 1) EnableXButton();
        else DisableXButton();

        if(cCooldown.fillAmount >= 1) EnableCButton();
        else DisableCButton();

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

    public void DisableZButton()
    {
        if(zButton.interactable) //To make sure it will change the alpha only once
        {
            Image[] childrenImage = zSlider.GetComponentsInChildren<Image>(); //Get all the Children
            foreach(Image img in childrenImage) //For all of them
            {
                img.color -= new Color(0,0,0,0.9f); //Change the transparency of it.
            }
        }
        zButton.interactable = false; //Cannot be interactible while on Cooldown.        
    }

    public void EnableZButton()
    {
        Image[] childrenImage = zSlider.GetComponentsInChildren<Image>(); //Get all the Children
        foreach(Image img in childrenImage) //For all of them
        {
            img.color += new Color(0,0,0,0.9f); //Change back the transparency to normal.
        }
        zButton.interactable = true; //Gets interactible again after Cooldown.
    }

    public void DisableXButton()
    {
        if(xButton.interactable) //To make sure it will change the alpha only once
        {
            Image[] childrenImage = xSlider.GetComponentsInChildren<Image>(); //Get all the Children
            foreach(Image img in childrenImage) //For all of them
            {
                img.color -= new Color(0,0,0,0.9f); //Change the transparency of it.
            }
        }
        xButton.interactable = false; //Cannot be interactible while on Cooldown.        
    }

    public void EnableXButton()
    {
        Image[] childrenImage = xSlider.GetComponentsInChildren<Image>(); //Get all the Children
        foreach(Image img in childrenImage) //For all of them
        {
            img.color += new Color(0,0,0,0.9f); //Change back the transparency to normal.
        }
        xButton.interactable = true; //Gets interactible again after Cooldown.
    }

    public void DisableCButton()
    {
        if(cButton.interactable) //To make sure it will change the alpha only once
        {
            Image[] childrenImage = cSlider.GetComponentsInChildren<Image>(); //Get all the Children
            foreach(Image img in childrenImage) //For all of them
            {
                img.color -= new Color(0,0,0,0.9f); //Change the transparency of it.
            }
        }
        cButton.interactable = false; //Cannot be interactible while on Cooldown.        
    }

    public void EnableCButton()
    {
        Image[] childrenImage = cSlider.GetComponentsInChildren<Image>(); //Get all the Children
        foreach(Image img in childrenImage) //For all of them
        {
            img.color += new Color(0,0,0,0.9f); //Change back the transparency to normal.
        }
        cButton.interactable = true; //Gets interactible again after Cooldown.
    }
}
