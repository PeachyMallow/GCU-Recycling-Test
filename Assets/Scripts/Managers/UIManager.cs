using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // player's capacity UI
    [Header("Drag the Capacity UI GameObject here")]
    [SerializeField]
    private TextMeshProUGUI capacityGO;


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
