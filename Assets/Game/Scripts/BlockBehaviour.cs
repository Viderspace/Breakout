using System;
using UnityEngine;

namespace Game.Scripts
{
    public class BlockBehaviour : MonoBehaviour
    {
        #region Fields

        private SpriteRenderer _spriteRenderer;

        //animation related
        [NonSerialized] public Vector2Int Coordinates;
        public bool animationTrigger;
        private bool _animationDone;
        private float _growingFactor;
        private readonly float growingSpeed = 2.5f;

        // fixed size localScale for a block:
        private const float XScale = 1.472534f;
        private const float YScale = 0.28776f;

        #endregion

        #region Methods

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        #endregion
    
        #region MonoBehavior

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(0, 0, 0);
            _animationDone = false;
        }

        private void Update()
        {
            if (_animationDone || !animationTrigger) return;

            _growingFactor += Time.deltaTime * growingSpeed;
            _spriteRenderer.transform.localScale = new Vector3(XScale * _growingFactor, YScale * _growingFactor, 0);

            if (!(_growingFactor >= 1f)) return;
            _spriteRenderer.transform.localScale = new Vector3(XScale, YScale, 0);
            _animationDone = true;
        }

        private void OnCollisionEnter2D()
        {
            BlocksManager blocksManager = FindObjectOfType<BlocksManager>();
            blocksManager.ReduceBlock(gameObject);
            Destroy(gameObject);
        }

        #endregion
    }
}