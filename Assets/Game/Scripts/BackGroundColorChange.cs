using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColorChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer = default;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer.color = Color.black;
    }
    void Awake()
    {
        spriteRenderer.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {

    //
    //     if (_gameIsPaused)
    //     {
    //         _spriteRenderer.color = Color.gray;
    //     }
    //     if (_gameOver)
    //     {
    //         _spriteRenderer.color = Color.red;
    //     }
    //
    //     if (_victory)
    //     {
    //         _spriteRenderer.color = Color.green;
    //     }
    //
    //     _spriteRenderer.color = Color.black;
    }
}
