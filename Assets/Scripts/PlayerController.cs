using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float movementSpd = 2f;
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
        _rigidbody.MovePosition(_rigidbody.position + Time.fixedDeltaTime * movementSpd * _movement);
    }
}
