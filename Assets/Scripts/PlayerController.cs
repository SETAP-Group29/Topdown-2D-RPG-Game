using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float velocity = 1f;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;

    [SerializeField] private float collisionOffset = 0.05f;
    private ContactFilter2D _contactFilter;
    private List<RaycastHit2D> _results = new List<RaycastHit2D>();

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
    }
    
    private void FixedUpdate() {
        PlayerMovement(_movement);
    }

    private void PlayerMovement(Vector2 movement) {
        Vector2 xy = movement;
        Vector2 x = new Vector2(movement.x, 0);
        Vector2 y = new Vector2(0, movement.y);
        if (FlagCollisionWithPlayer(xy) == 1) PlayerMovePos(xy);
        else {
            if (FlagCollisionWithPlayer(x) == 1) PlayerMovePos(x);
            else {
                if (FlagCollisionWithPlayer(y) == 1) PlayerMovePos(y);
            }
        }
    }
    
    private void PlayerMovePos(Vector2 direction) {
        _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * velocity * direction);
    }
    
    private int FlagCollisionWithPlayer(Vector2 movement) {
        return _rigidbody.Cast(movement, _contactFilter, _results, collisionOffset + Time.fixedDeltaTime * velocity) == 0 ? 1 : 0;
    }
}
