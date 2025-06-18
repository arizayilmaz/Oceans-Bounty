using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject[] ships;
    public int CurrentShipIndex { get; private set; } = 0;

    public void UpgradeShip()
    {
        ships[CurrentShipIndex].SetActive(false);
        CurrentShipIndex = Mathf.Min(CurrentShipIndex + 1, ships.Length - 1);
        ships[CurrentShipIndex].SetActive(true);
    }
}