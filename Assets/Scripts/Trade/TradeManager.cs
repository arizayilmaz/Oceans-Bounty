using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PerformTrade(int tradeId)
    {
        var tradeData = GetTradeData(tradeId);
        if (tradeData == null)
        {
            Debug.LogWarning($"No trade data found for ID {tradeId}");
            return;
        }

        foreach (var item in tradeData.RequiredItems)
        {
            if (PlayerInventory.Instance.GetItemCount(item.Key) < item.Value)
            {
                TradeUIController.Instance.ShowMessage($"Not enough {item.Key} (Need {item.Value})", false);
                return;
            }
        }

        foreach (var item in tradeData.RequiredItems)
        {
            PlayerInventory.Instance.RemoveItem(item.Key, item.Value);
        }

        foreach (var item in tradeData.RewardItems)
        {
            PlayerInventory.Instance.AddItem(item.Key, item.Value);
        }

        TradeUIController.Instance.ShowMessage($"Trade Successful: {tradeData.Message}", true);
    }

    private TradeData GetTradeData(int id)
    {
        switch (id)
        {
            case 0: return new TradeData(new() { { "Fish", 5 } }, new() { { "Logs", 1 } }, "5 Fish → 1 Logs");
            case 1: return new TradeData(new() { { "Logs", 5 } }, new() { { "Plank", 1 } }, "2 Logs → 1 Plank");
            case 2: return new TradeData(new() { { "Plank", 5 } }, new() { { "Iron", 1 } }, "2 Plank → 1 Iron");
            case 3: return new TradeData(new() { { "Iron", 2 } }, new() { { "Copper", 1 } }, "2 Iron → 1 Copper");
            case 4: return new TradeData(new() { { "Copper", 2 } }, new() { { "Compass", 1 } }, "2 Copper → 1 Compass");
            case 5: return new TradeData(new() { { "Compass", 2 } }, new() { { "Parchment", 1 } }, "2 Compass → 1 Parchment");
            default: return null;
        }
    }
}

public class TradeData
{
    public Dictionary<string, int> RequiredItems;
    public Dictionary<string, int> RewardItems;
    public string Message;

    public TradeData(Dictionary<string, int> required, Dictionary<string, int> reward, string message)
    {
        RequiredItems = required;
        RewardItems = reward;
        Message = message;
    }
}
