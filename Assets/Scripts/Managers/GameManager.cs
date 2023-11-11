using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // timer set in inspector
    [Header("Enter time in seconds")]
    [SerializeField]
    private float timer;

    // variable for the script to access the time set in the inspector later on
    [SerializeField]
    private float totalTime;

    // current time that is passed to timer UI
    [SerializeField]
    private float currentTime;

    // is the timer active
    [SerializeField]
    private bool timerActive;


    private void Start()
    {
        totalTime = timer;
        timerActive = true;
    }

    private void Update()
    {
        if (timerActive)
        {
            if (timer > 0)
            {
                Timer();
            }

            else
            {
                timer = 0;
                Debug.Log("Time's Up!");
                timerActive = false;            
            }
        }
    }

    private float Timer()
    {
        return timer -= Time.deltaTime;
    }

    // public method for other scripts to access the current time 
    public float CurrentTime()
    {
        return timer;
    }

    // will start the timer from the time set in the inspector 
    public void StartTimer()
    {

    }

    // will reset the timer to the time set in the inspector 
    public void ResetTimer()
    {
        timer = totalTime;
    }


    public bool IsTimeUp()
    {
        return timer <= 0;
    }
}
