using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaivior : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void OnCollisionEnter2D()
    {
        FindObjectOfType<BricksManager>().ReduceBlock();
        Destroy(gameObject);
    }
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

}
