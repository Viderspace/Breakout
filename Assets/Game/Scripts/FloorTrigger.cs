using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : GameManager
{


    private void OnTriggerEnter2D()
    {
        BallEscaped();
        
    }
}
