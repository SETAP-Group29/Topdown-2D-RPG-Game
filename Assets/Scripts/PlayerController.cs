using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float velocity = 1f;

	[SerializeField] private float collisionOffset = 0.05f;
	private ContactFilter2D _contactFilter;
	private Vector2 _movement;
	private readonly List<RaycastHit2D> _results = new();
	private Rigidbody2D _rigidbody;
	[SerializeField] private Animator _animator;
	
	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		_movement.x = Input.GetAxis("Horizontal");
		_movement.y = Input.GetAxis("Vertical");

		// _animator.SetFloat("Horizontal", _movement.x);
		// _animator.SetFloat("Vertical", _movement.y);
		// _animator.SetFloat("Speed", _movement.sqrMagnitude);
	}

	private void FixedUpdate() {
		if (DialogueManager.Instance.DialogueIsPlaying) {
			return;
		}
		PlayerMovement(_movement);
		_animator.SetFloat("Horizontal", _movement.x);
		_animator.SetFloat("Vertical", _movement.y);
		_animator.SetFloat("Speed", _movement.sqrMagnitude);
	}

	private void PlayerMovement(Vector2 movement) {
		var xy = movement;
		var x = new Vector2(movement.x, 0);
		var y = new Vector2(0, movement.y);
		if (FlagCollisionWithPlayer(xy) == 1) {
			PlayerMovePos(xy);
		}
		else {
			if (FlagCollisionWithPlayer(x) == 1) {
				PlayerMovePos(x);
			}
			else {
				if (FlagCollisionWithPlayer(y) == 1) PlayerMovePos(y);
			}
		}
	}

	private void PlayerMovePos(Vector2 direction) {
		_rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * velocity * direction);
	}

	private int FlagCollisionWithPlayer(Vector2 movement) {
		return _rigidbody.Cast(movement, _contactFilter, _results, collisionOffset + Time.fixedDeltaTime * velocity) ==
		       0
			? 1
			: 0;
	}
}