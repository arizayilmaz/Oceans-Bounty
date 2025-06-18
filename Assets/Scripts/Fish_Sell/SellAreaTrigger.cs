using UnityEngine;

public class SellAreaTrigger : MonoBehaviour
{
    [Header("References")]
    public FishSeller fishSeller;
    public SellInputHandler inputHandler;
    public SellUIController uiController;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inputHandler.EnableInput(true);

        // Panel yerine UIController'ýn GameObject'ini aç
        uiController.gameObject.SetActive(true);
        uiController.UpdateUI();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inputHandler.EnableInput(false);

        // Artýk StopSelling yoksa bu satýrý tamamen kaldýrýyoruz:
        // fishSeller.StopSelling();

        // UI'ý kapat
        uiController.gameObject.SetActive(false);
    }
}
