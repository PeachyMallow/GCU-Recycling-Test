using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : MonoBehaviour
{
    private UGS_Analytics analytics;
    public string CitemName;
    public string IitemName;
    public string IitemName1;
    public string IitemName2;

    private void Start()
    {
        analytics = FindObjectOfType<UGS_Analytics>();

        // Ensure the analytics component is found in the scene
        if (analytics == null)
        {
            Debug.LogError("UGS_Analytics component not found in the scene.");
        }
    }

    public void DepositedCorrectly(string binName)
    {
        analytics.CorrectPaperBinDepositEvent(binName, CitemName);
    }

    public void DepositedIncorrectly(string binName)
    {
        // Assuming you want to pass all three incorrect item names
        analytics.IncorrectPaperBinDepositEvent(binName, IitemName, IitemName1, IitemName2);
    }
}
