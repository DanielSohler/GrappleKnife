using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("Countdown Vars")]
    [SerializeField] float timeRemaining = 10;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] float startDelay;
    [SerializeField] Color[] textColours;
    [Header("Refs")]
    [SerializeField] TextMeshProUGUI timerText;
    public UnityEvent endTimerEvent;

    public void StartTimer()
    {
        timerText.enabled = true;
        Debug.Log("Ayoo");

        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        StartCoroutine(startDelayTest());
    }

    public void AddTImeToTimer(float addedTIme)
    {
        if (timerIsRunning)
        {
            timeRemaining += addedTIme;
            Debug.Log("Added " + addedTIme + "s to timer");
        }
        else
            return;
        
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                endTimerEvent.Invoke();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        //divides and multiplies float to ints to be turned into mins and secs
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public IEnumerator startDelayTest()
    {
        timerText.color = Color.grey;
        yield return new WaitForSeconds(startDelay);
        timerText.color = Color.white;
        timerIsRunning = true;
    }
}
