using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] private float velocity = 1f;
	private Rigidbody2D _rigidbody;
	private Vector2 _movement;
	public Animator animator;
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public float attackRange = 0.5f;
	private bool isFlipped;
	public int attackDamage = 40;
	
	
	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		_movement.x = Input.GetAxis("Horizontal");
		_movement.y = Input.GetAxis("Vertical");
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
		}
	}

	private void FixedUpdate() {
		if (DialogueManager.Instance.DialogueIsPlaying)
		{
			return;
		}

		PlayerManager.Instance.PlayerMovement(_movement, _rigidbody, velocity);
		PlayerManager.Instance.PlayerAnimation(_movement);

		if (isFlipped && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			isFlipped = false;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		};
	}

	private void Attack()
	{
		bool flipped = _movement.x < 0;

		if (flipped)
		{
			
			animator.Play("Attack");
			isFlipped = true;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			
			
		}
		else
		{
			animator.Play("Attack");
		}

		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<enemyController>().TakeDamage(attackDamage);
		}

	}

	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
			return;
		
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}