using UnityEngine;
using TMPro;

public class BattleCrewHandler : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text villagerText;
    public TMP_Text archerText;
    public TMP_Text swordsmanText;
    public TMP_Text warningText;

    [Header("Battle Panel")]
    public GameObject battlePanel;

    [Header("Crew Values & Costs")]
    public int villagerPower = 10, villagerCost = 1;
    public int archerPower = 20, archerCost = 2;
    public int swordsmanPower = 25, swordsmanCost = 3;

    [Header("Crew Limits")]
    public int maxVillagers = 20;
    public int maxArchers = 20;
    public int maxSwordsmen = 15;


    [Header("Raid Integration")]
    public RaidMissionHandler raidHandler;
    private Transform islandTransform;
    private int islandPower;

    private int sentV, sentA, sentS;
    public bool HasBattleStarted { get; private set; }

    public void Setup(int power, Transform island)
    {
        islandPower = power;
        islandTransform = island;
        sentV = sentA = sentS = 0;
        HasBattleStarted = false;
        UpdateUI();
    }

    private void Update()
    {
        if (battlePanel == null || !battlePanel.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Alpha1)) SendCrew(CrewType.Villager);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SendCrew(CrewType.Archer);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SendCrew(CrewType.Swordsman);
        if (Input.GetKeyDown(KeyCode.F)) TryStartRaid();
        if (Input.GetKeyDown(KeyCode.Escape)) CancelBattle();
    }

    private void SendCrew(CrewType type)
    {
        // Hangi değişken ve limiti kullanacağımızı seçelim
        int currentCount, maxCount, cost;
        switch (type)
        {
            case CrewType.Villager:
                currentCount = sentV;
                maxCount = maxVillagers;
                cost = villagerCost;
                break;
            case CrewType.Archer:
                currentCount = sentA;
                maxCount = maxArchers;
                cost = archerCost;
                break;
            case CrewType.Swordsman:
                currentCount = sentS;
                maxCount = maxSwordsmen;
                cost = swordsmanCost;
                break;
            default:
                return;
        }

        // 1) Limit kontrolü
        if (currentCount >= maxCount)
        {
            ShowWarning($"You can send at most {maxCount} {type}s!");
            return;
        }

        // 2) Meat stoğu kontrolü
        if (PlayerInventory.Instance.GetItemCount("Meat") < cost)
        {
            ShowWarning($"Not enough Meat! Required: {cost}");
            return;
        }

        // 3) Kaynakları düş ve sayaçları arttır
        PlayerInventory.Instance.RemoveItem("Meat", cost);
        if (type == CrewType.Villager) sentV++;
        if (type == CrewType.Archer) sentA++;
        if (type == CrewType.Swordsman) sentS++;

        UpdateUI();
        ShowWarning("");
    }


    private void TryStartRaid()
    {
        if (raidHandler.IsRaidActive)
        {
            ShowWarning("A raid is already in progress!");
            return;
        }
        if (sentV + sentA + sentS == 0)
        {
            ShowWarning("You must send at least one troop!");
            return;
        }

        int totalPower = GetTotalPower();
        HasBattleStarted = true;
        battlePanel.SetActive(false);
        raidHandler.StartRaid(islandTransform, islandPower, totalPower);
    }

    public void CancelBattle()
    {
        int spent = sentV * villagerCost
                  + sentA * archerCost
                  + sentS * swordsmanCost;
        if (!HasBattleStarted && spent > 0)
            PlayerInventory.Instance.AddItem("Meat", spent);
        sentV = sentA = sentS = 0;
        UpdateUI();
        ShowWarning("");
    }

    private void UpdateUI()
    {
        villagerText.text = sentV.ToString();
        archerText.text = sentA.ToString();
        swordsmanText.text = sentS.ToString();
    }

    private void ShowWarning(string msg)
    {
        warningText.text = msg;
        warningText.color = Color.red;
    }

    public int GetTotalPower()
    {
        return sentV * villagerPower
             + sentA * archerPower
             + sentS * swordsmanPower;
    }
}
