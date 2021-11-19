using System;
using UnityEngine;


public class PaddleBehaviour : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;


    #region Fields

    private Rigidbody2D _rigidbody2D;
    private readonly Vector3 _initPosition = new Vector3(0, -4.5f, 0);
    private float _move = 0f;
    private float _paddleSpeed;
    private State _currentState;

    public enum State
    {
        Disabled,
        Enabled
    }

    #endregion


    #region Methods

    public void InitPosition()
    {
        transform.position = _initPosition;
        DeactivatePaddle();
    }

    public void DeactivatePaddle()
    {
        _currentState = State.Disabled;
    }

    public void ActivatePaddle()
    {
        _currentState = State.Enabled;
    }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _currentState = State.Disabled;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _paddleSpeed = _gameManager.paddleSpeed;
    }


    private void Update()
    {
        if (_currentState == State.Disabled) return;

        _move = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (_currentState == State.Disabled) return;

        _rigidbody2D.AddForce(new Vector2(_move * _paddleSpeed, 0));
    }

    #endregion
}