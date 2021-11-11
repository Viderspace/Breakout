using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private int _direction  = 0;
    public float speed = 3;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player wants to move Paddle left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction = -1;
        }
        // Player wants to move Paddle right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction = 1;
        }
        // no movement
        else
        {
            _direction = 0;
        }
    }
    
    
    
//     private void FixedUpdate()
//     {
//         if (_direction == 0)
//         {
//         }
//     }
//     
//     
//     
//     
}




