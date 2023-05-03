using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	
	[SerializeField] private GameObject visualCue;
	[SerializeField] private TextAsset inkJSON;
	private bool _playerInRange;
	
	private void Awake() {
		visualCue.SetActive(false);
	}

	private void Update() {
		if (_playerInRange && !DialogueManager.Instance.DialogueIsPlaying) {
			visualCue.SetActive(true);
			if (Input.GetKeyDown("i")) {
				DialogueManager.Instance.EnterDialogueMode(inkJSON);
			}
		}
		else {
			visualCue.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Player")) {
			_playerInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			_playerInRange = false;
		}
	}
}
