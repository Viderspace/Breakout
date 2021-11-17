using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    #region Fields

    private Rigidbody2D _rigidbody2D;

    private State _currentState;

    enum State
    {
        Idle,
        Left,
        Right
    }

    #endregion


    #region Inspector

    [SerializeField] [Range(1, 200)] private float paddleSpeed;

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            _currentState = State.Left;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            _currentState = State.Right;
            return;
        }
        _currentState = State.Idle;
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case State.Left:
                _rigidbody2D.AddForce(Vector3.left * paddleSpeed);
                break;
            case State.Right:
                _rigidbody2D.AddForce(Vector3.right * paddleSpeed);
                break;
        }
    }

    #endregion
}


