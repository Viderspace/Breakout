using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D physics;
    private Vector2 _prevVelocity;
    private Vector3 initPos = new Vector3(0, -3.5f, 0);
    private bool maxSpeed = false;

    private float initSpeed = 3f;
    // private float currtSpeedFactor = 1;
    private int paddleHitCount = 0;

    private void BallHitsPaddle()
    {
        paddleHitCount += 1;
        Debug.Log($"hit count = {paddleHitCount}");
        switch (paddleHitCount)
        {
            case 4:
                
                _prevVelocity = _prevVelocity.normalized * initSpeed * 2;
                Debug.Log("2X Speed");
                break;
            case 8:
                _prevVelocity = _prevVelocity.normalized * initSpeed * 3;
                Debug.Log("3X Speed");
                break;
            case 12:
                _prevVelocity = _prevVelocity.normalized * initSpeed * 4;
                maxSpeed = true;
                Debug.Log("4X Speed");
                break;
        }
    }
    private void BallHitsMiddleBlock()
    {
        _prevVelocity = _prevVelocity.normalized * initSpeed * 4;
        maxSpeed = true;
        Debug.Log("4X Speed");
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(_prevVelocity);
        if (other.collider.name == "Paddle" && !maxSpeed)
        {
            BallHitsPaddle();
        }

        
        else if (other.collider.tag == "MidRowBlock"  && !maxSpeed)
        {
            BallHitsMiddleBlock();
            Debug.Log("mid row hit!");
        }
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        physics.velocity = newVelocity;
        _prevVelocity = newVelocity;
    }



    public void Respwan()
    {
        transform.position = initPos;
        physics.velocity = new Vector2(0, 0);
        // currtSpeedFactor = 1;
        paddleHitCount = 0;
        maxSpeed = false;

    }
    
    void Awake()
    {
        physics.gravityScale = 0;
        Respwan();
        FindObjectOfType<BricksManager>().ResetLevel();
        

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)  && physics.velocity.magnitude < 0.1)
        {
            float xRand = Random.Range(-0.8f, 0.8f);
            float yRand = Mathf.Sqrt(1 - xRand);
            // Debug.Log($"x+y is: {xRand+yRand}. x is {xRand}, y is {yRand}");
            physics.velocity = _prevVelocity = new Vector2(xRand, yRand).normalized * initSpeed;
        }
    }
}
