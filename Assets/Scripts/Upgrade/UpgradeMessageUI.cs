using UnityEngine;
using TMPro;

public class UpgradeMessageUI : MonoBehaviour
{
    public GameObject messagePrefab;
    public Transform canvasTransform;
    public float messageDisplayTime = 3f;

    private GameObject currentMessage;

    public void ShowMessage(string message, bool success)
    {
        // Önceki mesaj varsa yok et
        if (currentMessage != null)
        {
            Destroy(currentMessage);
        }

        // Yeni mesaj oluþtur
        currentMessage = Instantiate(messagePrefab, canvasTransform);
        TMP_Text text = currentMessage.GetComponent<TMP_Text>();
        text.text = message;
        text.color = success ? Color.green : Color.red;
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = 24;

        // Belirli süre sonra yok et ve referansý sýfýrla
        StartCoroutine(HideAfterDelay());
    }

    private System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        if (currentMessage != null)
        {
            Destroy(currentMessage);
            currentMessage = null;
        }
    }
}