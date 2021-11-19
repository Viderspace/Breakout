using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] private BallBehaviour ballBehaviour;
    [SerializeField] private PaddleBehaviour paddleBehaviour;
    [SerializeField] private Animator paddleAnimator;
    [SerializeField] private LivesManager livesManager;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private Camera mainCam;
    [SerializeField] private AudioManager audioManager;

    [Header("Game controls")] [SerializeField] [Range(1, 200)]
    public float paddleSpeed = 20f;

    [SerializeField] [Range(0.5f, 5f)] public float ballSpeed = 3f;

    [Header("Visual controls")] [SerializeField] [Range(0.001f, 1f)]
    public float blocksAnimationTime = 0.2f;

    #endregion


    #region Fields

    private static GameManager _shared;
    private bool _gameOver;
    private bool _win;


    public enum BallSpeedFactor
    {
        Original = 1,
        Twice = 2,
        Trice = 3,
        Max = 4
    }

    public BallSpeedFactor ballSpeedFactor;

    #endregion


    #region Properties

    private bool GameOver
    {
        get => _gameOver;
        set
        {
            _gameOver = value;
            gameOverWindow.SetActive(true);
        }
    }

    private bool Win
    {
        get => _win;
        set
        {
            _win = value;
            if (value)
            {
                Debug.Log("Win!");
                winWindow.SetActive(true);
                PauseGame();
            }
        }
    }

    #endregion


    #region Methods

    private void PauseGame()
    {
        ballBehaviour.PauseBall();
        paddleBehaviour.DeactivatePaddle();
    }

    private void ResumeGame()
    {
        ballBehaviour.ResumeBall();
        audioManager.PlaySound("Ball Launch");
        paddleBehaviour.ActivatePaddle();
    }


    private void InitGame()
    {
        GameOver = false;
        Win = false;
        gameOverWindow.SetActive(false);
        winWindow.SetActive(false);
        blocksManager.ResetLevel();
        livesManager.ActivateAllLives();
        ballBehaviour.Respawn();
        PauseGame();
        gameOverWindow.SetActive(false);
        audioManager.PlaySound("InitGame");
    }

    public void BallEscaped()
    {
        paddleBehaviour.InitPosition();
        PauseGame();
        livesManager.ReduceLife();

        if (livesManager.LivesCount == 0)
        {
            GameOver = true;
            return;
        }

        mainCam.GetComponent<Animator>().SetTrigger("Ball Escaped Shake");
        audioManager.PlaySound("BallEscaped");
        ballBehaviour.Respawn();
    }


    public void BallCollision(Collision2D other)
    {
        if (other.collider.name == "Paddle")
        {
            paddleAnimator.SetTrigger("Hit");
            audioManager.PlaySound("Paddle Hit");

            if (ballSpeedFactor == BallSpeedFactor.Max)
            {
                return;
            }

            ballBehaviour.PaddleHitsSpeedBoost();
        }

        else if (other.collider.CompareTag("MidRowBlock"))
        {
            if (BallSpeedFactor.Max != ballSpeedFactor)
            {
                ballSpeedFactor = BallSpeedFactor.Max;
                audioManager.PlaySound("SpeedBoost");
                ballBehaviour.BallHitsMiddleBlock();
                return;
            }

            audioManager.PlaySound("BlockHit");
        }
        else if (other.collider.CompareTag("WORLD"))
        {
            audioManager.PlaySound("Wall Hit");
        }
        else if (other.collider.CompareTag("Block"))
        {
            audioManager.PlaySound("BlockHit");
        }

        if (blocksManager.BlocksRemaining == 0)
        {
            Win = true;
        }
    }

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        _shared = this;
        InitGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Space)) return;
        if (GameOver || Win)
        {
            InitGame();
            return;
        }

        if (ballBehaviour.ballIsActive == false)
        {
            ResumeGame();
            return;
        }

        PauseGame();
    }

    #endregion
}