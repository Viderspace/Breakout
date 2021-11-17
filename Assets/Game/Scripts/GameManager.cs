using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private BlocksManager _blocksManager;
    [SerializeField] private BallBehaviour _ballBehaviour;
    [SerializeField] private PaddleBehaviour _paddleBehaviour;
    [SerializeField] private LivesManager _livesManager;

    #endregion


    #region Fields

    private static GameManager _shared;

    //Game Stats:
    private bool _gameOver;
    private bool _gameIsPaused;
    private bool _victory;

    // private  int _blocksRemaining = 55;
    
    #endregion


    #region Methods

    public void PauseResumeGame()
    {
        _gameIsPaused = !_gameIsPaused;

        _ballBehaviour.PauseResumeBall(_gameIsPaused);
        // if (_gameIsPaused)
        // {
        //     _ballBehaviour.PauseBallMovement();
        // }
        // else
        // {
        //     _ballBehaviour.RestartBallMovement();
        // }
    }


    // public void BallEscaped()
    //     // this function is called only by "invisible floor" object.
    //     //when the ball escapes it triggers the floor to reset the game
    // {
    //     Debug.Log("ball escaped (manager)");
    //     _lives -= 1;
    //     if (_lives == 0)
    //     {
    //         Debug.Log(_lives);
    //         _gameOver = true;
    //         return;
    //     }
    //
    //     PauseResumeGame();
    //
    //     // TODO : when player still have some lives left:
    //     // 1. set "lives remaining" animation on screen
    //     // 2. reset ball to init position
    //     // 3. hold the ball still until space key is pressed
    // }

    #endregion

    private void InitGame()
    {
        _blocksManager.ResetLevel();
        _livesManager.ActivateLives();
        _ballBehaviour.Respawn();
        _gameOver = false; 
        _gameIsPaused = true; 
        _victory = false;
        
        

    }

    public void BallFell()
    {
        _gameIsPaused = true;
        _livesManager.ReduceLife();
        if (_livesManager.LivesCount > 0)
        {
            _ballBehaviour.Respawn();
            return;
        }
        _gameOver = true;
        InitGame();
    }

    

    
    
    

    #region MonoBehaviour

    void Awake()
    {
        _shared = this;
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) )
        {
            PauseResumeGame();
        }
    }

    #endregion


}
