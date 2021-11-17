using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehaviour : MonoBehaviour
{
    #region Inspector

    [SerializeField] private Animator paddleAnimator;

    [SerializeField] private Rigidbody2D physics;

    [SerializeField] [Range(0.5f, 5f)] private float initSpeed = 3f;

    #endregion


    #region Fields

    private readonly Vector3 _initPos = new Vector3(0, -3.5f, 0);

    private Vector2 _prevVelocity;

    private bool _maxSpeed = false;

    private int _paddleHitCount = 0;

    #endregion


    #region Functions

    private void BallHitsPaddle()
    {
        _paddleHitCount += 1;
        // Debug.Log($"hit count = {_paddleHitCount}");
        switch (_paddleHitCount)
        {
            case 4:
                _prevVelocity = _prevVelocity.normalized * initSpeed * 2;
                Debug.Log("2X Speed");
                break;
            case 8:
                _prevVelocity = _prevVelocity.normalized * initSpeed * 3;
                Debug.Log("3X Speed");
                break;
            case 12:
                _prevVelocity = _prevVelocity.normalized * initSpeed * 4;
                _maxSpeed = true;
                Debug.Log("4X Speed");
                break;
        }
    }

    private void BallHitsMiddleBlock()
    {
        _prevVelocity = _prevVelocity.normalized * initSpeed * 4;
        _maxSpeed = true;
        Debug.Log("4X Speed");
    }


    public void Respawn()
    {
        transform.position = _initPos;
        physics.velocity = new Vector2(0, 0);
        _paddleHitCount = 0;
        _maxSpeed = false;
    }

    private Vector2 RandomDirection()
    {
        float xRand = Random.Range(-0.99f, 0.99f);
        float yRand = Mathf.Sqrt(1 - xRand);
        return new Vector2(xRand, yRand).normalized * initSpeed;
    }

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        physics.gravityScale = 0;
        Respawn();
        FindObjectOfType<BlocksManager>().ResetLevel();
    }


    private void Update()
    {
        //TODO : add a condition here below (from GameManager), so that the ball cant start move during the blocks animation 
        if (Input.GetKeyUp(KeyCode.Space) && physics.velocity.magnitude < 0.1)
        {
            physics.velocity = _prevVelocity = RandomDirection();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Paddle")
        {
            //TODO - the animator was deleted, I need to recreate it for this call to trigger the animation
            // paddleAnimator.SetTrigger("PaddleBounce");
            if (!_maxSpeed)
            {
                BallHitsPaddle();
            }
        }


        else if (other.collider.CompareTag("MidRowBlock") && !_maxSpeed)
        {
            BallHitsMiddleBlock();
            Debug.Log("mid row hit!");
        }

        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        physics.velocity = newVelocity;
        _prevVelocity = newVelocity;
    }

    #endregion
}


