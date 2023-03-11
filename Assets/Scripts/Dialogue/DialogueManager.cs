using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour {
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

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

        if (Input.GetKeyDown("space")) {
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
    }

    private void ContinueStory() {
        if (_currentStory.canContinue) {
            dialogueText.text = _currentStory.Continue();
        }
        else {
            ExitDialogueMode();
        }
    }
}
