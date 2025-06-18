using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IslandEffectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    [SerializeField] private GameObject fire;

    private void ToggleEffects(bool visible)
    {
        if (fog != null) fog.SetActive(visible);
        if (fire != null) fire.SetActive(visible);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrewShip"))
            ToggleEffects(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CrewShip"))
            ToggleEffects(false);
    }

}
