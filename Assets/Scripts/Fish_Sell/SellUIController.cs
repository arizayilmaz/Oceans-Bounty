using TMPro;
using UnityEngine;

public class SellUIController : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI fishText;

    void Start()
    {
        // Baþlangýçta gizle
        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (goldText != null)
            goldText.text = PlayerInventory.Instance.GetItemCount("Gold").ToString();

        if (fishText != null)
            fishText.text = PlayerInventory.Instance.GetItemCount("Fish").ToString();
    }
}
