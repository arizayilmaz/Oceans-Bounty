using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private static PlayerInventory _instance;

    [Header("UI References")]
    public GameObject inventoryPanel;
    public TextMeshProUGUI[] itemTexts; // Array for all item texts

    [Header("Other Panels (won't open inventory if these are active)")]
    [SerializeField] private GameObject raidPanel;
    [SerializeField] private GameObject shopPanel;

    private bool isInventoryOpen = false;

    public static PlayerInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInventory>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(PlayerInventory).Name;
                    _instance = obj.AddComponent<PlayerInventory>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        InitializeInventory();
    }

    void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    private void Update()
    {
      
        if (!Input.GetKeyDown(KeyCode.Alpha1))
            return;

        if (isInventoryOpen)
        {
            CloseInventory();
            return;
        }

        if ((shopPanel != null && shopPanel.activeSelf) ||
            (raidPanel != null && raidPanel.activeSelf))
        {
            Debug.Log("Cannot open Inventory while Shop or Raid panel is open.");
            return;
        }

        OpenInventory();
    }

    private void OpenInventory()
    {
        isInventoryOpen = true;
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
            UpdateInventoryUI();
        }
    }

    private void CloseInventory()
    {
        isInventoryOpen = false;
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    private void UpdateInventoryUI()
    {
        if (itemTexts == null || itemTexts.Length < 14)
            return;

        itemTexts[0].text = GetItemCount("Fish").ToString();
        itemTexts[1].text = GetItemCount("Gold").ToString();
        itemTexts[2].text = GetItemCount("Moss").ToString();
        itemTexts[3].text = GetItemCount("Tooth").ToString();
        itemTexts[4].text = GetItemCount("IceCube").ToString();
        itemTexts[5].text = GetItemCount("Jellyfish").ToString();
        itemTexts[6].text = GetItemCount("Starfish").ToString();
        itemTexts[7].text = GetItemCount("Logs").ToString();
        itemTexts[8].text = GetItemCount("Plank").ToString();
        itemTexts[9].text = GetItemCount("Iron").ToString();
        itemTexts[10].text = GetItemCount("Copper").ToString();
        itemTexts[11].text = GetItemCount("Compass").ToString();
        itemTexts[12].text = GetItemCount("Parchment").ToString();
        itemTexts[13].text = GetItemCount("Meat").ToString();
    }

    public void AddItem(string itemName, int amount)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += amount;
            if (isInventoryOpen) UpdateInventoryUI();
        }
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] = Mathf.Max(0, inventory[itemName] - amount);
            if (isInventoryOpen) UpdateInventoryUI();
        }
    }

    public int GetItemCount(string itemName)
    {
        if (inventory.ContainsKey(itemName))
            return inventory[itemName];
        return 0;
    }

    public void PrintInventory()
    {
        foreach (var item in inventory)
            Debug.Log(item.Key + ": " + item.Value);
    }

    private void InitializeInventory()
    {
        inventory.Clear();
        inventory = new Dictionary<string, int>
    {
        { "Fish",      50      },
        { "Gold",      125000  },
        { "Moss",      1110    },
        { "Tooth",     1110    },
        { "IceCube",   1110    },
        { "Jellyfish", 1110    },
        { "Starfish",  1110    },
        { "Logs",      1110    },
        { "Plank",     1110    },
        { "Iron",      1110    },
        { "Copper",    1110    },
        { "Compass",   1110    },
        { "Parchment", 1110    },
        { "Meat",      1000    }
    };
    }

}