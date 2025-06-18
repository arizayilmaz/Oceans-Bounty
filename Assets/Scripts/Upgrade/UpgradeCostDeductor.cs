using UnityEngine;

public class UpgradeCostDeductor : MonoBehaviour
{
    public UpgradeRequirementChecker checker;

    public void DeductResources(int index)
    {
        var (resources, amounts) = checker.GetResourcesForIndex(index);
        for (int i = 0; i < resources.Length; i++)
        {
            PlayerInventory.Instance.RemoveItem(resources[i], amounts[i]);
        }
    }
}