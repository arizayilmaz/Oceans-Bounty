using UnityEngine;

public class ShipUpgradeManager : MonoBehaviour
{
    public ShipController shipController;
    public UpgradeUIController uiController;
    public UpgradeMessageUI messageUI;
    public UpgradeRequirementChecker checker;
    public UpgradeCostDeductor deductor;

    private bool isInUpgradeArea = false;

    private void Update()
    {
        if (isInUpgradeArea && Input.GetKeyDown(KeyCode.F))
        {
            TryUpgrade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInUpgradeArea = true;
            uiController.ShowPanel(shipController.CurrentShipIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInUpgradeArea = false;
            uiController.HideAllPanels();
        }
    }

    private void TryUpgrade()
    {
        int currentIndex = shipController.CurrentShipIndex;

        if (checker.HasRequiredResources(currentIndex))
        {
            // Kaynaklar� d�� ve gemiyi y�kselt
            deductor.DeductResources(currentIndex);
            shipController.UpgradeShip();

            // Ba�ar� mesaj� g�ster
            messageUI.ShowMessage("Upgrade Successful!", true);

            // **PANEL� KAPAT**
            uiController.HideAllPanels();

            // Art�k kullan�c� bu alandan ��k�p tekrar girerse
            // OnTriggerEnter tetiklenip yeni index ile ShowPanel() �a�r�lacak.
        }
        else
        {
            // Yetersiz kaynak mesaj�
            messageUI.ShowMessage("Not enough resources!", false);
        }

        // Bu sat�r� art�k kald�rabilir veya yorum sat�r� yapabilirsin:
        // uiController.UpdateResourcesInfo(currentIndex);
    }



}