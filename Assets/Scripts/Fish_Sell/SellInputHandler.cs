using UnityEngine;

public class SellInputHandler : MonoBehaviour
{
    public FishSeller fishSeller;
    private bool canSell = false;

    public void EnableInput(bool state) => canSell = state;

    private void Update()
    {
        if (canSell && Input.GetKeyDown(KeyCode.F))
            fishSeller.SellOnce();
    }
}
