using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float movementSpd = 2f;
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
        if (_rigidbody.Cast(_movement, _contactFilter, _results, movementSpd * Time.fixedDeltaTime + collisionOffset) == 0) {
            _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * movementSpd * _movement);
        }
        else {
            Vector2 tryVertical = new Vector2(0, _movement.y);
            if (_rigidbody.Cast(tryVertical, _contactFilter, _results, movementSpd * Time.fixedDeltaTime + collisionOffset) == 0) {
                _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * movementSpd * tryVertical);
            }
            else {
                Vector2 tryHorizontal = new Vector2(_movement.x, 0);
                if (_rigidbody.Cast(tryHorizontal, _contactFilter, _results, movementSpd * Time.fixedDeltaTime + collisionOffset) == 0) {
                    _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * movementSpd * tryHorizontal);
                }
            }
        };
    }
}
