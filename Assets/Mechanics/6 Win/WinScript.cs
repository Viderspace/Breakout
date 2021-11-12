using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private  int _blocks_remaining = 9;
    private SpriteRenderer _spriteRenderer;

    // Update is called once per frame
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _blocks_remaining -= 1;
            gameObject.transform.GetChild(_blocks_remaining).gameObject.SetActive(false);
            Debug.Log(_blocks_remaining);
        }
        
        if ( _blocks_remaining <= 0)
        {
            _spriteRenderer.color = Color.green;
        }
    }
}
