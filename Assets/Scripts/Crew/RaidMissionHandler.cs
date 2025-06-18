using UnityEngine;
using System.Collections;
using TMPro;

public class RaidMissionHandler : MonoBehaviour
{
    [Header("Spawn & Return Point")]
    public GameObject shipPrefab;
    public Transform shipSpawnPoint;

    [Header("UI Panels (Countdown + Result)")]
    public GameObject[] eventPanels;
    private int panelIndex = 0;

    [Header("Chest Prefab")]
    public GameObject chestPrefab;

    [Header("Settings")]
    public float approachDistance = 20f;
    public float travelSpeed = 10f;
    public float battleDuration = 60f;
    public float postBattleWait = 2f;

    private bool isRaidActive = false;
    public bool IsRaidActive => isRaidActive;

    public void StartRaid(Transform islandTransform, int islandPower, int totalSentPower)
    {
        if (isRaidActive) return;
        isRaidActive = true;

        Vector3 startPos = shipSpawnPoint.position;
        Vector3 islandPos = islandTransform.position;
        Vector3 returnPos = startPos;

        GameObject ship = Instantiate(shipPrefab, startPos, Quaternion.identity);
        CrewShipAI ai = ship.AddComponent<CrewShipAI>();
        ai.Init(travelSpeed);

        StartCoroutine(RunRaidRoutine(ship, ai, islandPos, returnPos, islandPower, totalSentPower));
    }

    private IEnumerator RunRaidRoutine(GameObject ship, CrewShipAI ai, Vector3 islandPos, Vector3 returnPos, int targetPower, int totalSentPower)
    {
        yield return StartCoroutine(ai.MoveTo(islandPos, approachDistance));

        foreach (var p in eventPanels)
            p.SetActive(false);

        GameObject panel = eventPanels[panelIndex];
        panelIndex = (panelIndex + 1) % eventPanels.Length;
        panel.SetActive(true);

        TMP_Text countdown = panel.transform.Find("CountdownText")?.GetComponent<TMP_Text>();
        TMP_Text rewardText = panel.transform.Find("RewardText")?.GetComponent<TMP_Text>();
        TMP_Text readyText = panel.transform.Find("ReadyText")?.GetComponent<TMP_Text>();

        if (rewardText != null) rewardText.text = string.Empty;
        if (readyText != null) readyText.text = string.Empty;

        float timer = battleDuration;
        while (timer > 0f)
        {
            if (countdown != null)
                countdown.text = $"Time: {Mathf.CeilToInt(timer)}";
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }
        if (countdown != null)
            countdown.text = "Time: 0";

        bool success = totalSentPower >= targetPower;
        if (rewardText != null)
        {
            rewardText.text = success ? "SUCCESS" : "FAILED";
            rewardText.color = success ? Color.green : Color.red;
        }
        if (readyText != null)
            readyText.color = success ? Color.green : Color.red;

        yield return new WaitForSeconds(postBattleWait);
        panel.SetActive(false);

        yield return StartCoroutine(ai.MoveTo(returnPos));

        if (success && chestPrefab != null)
        {
            GameObject chest = Instantiate(chestPrefab, returnPos, Quaternion.identity);
            var handler = chest.GetComponent<ChestRewardHandler>();
            if (handler != null)
                handler.islandPower = targetPower;
        }

        Destroy(ship);
        isRaidActive = false;
    }
}
