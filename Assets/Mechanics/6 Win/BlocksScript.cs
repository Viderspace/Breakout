using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksScript : RedTurnsGreen
{
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        ReduceOne();
    }
}
