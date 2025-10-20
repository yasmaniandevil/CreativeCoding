using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    //how long we want our timer for
    public float timerTime = 60f;
    //the text box for our timer
    public TextMeshProUGUI timerText;
    //the text box for our score
    public TextMeshProUGUI scoreUI;
    private bool gameOver = false;
    //the variable that keeps track of our score
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        //this is going to count down for us reduce the timer by the time that has passed
        timerTime -= Time.deltaTime;

        //if timer is less than or equaL TO 0, 
        // we cap the timer at zero and we change the text to game over
        if (timerTime <= 0)
        {
            Debug.Log("Time is over");
            timerTime = 0;
            timerText.text = "Game Over";
        }
        else //if the game isnt over update our text
        {
            UpdateTimerText(timerTime);
        }
    }

    //making this function public to call in our other scripts 
    //when player collides with something we call AddScore
    public void AddScore(int amt)
    {
        //add the amount we are adding by to the score
        score += amt;
        //display the score
        scoreUI.text = "Score: " + score.ToString();
    }

    void UpdateTimerText(float secondsLeft)
    {
        int minutes = Mathf.FloorToInt(secondsLeft / 60);
        int seconds = Mathf.FloorToInt(secondsLeft % 60f);
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
    }


}
