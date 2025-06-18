using UnityEngine;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject collectionZonePrefab;
    [SerializeField] private int maxActiveZones = 3;
    [SerializeField] private float spawnCheckInterval = 30f;

    [Header("Spawn Boundaries")]
    [SerializeField] private float xMin = -1000f;
    [SerializeField] private float xMax = 1000f;
    [SerializeField] private float zMin = -1000f;
    [SerializeField] private float zMax = 1000f;

    private List<SingleMaterialCollectionZone> activeZones = new List<SingleMaterialCollectionZone>();

    private void Start()
    {
        // Baþlangýçta zonlarý spawn et
        for (int i = 0; i < maxActiveZones; i++)
        {
            SpawnNewZone();
        }

        InvokeRepeating(nameof(CheckZones), spawnCheckInterval, spawnCheckInterval);
    }

    private void CheckZones()
    {
        // Eksik zon varsa yenilerini ekle
        while (activeZones.Count < maxActiveZones)
        {
            SpawnNewZone();
        }
    }

    private void SpawnNewZone()
    {
        Vector3 spawnPos = GetRandomPositionWithinBounds();
        GameObject newZone = Instantiate(collectionZonePrefab, spawnPos, Quaternion.identity);
        SingleMaterialCollectionZone zoneScript = newZone.GetComponent<SingleMaterialCollectionZone>();
        activeZones.Add(zoneScript);
    }

    public void ZoneCollected(SingleMaterialCollectionZone zone)
    {
        activeZones.Remove(zone);
        Destroy(zone.gameObject);
        CheckZones(); // Hemen yeni zone spawn et
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        return new Vector3(
            Random.Range(xMin, xMax),
            0f,
            Random.Range(zMin, zMax)
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((xMin + xMax) / 2, 0, (zMin + zMax) / 2);
        Vector3 size = new Vector3(xMax - xMin, 0.1f, zMax - zMin);
        Gizmos.DrawWireCube(center, size);
    }
}