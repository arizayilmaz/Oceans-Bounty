using UnityEngine;

public class FishSeller : MonoBehaviour
{
    [Header("Satýþ Ayarlarý")]
    [Tooltip("Her satýþta kaç balýk gidecek?")]
    public int fishPerTransaction = 10;
    [Tooltip("Her balýk için verilecek altýn miktarý.")]
    public int goldPerFish = 5;

    [Header("UI Controller")]
    public SellUIController uiController;

    /// <summary>
    /// Tek seferlik satýþ: envanterde ne kadar balýk varsa en fazla fishPerTransaction kadarýný satar.
    /// </summary>
    public void SellOnce()
    {
        int availableFish = PlayerInventory.Instance.GetItemCount("Fish");
        int fishToSell = Mathf.Min(fishPerTransaction, availableFish);
        if (fishToSell <= 0)
            return;

        // Balýklarý azalt
        PlayerInventory.Instance.RemoveItem("Fish", fishToSell);
        // Altýný ekle
        PlayerInventory.Instance.AddItem("Gold", fishToSell * goldPerFish);
        Debug.Log($"[FishSeller] Sold {fishToSell} fish for {fishToSell * goldPerFish} gold.");

        // Sell UI güncelle
        uiController?.UpdateUI();
    }
}