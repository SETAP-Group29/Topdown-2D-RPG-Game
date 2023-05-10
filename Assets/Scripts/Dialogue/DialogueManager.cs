using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Random = System.Random;

public class DialogueManager : MonoBehaviour {
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    public Transform collectables;

    public Transform target;
    private float yOffset = 0.5f;
    private int numOfCoins = 10; 
    
    private Story _currentStory;
    public bool DialogueIsPlaying { get; private set; } 
    public static DialogueManager Instance { get; private set; }
    
    

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        Instance = this;
    }

    private void Update() {
        if (!DialogueIsPlaying) {
            return;
        }

        if (Input.GetKeyDown("s")) {
            ContinueStory();
        }
    }

    private void Start() {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    public void EnterDialogueMode(TextAsset inkJson) {
        _currentStory = new Story(inkJson.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogueMode() {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        GetCoins();
    }

    private void ContinueStory() {
        if (_currentStory.canContinue) {
            dialogueText.text = _currentStory.Continue();
        }
        else {
            ExitDialogueMode();
        }
    }
    
    public void GetCoins()
    {
        if (PlayerManager.Instance.isQuestCompleted)
        {
            Random rnd = new Random();

            for (int i = 0; i < numOfCoins; i++)
            {
                float x = Convert.ToSingle(rnd.NextDouble() - 0.5);
                float y = Convert.ToSingle(rnd.NextDouble() - 0.5);
                Instantiate(collectables, new Vector2(target.transform.position.x - x, target.transform.position.y - yOffset - y), Quaternion.identity);
            }
            
        }
    }
}
