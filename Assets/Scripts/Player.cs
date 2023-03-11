using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class Player : MonoBehaviour
{
    public InventoryMangaer inventory;


    private void Awake()
    {
        inventory = GetComponent<InventoryMangaer>();
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLacation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 0.4f;

        Instantiate(item, spawnLacation + spawnOffset, Quaternion.identity);
    }
    
    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}
