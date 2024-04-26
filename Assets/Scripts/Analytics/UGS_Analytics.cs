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
    private Inventory I;
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
    public void Correct_Paper_Bin_Deposit( string recyclingType)
    {
        Analytics.CustomEvent("Correct_Paper_Bin_Deposit", new Dictionary<string, object>()
        {
            { "Paper", recyclingType },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Paper_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }

    public void Correct_Food_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Correct_Food_Bin_Deposit", new Dictionary<string, object>()
        {
            { "FoodWaste", recyclingType },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Food_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }
    public void Correct_Plastic_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Correct_Plastic_Bin_Deposit", new Dictionary<string, object>()
        {
            { "Plastic", recyclingType},
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Plastic_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }

    public void Correct_General_Waste_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Correct_General_Waste_Bin_Deposit", new Dictionary<string, object>()
        {
            { "NonRecyclable", recyclingType },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_General_Waste_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }
    #endregion

    #region Incorrect Bin Deposits
    public void Incorrect_Paper_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Incorrect_Paper_Bin_Deposit", new Dictionary<string, object> ()
        {
            { "FoodWaste", recyclingType },
            { "NonRecyclable", recyclingType },
            { "Plastic", recyclingType }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Paper_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }

    public void Incorrect_Food_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Incorrect_Food_Bin_Deposit", new Dictionary<string, object>()
        {
            { "Paper", recyclingType },
            { "NonRecyclable", recyclingType },
            { "Plastic", recyclingType }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Food_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }

    public void Incorrect_General_Waste_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Incorrect_General_Waste_Bin_Deposit", new Dictionary<string, object>()
        {
            { "FoodWaste", recyclingType },
            { "NonRecyclable", recyclingType },
            { "Plastic", recyclingType }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_General_Waste_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);

    }

    public void Incorrect_Plastic_Bin_Deposit(string recyclingType)
    {
        Analytics.CustomEvent("Incorrect_Plastic_Bin_Deposit", new Dictionary<string, object>()
        {
            { "FoodWaste", recyclingType },
            { "NonRecyclable", recyclingType },
            { "Paper", recyclingType }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Plastic_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(recyclingType);
    }
    #endregion

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}