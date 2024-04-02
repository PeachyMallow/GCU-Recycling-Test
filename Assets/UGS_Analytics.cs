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
    public void Correct_Paper_Bin_Deposit(string RubbishBin, string Paper)
    {
        Analytics.CustomEvent("Correct_Paper_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin },
            { "Paper", Paper },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Paper_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(Paper);
    }

    public void Correct_Food_Bin_Deposit(string RubbishBin, string FoodWaste)
    {
        Analytics.CustomEvent("Correct_Food_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin },
            { "Paper", FoodWaste },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Food_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(FoodWaste);
    }
    public void Correct_Plastic_Bin_Deposit(string RubbishBin, string Plastic)
    {
        Analytics.CustomEvent("Correct_Food_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin },
            { "Paper", Plastic },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_Food_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }

    public void Correct_General_Waste_Bin_Deposit(string RubbishBin, string NonRecyclable)
    {
        Analytics.CustomEvent("Correct_General_Waste_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin },
            { "Paper", NonRecyclable },
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Correct_General_Waste_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
    }
    #endregion

    #region Incorrect Bin Deposits
    public void Incorrect_Paper_Bin_Deposit(string RubbishBin, string FoodWaste, string NonRecyclable, string Plastic)
    {
        Analytics.CustomEvent("Incorrect_Paper_Bin_Deposit", new Dictionary<string, object> ()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "FoodWaste", FoodWaste },
            { "NonRecyclable", NonRecyclable },
            { "Plastic", Plastic }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Paper_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(FoodWaste);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }

    public void Incorrect_Food_Bin_Deposit(string RubbishBin, string Paper, string NonRecyclable, string Plastic)
    {
        Analytics.CustomEvent("Incorrect_Food_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "Paper", Paper },
            { "NonRecyclable", NonRecyclable },
            { "Plastic", Plastic }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Food_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(Paper);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }

    public void Incorrect_General_Waste_Bin_Deposit(string RubbishBin, string FoodWaste, string paper, string Plastic)
    {
        Analytics.CustomEvent("Incorrect_General_Waste_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "FoodWaste", FoodWaste },
            { "NonRecyclable", paper },
            { "Plastic", Plastic }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_General_Waste_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(FoodWaste);
        AnalyticsService.Instance.RecordEvent(paper);
        AnalyticsService.Instance.RecordEvent(Plastic);
    }

    public void Incorrect_Plastic_Bin_Deposit(string RubbishBin, string FoodWaste, string NonRecyclable, string Paper)
    {
        Analytics.CustomEvent("Incorrect_Plastic_Bin_Deposit", new Dictionary<string, object>()
        {
            { "RubbishBin", RubbishBin }, //used to be binName
            { "FoodWaste", FoodWaste },
            { "NonRecyclable", NonRecyclable },
            { "Paper", Paper }
            // You can add more parameters as needed
        });
        AnalyticsService.Instance.RecordEvent("Incorrect_Plastic_Bin_Deposit");
        AnalyticsService.Instance.RecordEvent(RubbishBin);
        AnalyticsService.Instance.RecordEvent(FoodWaste);
        AnalyticsService.Instance.RecordEvent(NonRecyclable);
        AnalyticsService.Instance.RecordEvent(Paper);
    }
    #endregion

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}