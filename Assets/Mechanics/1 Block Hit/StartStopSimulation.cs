using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopSimulation : MonoBehaviour
{

    public Rigidbody2D physics;
    private bool _gravity;
    
    void Awake()
    {
        physics.bodyType = RigidbodyType2D.Static;
    }

    private void ToggleGravity()
    {
        _gravity = !_gravity;
        
        if (_gravity)
        {
            //set rigidbody to dynamic mode
            physics.bodyType = RigidbodyType2D.Dynamic;
            return;
        }
        //reset ball to original location + set rigidbody to static mode
        gameObject.transform.position = new Vector3(0, 0, 0);
        physics.bodyType = RigidbodyType2D.Static;
    }


    // Update is called once per frame
    void Update()
    {
        // when space key is triggered, toggle modes
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ToggleGravity();
        }
    }
}
