using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UpgradeRequirementChecker : MonoBehaviour
{
    // ▶ Buraya sizin doldurduğunuz dictionary’yi koyun:
    private readonly Dictionary<int, (string[] Names, int[] Counts)> requirements = new()
    {
        [0] = (Names: new[] { "Gold", "Logs", "Plank", "Starfish", "IceCube", "Moss", "Jellyfish", "Tooth" },
               Counts: new[] { 500, 5, 1, 5, 5, 5, 5, 5 }),
        [1] = (Names: new[] { "Logs", "Plank", "Iron", "Starfish", "IceCube", "Moss", "Jellyfish", "Tooth" },
               Counts: new[] { 25, 5, 1, 10, 10, 10, 10, 10 }),
        [2] = (Names: new[] { "Plank", "Iron", "Copper", "Starfish", "IceCube", "Moss", "Jellyfish", "Tooth" },
               Counts: new[] { 25, 5, 1, 15, 15, 15, 15, 15 }),
        [3] = (Names: new[] { "Iron", "Copper", "Compass", "Starfish", "IceCube", "Moss", "Jellyfish", "Tooth" },
               Counts: new[] { 25, 5, 1, 20, 20, 20, 20, 20 }),
        [4] = (Names: new[] { "Copper", "Compass", "Parchment", "Starfish", "IceCube", "Moss", "Jellyfish", "Tooth" },
               Counts: new[] { 25, 5, 1, 25, 25, 25, 25, 25 })
    };

    /// <summary> Belirli bir indeksteki gereksinimleri döner. </summary>
    public (string[] resources, int[] amounts) GetResourcesForIndex(int index)
    {
        if (requirements.TryGetValue(index, out var req))
            return (req.Names, req.Counts);

        return (new string[0], new int[0]);
    }

    /// <summary> Envanterde yeterli kaynak var mı? </summary>
    public bool HasRequiredResources(int index)
    {
        var (resources, amounts) = GetResourcesForIndex(index);
        for (int i = 0; i < resources.Length; i++)
        {
            if (PlayerInventory.Instance.GetItemCount(resources[i]) < amounts[i])
                return false;
        }
        return resources.Length > 0;
    }

    /// <summary> UI için renkli metin olarak formatlanmış kaynak bilgisini döner. </summary>
    public string GetFormattedResourcesInfo(int index)
    {
        var (resources, amounts) = GetResourcesForIndex(index);
        var sb = new StringBuilder();
        for (int i = 0; i < resources.Length; i++)
        {
            int current = PlayerInventory.Instance.GetItemCount(resources[i]);
            bool enough = current >= amounts[i];

            sb.Append($"{resources[i]}: ");
            sb.Append(enough ? "<color=green>" : "<color=red>");
            sb.Append($"{current}/{amounts[i]}");
            sb.Append("</color>");

            if (i < resources.Length - 1)
                sb.Append(" | ");
        }
        return sb.ToString();
    }
}