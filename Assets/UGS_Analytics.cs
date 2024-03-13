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

    #region Correct Bin Deposits
    public void CorrectPaperBinDepositEvent(string binName, string rubbishType)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType },
            // You can add more parameters as needed
        });
    }

    public void CorrectFoodBinDepositEvent(string binName, string rubbishType)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType },
            // You can add more parameters as needed
        });
    }

    public void CorrectPlasticBinDepositEvent(string binName, string rubbishType)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType },
            // You can add more parameters as needed
        });
    }

    public void CorrectNonBinDepositEvent(string binName, string rubbishType)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType },
            // You can add more parameters as needed
        });
    }
    #endregion

    #region Incorrect Bin Deposits
    public void IncorrectPaperBinDepositEvent(string binName, string rubbishType1, string rubbishType2, string rubbishType3)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType1 },
            { "RubbishType2", rubbishType2 },
            { "RubbishType3", rubbishType3 }
            // You can add more parameters as needed
        });
    }

    public void IncorrectFoodBinDepositEvent(string binName, string rubbishType1, string rubbishType2, string rubbishType3)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType1 },
            { "RubbishType2", rubbishType2 },
            { "RubbishType3", rubbishType3 }
            // You can add more parameters as needed
        });
    }

    public void IncorrectPlasticBinDepositEvent(string binName, string rubbishType1, string rubbishType2, string rubbishType3)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType1 },
            { "RubbishType2", rubbishType2 },
            { "RubbishType3", rubbishType3 }
            // You can add more parameters as needed
        });
    }
    public void IncorrectNonBinDepositEvent(string binName, string rubbishType1, string rubbishType2, string rubbishType3)
    {
        Analytics.CustomEvent("IncorrectBinDeposit", new Dictionary<string, object>
        {
            { "BinName", binName },
            { "RubbishType1", rubbishType1 },
            { "RubbishType2", rubbishType2 },
            { "RubbishType3", rubbishType3 }
            // You can add more parameters as needed
        });
    }
    #endregion
    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}