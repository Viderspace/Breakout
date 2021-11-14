using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColorChange : GameManager
{
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
        // _spriteRenderer.color = Color.black;
    }
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {


        if (_gameIsPaused)
        {
            _spriteRenderer.color = Color.gray;
        }
        if (_gameOver)
        {
            _spriteRenderer.color = Color.red;
        }

        if (_victory)
        {
            _spriteRenderer.color = Color.green;
        }

        _spriteRenderer.color = Color.black;
    }
}
