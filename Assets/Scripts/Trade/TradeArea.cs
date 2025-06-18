using UnityEngine;

public class TradeArea : MonoBehaviour
{
    public int tradeId; // Trade_0, Trade_1 gibi, Inspector'da ayarlanır
    private bool isPlayerInArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            TradeUIController.Instance.OpenPanel(tradeId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            TradeUIController.Instance.CloseAllPanels();
        }
    }

    private void Update()
    {
        if (isPlayerInArea && Input.GetKeyDown(KeyCode.F))
        {
            TradeManager.Instance.PerformTrade(tradeId);
        }
    }
}
