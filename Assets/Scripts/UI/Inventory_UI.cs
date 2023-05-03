using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public string inventoryName;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;
    
    
    private bool dragSingle;

    private Inventory inventory;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoyByName(inventoryName);
        SetupSlots();
        Refresh();
    }

  
    


    public void Refresh()
    {
        if (slots.Count == inventory.slots.Count)
        {

            for (int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
        
    }

    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(
            inventory.slots[Ui_Manager.draggedSlot.slotID].itemName);
        if (itemToDrop != null)
        {
            if (Ui_Manager.dragSingle)
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(Ui_Manager.draggedSlot.slotID);
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop, inventory.slots[Ui_Manager.draggedSlot.slotID].count );
                inventory.Remove(Ui_Manager.draggedSlot.slotID,  inventory.slots[Ui_Manager.draggedSlot.slotID].count);
            }
            Refresh();
        }

        Ui_Manager.draggedSlot = null;
    }
    
    public void slotBeginDrag(Slot_UI slot)
    {
        Ui_Manager.draggedSlot = slot;
        Ui_Manager.draggedIcon = Instantiate(slot.itemIcon);
        Ui_Manager.draggedIcon.transform.SetParent(canvas.transform);
        Ui_Manager.draggedIcon.raycastTarget = false;
        Ui_Manager.draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        
        MoveToMousePosition(Ui_Manager.draggedIcon.gameObject);
    }

    public void slotDrag()
    {
        MoveToMousePosition(Ui_Manager.draggedIcon.gameObject);
        // Debug.Log("Dragging: "+ draggedSlot.name);
    }

    public void slotEndDrag()
    {
        Destroy(Ui_Manager.draggedIcon.gameObject);
        Ui_Manager.draggedIcon = null;
        
        // Debug.Log("Done Drag: " + draggedSlot.name);
    }

    public void slotDrop(Slot_UI slot)
    {
        if (Ui_Manager.dragSingle)
        {
            Ui_Manager.draggedSlot.inventory.moveSlot(Ui_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
        }
        else
        {
            Ui_Manager.draggedSlot.inventory.moveSlot(Ui_Manager.draggedSlot.slotID, slot.slotID, slot.inventory,
                Ui_Manager.draggedSlot.inventory.slots[Ui_Manager.draggedSlot.slotID].count);
        }
        GameManager.instance.UiManager.RefreshAll();
        
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    private void SetupSlots()
    {
        int counter = 0;
        foreach (Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}


