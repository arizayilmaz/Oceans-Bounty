using UnityEngine;
using TMPro;

public class MeatTradeSystem : MonoBehaviour
{
    [Header("1) Market Panel Referansı")]
    public GameObject shopPanel;
    [Header("2) Panel Üzerindeki Sayı Metinleri")]
    public TMP_Text goldText;
    public TMP_Text meatText;
    [Header("3) Ayarlar")]
    public int goldPerMeat = 50;

    private bool isPlayerInZone = false;

    private void Update()
    {
       
        if (!isPlayerInZone || shopPanel == null || !shopPanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            PerformTrade(1);
    }

    private void PerformTrade(int amount)
    {
        // 1. Önce mevcut değerleri al
        int currentGold = PlayerInventory.Instance.GetItemCount("Gold");
        int currentMeat = PlayerInventory.Instance.GetItemCount("Meat");
        int cost = amount * goldPerMeat;

        // 2. Yeterli Gold var mı?
        if (currentGold < cost)
        {
            Debug.LogWarning("[Market] Yeterli Gold yok!");
            return;
        }

        // 3. Gold azalt, Meat ekle
        PlayerInventory.Instance.RemoveItem("Gold", cost);
        PlayerInventory.Instance.AddItem("Meat", amount);

        // 4. Panelde gördüğünüz metinleri anında güncelle
        if (goldText != null)
            goldText.text = PlayerInventory.Instance.GetItemCount("Gold").ToString();
        if (meatText != null)
            meatText.text = PlayerInventory.Instance.GetItemCount("Meat").ToString();

        Debug.Log($"[Market] Trade tamam: -{cost} Gold, +{amount} Meat");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            shopPanel.SetActive(true);
            UpdateUIText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            shopPanel.SetActive(false);
        }
    }

    private void UpdateUIText()
    {
        if (goldText != null)
            goldText.text = PlayerInventory.Instance.GetItemCount("Gold").ToString();
        if (meatText != null)
            meatText.text = PlayerInventory.Instance.GetItemCount("Meat").ToString();
    }
}
