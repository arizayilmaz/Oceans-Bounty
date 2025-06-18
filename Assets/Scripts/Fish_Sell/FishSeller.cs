using UnityEngine;

public class FishSeller : MonoBehaviour
{
    [Header("Sat�� Ayarlar�")]
    [Tooltip("Her sat��ta ka� bal�k gidecek?")]
    public int fishPerTransaction = 10;
    [Tooltip("Her bal�k i�in verilecek alt�n miktar�.")]
    public int goldPerFish = 5;

    [Header("UI Controller")]
    public SellUIController uiController;

    /// <summary>
    /// Tek seferlik sat��: envanterde ne kadar bal�k varsa en fazla fishPerTransaction kadar�n� satar.
    /// </summary>
    public void SellOnce()
    {
        int availableFish = PlayerInventory.Instance.GetItemCount("Fish");
        int fishToSell = Mathf.Min(fishPerTransaction, availableFish);
        if (fishToSell <= 0)
            return;

        // Bal�klar� azalt
        PlayerInventory.Instance.RemoveItem("Fish", fishToSell);
        // Alt�n� ekle
        PlayerInventory.Instance.AddItem("Gold", fishToSell * goldPerFish);
        Debug.Log($"[FishSeller] Sold {fishToSell} fish for {fishToSell * goldPerFish} gold.");

        // Sell UI g�ncelle
        uiController?.UpdateUI();
    }
}