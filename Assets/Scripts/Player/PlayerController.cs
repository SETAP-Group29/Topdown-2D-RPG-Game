using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private float velocity = 1f;
	private Rigidbody2D _rigidbody;
	private Vector2 _movement;
	
	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		_movement.x = Input.GetAxis("Horizontal");
		_movement.y = Input.GetAxis("Vertical");
	}

	private void FixedUpdate() {
		if (DialogueManager.Instance.DialogueIsPlaying) {
			return;
		}
		
		PlayerManager.Instance.PlayerMovement(_movement, _rigidbody, velocity);
		PlayerManager.Instance.PlayerAnimation(_movement);
	}
}