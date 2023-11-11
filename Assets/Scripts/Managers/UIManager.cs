using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // the timer UI
    [Header("Drag the Timer UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI timerGO;

    // holds the GameManager script to access its methods
    [SerializeField]
    private GameManager gM;

    // separating the time into seconds and minutes to use for UI purposes
    [SerializeField]
    private float mins;

    [SerializeField]
    private float secs;

    private void Start()
    {
        //gM = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (timerGO != null)
        {
            DisplayTime();

        }

        else
        {
            Debug.Log("Please attach the timer UI to the UI Manager in the Inspector (in the hierarchy UI > Timer)");
        }
    }

    // converts float into minutes and seconds to display correctly
    private void DisplayTime()
    {
        mins = Mathf.FloorToInt(gM.CurrentTime() / 60);
        secs = Mathf.FloorToInt(gM.CurrentTime() % 60);
        timerGO.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}
