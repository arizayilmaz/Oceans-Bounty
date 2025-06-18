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
            // Kaynaklarý düþ ve gemiyi yükselt
            deductor.DeductResources(currentIndex);
            shipController.UpgradeShip();

            // Baþarý mesajý göster
            messageUI.ShowMessage("Upgrade Successful!", true);

            // **PANELÝ KAPAT**
            uiController.HideAllPanels();

            // Artýk kullanýcý bu alandan çýkýp tekrar girerse
            // OnTriggerEnter tetiklenip yeni index ile ShowPanel() çaðrýlacak.
        }
        else
        {
            // Yetersiz kaynak mesajý
            messageUI.ShowMessage("Not enough resources!", false);
        }

        // Bu satýrý artýk kaldýrabilir veya yorum satýrý yapabilirsin:
        // uiController.UpdateResourcesInfo(currentIndex);
    }



}