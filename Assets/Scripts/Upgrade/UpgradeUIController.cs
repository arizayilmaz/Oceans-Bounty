using TMPro;
using UnityEngine;

public class UpgradeUIController : MonoBehaviour
{
    public GameObject[] upgradePanels;
    public TMP_Text resourcesInfoText;
    public UpgradeRequirementChecker checker;

    public void ShowPanel(int index)
    {
        // 1) Önemli referanslar null mı?
        if (upgradePanels == null || resourcesInfoText == null || checker == null)
        {
            Debug.LogError("[UpgradeUIController] ShowPanel: " +
                (upgradePanels == null ? "upgradePanels null; " : "") +
                (resourcesInfoText == null ? "resourcesInfoText null; " : "") +
                (checker == null ? "checker null; " : "")
            );
            return;
        }

        // 2) Önce tüm panelleri gizle
        HideAllPanels();

        // 3) İndex geçerliyse aç
        if (index >= 0 && index < upgradePanels.Length && upgradePanels[index] != null)
        {
            upgradePanels[index].SetActive(true);
            UpdateResourcesInfo(index);
            resourcesInfoText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[UpgradeUIController] ShowPanel: Index {index} out of range or panel null");
        }
    }

    public void HideAllPanels()
    {
        // 1) Eğer dizi atanmadıysa çık
        if (upgradePanels == null || upgradePanels.Length == 0)
            return;

        // 2) Tüm panelleri gizle (element null mu kontrol et)
        foreach (var panel in upgradePanels)
            if (panel != null)
                panel.SetActive(false);

        // 3) resourcesInfoText varsa kapat
        if (resourcesInfoText != null)
            resourcesInfoText.gameObject.SetActive(false);
    }

    public void UpdateResourcesInfo(int index)
    {
        // Bu metodu da korumak istersen benzer null‐check ekleyebilirsin:
        if (checker == null || resourcesInfoText == null)
        {
            Debug.LogError("[UpgradeUIController] UpdateResourcesInfo: checker veya resourcesInfoText null");
            return;
        }

        resourcesInfoText.text = checker.GetFormattedResourcesInfo(index);
    }
}
