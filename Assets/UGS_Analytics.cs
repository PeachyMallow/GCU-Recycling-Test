using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using UnityEngine.Analytics;
using Unity.VisualScripting;

public class UGS_Analytics : MonoBehaviour
{
    private RubbishInteraction RI;
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent(); //Get user consent according to various legislations
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void IncorrectPaperBinDepositEvent(string binName, string rubbishType)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType", rubbishType }
            // You can add more parameters as needed
        });
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}