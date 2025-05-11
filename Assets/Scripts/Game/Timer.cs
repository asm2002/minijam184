using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    public float defaultTime;

    public TMP_Text timeRemainingDisplay;
    public TMP_Text pauseTimeDisplay;
    public TMP_Text endTimeDisplay;


    private float timeRemaining;
    private float timeElapsed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Starts timer
        timeRemaining = defaultTime;
        timeElapsed = 1;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeElapsed += Time.deltaTime;
            DisplayTime(timeRemaining, timeRemainingDisplay);
        }
        else {
            // End game via gameManager
            GameManager.Instance.EndGame();
        }
    }

    public void DisplayTime(float timeDisplayed, TMP_Text timeTextDisplay)
    {
        float minutes = Mathf.FloorToInt(timeDisplayed / 60);
        float seconds = Mathf.FloorToInt(timeDisplayed % 60);

        timeTextDisplay.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public void PauseGame()
    {
        DisplayTime(timeElapsed, pauseTimeDisplay);
    }

    public void AddTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
    }
    public void EndTimer()
    {
        Time.timeScale = 0;
        DisplayTime(timeElapsed, endTimeDisplay);

        Debug.Log("Time has run out");
    }
}