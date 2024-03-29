using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using UnityEngine.Analytics;
using Unity.VisualScripting;
using System.Security.Cryptography;

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
    public void CorrectPaperBinDepositEvent(string RubbishBin, string Paper)
    {
        Analytics.CustomEvent("CorrectPaperBinDepositEvent", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin },
            { "Paper", Paper },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("CorrectPaperBinDepositEvent");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(Paper);
    }
    #endregion

    #region Incorrect Bin Deposits
    public void IncorrectPaperBinDepositEvent(string RubbishBin, string FoodWaste, string NonRecyclable, string Plastic)
    {
        Analytics.CustomEvent("IncorrectPaperBinDepositEvent", new Dictionary<string, object> ()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "FoodWaste", FoodWaste },
            { "NonRecyclable", NonRecyclable },
            { "Plastic", Plastic }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("IncorrectPaperBinDepositEvent");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(FoodWaste);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }

    public void IncorrectFoodBinDepositEvent(string RubbishBin, string Paper, string NonRecyclable, string Plastic)
    {
        Analytics.CustomEvent("IncorrectPaperBinDepositEvent", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "Paper", Paper },
            { "NonRecyclable", NonRecyclable },
            { "Plastic", Plastic }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("IncorrectPaperBinDepositEvent");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(Paper);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }
    #endregion

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}