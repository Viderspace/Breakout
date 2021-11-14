using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool LeftisPressed;
    private bool RightisPressed;

    //allows to adjust paddle speed from inspector
    [Range(1,200)]
    public float paddleSpeed;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            LeftisPressed = true;
            RightisPressed = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            RightisPressed = true;
            LeftisPressed = false;
        }
        else
        {
            RightisPressed = false;
            LeftisPressed = false;
        }

    }

    private void FixedUpdate()
    {
        if (LeftisPressed && !RightisPressed)
        {
            _rigidbody2D.AddRelativeForce(Vector3.left * paddleSpeed  );
        }
        else if (RightisPressed && !LeftisPressed) 
        {
            _rigidbody2D.AddRelativeForce(Vector3.right * paddleSpeed  );
        }
    }
}


