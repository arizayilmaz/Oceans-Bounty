using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Fishing : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI fishCounterText;
    [SerializeField] private Image fishIcon;

    [Header("Fishing Settings")]
    [SerializeField] private float normalCollectionRate = 0.5f; // 2 saniyede 1 balýk (1/2 = 0.5)
    [SerializeField] private float inactivityThreshold = 15f;

    private float currentCollectionRate;
    private float inactivityTimer;
    private bool isCollectingFish = false;
    private Coroutine fishingCoroutine;
    private string currentItemToCollect = "Fish";

    private void Start()
    {
        currentCollectionRate = normalCollectionRate;
        UpdateFishCounter();
    }

    public void StartFishing()
    {
        if (!isCollectingFish)
        {
            isCollectingFish = true;
            inactivityTimer = 0f; // Reset inactivity timer when starting fishing
            fishingCoroutine = StartCoroutine(CollectFishCoroutine());
        }
    }

    public void StopFishing()
    {
        if (isCollectingFish)
        {
            isCollectingFish = false;
            if (fishingCoroutine != null)
            {
                StopCoroutine(fishingCoroutine);
                fishingCoroutine = null;
            }
        }
    }

    public void SetFishingSpeed(bool boosted)
    {
        currentCollectionRate = boosted ? 4f : 0.5f;

        if (isCollectingFish)
        {
            StopFishing();
            StartFishing();
        }
    }

    IEnumerator CollectFishCoroutine()
    {
        while (isCollectingFish)
        {
            float waitTime = 1f / currentCollectionRate;
            yield return new WaitForSeconds(waitTime);
            PlayerInventory.Instance.AddItem(currentItemToCollect, 1);
            UpdateFishCounter();
        }
    }

    public void CheckInactivity(bool isMoving)
    {
        if (isMoving)
        {
            inactivityTimer = 0f;
            if (!isCollectingFish)
            {
                StartFishing();
            }
        }
        else
        {
            inactivityTimer += Time.deltaTime;
            if (inactivityTimer >= inactivityThreshold && isCollectingFish)
            {
                StopFishing();
            }
        }
    }

    private void UpdateFishCounter()
    {
        if (fishCounterText != null && fishIcon != null)
        {
            fishCounterText.text = PlayerInventory.Instance.GetItemCount(currentItemToCollect).ToString();
        }
    }
}