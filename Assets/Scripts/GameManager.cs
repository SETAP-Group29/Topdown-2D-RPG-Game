using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;

    public Ui_Manager UiManager;

    public Player player;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();
        UiManager = GetComponent<Ui_Manager>();

        player = FindObjectOfType<Player>();
    }
}
