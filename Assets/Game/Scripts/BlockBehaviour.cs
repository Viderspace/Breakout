using System;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    #region Fields

    private SpriteRenderer _spriteRenderer;


    //animation related
    [NonSerialized] public Vector2Int Coordinates;
    public bool animationTrigger = false;
    private bool _animationDone = false;
    private float _growingFactor = 0.0f;
    private float growingSpeed = 2.5f;

    // fixed size localScale for a block:
    private float xScale = 1.472534f;
    private float yScale = 0.28776f;

    #endregion


    #region MonoBehavior

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.localScale = new Vector3(0, 0, 0);
        _animationDone = false;
    }

    private void Update()
    {
        if (_animationDone || !animationTrigger)
        {
            return;
        }

        _growingFactor += Time.deltaTime * growingSpeed;

        _spriteRenderer.transform.localScale = new Vector3(xScale * _growingFactor, yScale * _growingFactor, 0);
        if (_growingFactor >= 1f)
        {
            _spriteRenderer.transform.localScale = new Vector3(xScale, yScale, 0);
            _animationDone = true;
        }
    }

    private void OnCollisionEnter2D()
    {
        FindObjectOfType<BlocksManager>().ReduceBlock(gameObject);
        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
        Debug.Log(this.Coordinates);
    }

    #endregion
}