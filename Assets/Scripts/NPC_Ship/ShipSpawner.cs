using UnityEngine;

public class RandomShipSpawnPoint : MonoBehaviour
{
    [Header("Ship Prefabs (10 different ships)")]
    [Tooltip("Her spawn noktasý için rastgele seçilecek gemi prefab'larý")]
    [SerializeField] private GameObject[] shipPrefabs;

    [Header("Spawn Settings")]
    [Tooltip("Oyun baþladýktan hemen sonra spawn etsin mi?")]
    [SerializeField] private bool spawnOnStart = true;

    private void Start()
    {
        if (spawnOnStart)
            SpawnShip();
    }

    public void SpawnShip()
    {
        if (shipPrefabs == null || shipPrefabs.Length == 0)
        {
            return;
        }

        int idx = Random.Range(0, shipPrefabs.Length);
        GameObject prefab = shipPrefabs[idx];
        if (prefab == null)
        {
            return;
        }

        Instantiate(prefab, transform.position, transform.rotation);
    }
}
