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

    // player's capacity UI
    [Header("Drag the Capacity UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI capacityGO;

    // holds the GameManager script to access its methods
    [Header("Drag the GameManager GameObject here")]
    [SerializeField]
    private GameManager gM;

    // separating the time into seconds and minutes to use for UI purposes
    private float mins;
    private float secs;

    private void Update()
    {
        if (timerGO != null)
        {
            DisplayTime();
        }

        else
        { Debug.Log("Please attach the timer UI to the UI Manager in the Inspector (in the hierarchy UI > Timer)"); }
    }

    // converts float into minutes and seconds to display correctly
    private void DisplayTime()
    {
        mins = Mathf.FloorToInt(gM.CurrentTime() / 60);
        secs = Mathf.FloorToInt(gM.CurrentTime() % 60);
        timerGO.text = string.Format("{0:00}:{1:00}", mins, secs);
    }

    // updates the player capacity UI when the player has picked up/disposed of rubbish
    // also displays what the player's capacity limit is on screen
    public void UpdateCapacityUI(int a, int b)
    {
        if (capacityGO != null)
        {
            capacityGO.text = a + "\n_\n\n" + b;
        }
    }
}
