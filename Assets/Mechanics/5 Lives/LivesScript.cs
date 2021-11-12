using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    private  int _livesCount = 3;
    
    void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Space) || _livesCount <=0)
            return;
        
        _livesCount -= 1;
        gameObject.transform.GetChild(_livesCount).gameObject.SetActive(false);
        Debug.Log(_livesCount);
    }
}
