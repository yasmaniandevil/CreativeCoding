 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float timerTime = 60f;
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI scoreUi;
    int score;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if(scoreUi != null)
        {
            scoreUi.text = "Score: ";

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        //reduce the timer by the time that passed since the last frame
        timerTime -= Time.deltaTime;
        
        //if time reachers 0 or below clamp it and show time over
        if (timerTime <= 0f)
        {
            //Debug.Log("Time over");
            timerTime = 0f;
            timerText.text = "Game Over";
        }
        else
        {
            //while time remain displasys it
            UpdateTimerText(timerTime);
        }
    }

    //adds to score and updates the text
    public void AddScore(int amt)
    {
        score += amt;
        scoreUi.text = "Score: " + score.ToString();
    }

    //Converts seconds to a friendly "MM:SS" display (e.g., 1:05).
    void UpdateTimerText(float secondsLeft)
    {
        //timerText.text = timerTime.ToString();

        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(secondsLeft / 60f);
        int seconds = Mathf.FloorToInt(secondsLeft % 60f);
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
    }

    
}
