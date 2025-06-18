using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ChestRewardHandler : MonoBehaviour
{
    public string[] rewardPool = { "Starfish", "Jellyfish", "Tooth", "IceCube", "Moss" };
    [HideInInspector] public int islandPower;
    private bool playerInside = false;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
        var rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInside = false;
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            GiveRewards();
            Destroy(gameObject);
        }
    }

    private void GiveRewards()
    {
        int count = 0;
        if (islandPower >= 200 && islandPower < 400) count = 1;
        else if (islandPower >= 400 && islandPower < 600) count = 2;
        else if (islandPower >= 600) count = 3;

        for (int i = 0; i < count; i++)
        {
            string item = rewardPool[Random.Range(0, rewardPool.Length)];
            PlayerInventory.Instance.AddItem(item, 1);
            Debug.Log($"You obtained 1 {item} from the chest.");
        }
    }
}
