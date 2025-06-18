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

        // Panel yerine UIController'�n GameObject'ini a�
        uiController.gameObject.SetActive(true);
        uiController.UpdateUI();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inputHandler.EnableInput(false);

        // Art�k StopSelling yoksa bu sat�r� tamamen kald�r�yoruz:
        // fishSeller.StopSelling();

        // UI'� kapat
        uiController.gameObject.SetActive(false);
    }
}
