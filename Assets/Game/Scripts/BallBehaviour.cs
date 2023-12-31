using UnityEngine;

namespace Game.Scripts
{
    public class BallBehaviour : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private GameManager gameManager;
        [SerializeField] private ArrowRotation arrow;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Rigidbody2D physics;


        #endregion


        #region Fields

        private readonly Vector3 _initPos = new Vector3(0, -3.5f, 0);
        private float _initSpeed;
        [HideInInspector] public bool ballIsActive;
        private Vector2 _prevVelocity;
        private int PaddleHitCount { get; set; }

        #endregion


        #region Functions

        public void PaddleHitsSpeedBoost()
        {
            PaddleHitCount += 1;
            switch (PaddleHitCount)
            {
                case 4:
                    _prevVelocity = _prevVelocity.normalized * _initSpeed * (int) GameManager.BallSpeedFactor.Twice;
                    gameManager.ballSpeedFactor = GameManager.BallSpeedFactor.Twice;
                    break;
                case 8:
                    _prevVelocity = _prevVelocity.normalized * _initSpeed * (int) GameManager.BallSpeedFactor.Trice;
                    gameManager.ballSpeedFactor = GameManager.BallSpeedFactor.Trice;
                    break;
                case 12:
                    _prevVelocity = _prevVelocity.normalized * _initSpeed * (int) GameManager.BallSpeedFactor.Max;
                    gameManager.ballSpeedFactor = GameManager.BallSpeedFactor.Max;
                    break;
            }
        }

        public void BallHitsMiddleBlock()
        {
            _prevVelocity = _prevVelocity.normalized * _initSpeed * (int) GameManager.BallSpeedFactor.Max;
        }


        public void Respawn()
        {
            transform.position = _initPos;
            physics.velocity = _prevVelocity = new Vector2(0, 0);
            arrow.gameObject.SetActive(true);
            PaddleHitCount = 0;
            gameManager.ballSpeedFactor = GameManager.BallSpeedFactor.Original;
            ballIsActive = false;
        }
        

        public void PauseBall()
        {
            ballIsActive = false;
            _prevVelocity = physics.velocity;
            physics.velocity = new Vector2(0, 0);
            audioManager.PlaySound("Pause Game");
        }

        public void ResumeBall()
        {
            if (transform.localPosition != _initPos)
            {
                physics.velocity = _prevVelocity;
            }
            else
            {
                physics.velocity = _prevVelocity = arrow.GetVelocity()*_initSpeed;
                // ballLaunch.Invoke();
                arrow.gameObject.SetActive(false);
            }

            ballIsActive = true;
        }

        #endregion


        #region MonoBehaviour
        

        private void Awake()
        {
            _initSpeed = gameManager.ballSpeed;
            physics.gravityScale = 0;
            Respawn();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            gameManager.BallCollision(other);
            var contact = other.contacts[0];
            // ContactPoint2D contact = other.GetContact(0);
            var contactNormal = contact.normal;
            var newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
            physics.velocity = newVelocity;
            _prevVelocity = newVelocity;
        }

        #endregion
    }
}


