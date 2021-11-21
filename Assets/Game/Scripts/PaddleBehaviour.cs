using UnityEngine;

namespace Game.Scripts
{
    public class PaddleBehaviour : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;


        #region Fields

        private Rigidbody2D _rigidbody2D;
        private readonly Vector3 _initPosition = new Vector3(0, -4.5f, 0);
        private float _move;
        private float _paddleSpeed;
        private State _currentState;

        private enum State
        {
            Disabled,
            Enabled
        }

        #endregion


        #region Methods

        public void InitPosition()
        {
            DeactivatePaddle();
            _rigidbody2D.position = _initPosition;
            
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
            _paddleSpeed = gameManager.paddleSpeed;
        }
        

        private void Update()
        {
            if (_currentState == State.Disabled) return;
            _move = Input.GetAxis("Horizontal") * _paddleSpeed;
        }

        private void FixedUpdate()
        {
            if (_currentState == State.Disabled) return;
            _rigidbody2D.AddForce(new Vector2(_move, 0));
        }

        #endregion
    }
}