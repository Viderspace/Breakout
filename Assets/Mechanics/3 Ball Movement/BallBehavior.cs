using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Rigidbody2D physics = default;
    private Vector2 _prevVelocity;
    
    // slider for adjusting the ball's speed (in inspector)
    [Range(0.0f, 30.0f)]
    public float movementSpeed = 5f;
    


    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        physics.velocity = newVelocity;
        _prevVelocity = newVelocity;
    }
    
    void Awake()
    {
        physics.velocity = _prevVelocity = new Vector2(movementSpeed, movementSpeed);
    }
    
}

