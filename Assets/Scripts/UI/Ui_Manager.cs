using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Ui_Manager : MonoBehaviour
{

    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();

    public GameObject inventoryPanel;
    
    public List<Inventory_UI> InventoryUis;

    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
            RefreshInventoryUI("Backpack");
        }
        
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            dragSingle = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            dragSingle = false;
        }
    }
    
    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }
            
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }
    public void RefreshAll()
    {
        foreach (KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
        {
            
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }
        Debug.LogWarning("|| There is no inventory UI fro "+ inventoryName+" ||");
        return null;
    }

    void Initialize()
    {
        foreach (Inventory_UI ui in InventoryUis)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
