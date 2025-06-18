using System.Collections;
using UnityEngine;
using TMPro;

public class TradeUIController : MonoBehaviour
{
    public static TradeUIController Instance;

    public GameObject[] tradePanels; // Trade_0 → panels[0], vs.
    public TMP_Text resultText;
    public float messageDuration = 1.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OpenPanel(int id)
    {
        CloseAllPanels();

        if (id >= 0 && id < tradePanels.Length)
            tradePanels[id].SetActive(true);
    }

    public void CloseAllPanels()
    {
        foreach (var panel in tradePanels)
            if (panel != null) panel.SetActive(false);
    }

    public void ShowMessage(string message, bool isSuccess)
    {
        if (resultText == null) return;

        resultText.text = message;
        resultText.color = isSuccess ? Color.green : Color.red;
        resultText.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        resultText.gameObject.SetActive(false);
    }
}
