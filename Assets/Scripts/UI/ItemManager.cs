using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] Items;

    private Dictionary<string, Item> collectableItemDict =
        new Dictionary<string, Item>();

    private void Awake()
    {
        foreach (Item item in Items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        if (!collectableItemDict.ContainsKey(item.data.itemName))
        {
            collectableItemDict.Add(item.data.itemName, item);
        }
    }

    public Item GetItemByName(string key)
    {
        if(collectableItemDict.ContainsKey(key))
        {
            return collectableItemDict[key];
        }
        return null;
    }
}
