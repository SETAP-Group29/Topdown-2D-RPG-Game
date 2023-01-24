using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 10f;
    
    private Sprite _sprite;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {

        this._sprite = GetComponent<Sprite>();
        this._rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float horizontalVelocity = (Input.GetAxis("Horizontal")) * _speed;
        float verticalVelocity = (Input.GetAxis("Vertical")) * _speed;

        this._rigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    void Update()
    {
        
    }
}
