using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance { get; private set; }
    
    [SerializeField] private float collisionOffset = 0.05f;
    private ContactFilter2D _contactFilter;
    private readonly List<RaycastHit2D> _results = new();
    
    [SerializeField] private Animator animator;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning("Found more than one Player Manager in the scene");
        }
        Instance = this;
    }
    
    public void PlayerAnimation(Vector2 movement) {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void PlayerMovement(Vector2 movement, Rigidbody2D rigidbody, float velocity) {
        var xy = movement;
        var x = new Vector2(movement.x, 0);
        var y = new Vector2(0, movement.y);
        if (FlagCollisionWithPlayer(xy, rigidbody, velocity) == 1) {
            PlayerMovePos(xy, rigidbody, velocity);
        }
        else {
            if (FlagCollisionWithPlayer(x, rigidbody, velocity) == 1) {
                PlayerMovePos(x, rigidbody, velocity);
            }
            else {
                if (FlagCollisionWithPlayer(y, rigidbody, velocity) == 1) PlayerMovePos(y, rigidbody, velocity);
            }
        }
    }

    private void PlayerMovePos(Vector2 direction, Rigidbody2D rigidbody, float velocity) {
        rigidbody.MovePosition(rigidbody.position + Time.fixedDeltaTime * velocity * direction);
    }

    private int FlagCollisionWithPlayer(Vector2 movement, Rigidbody2D rigidbody, float velocity) {
        return rigidbody.Cast(movement, _contactFilter, _results, collisionOffset + Time.fixedDeltaTime * velocity) ==
               0
            ? 1
            : 0;
    }
    
}
