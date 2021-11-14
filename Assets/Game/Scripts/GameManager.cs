using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static int _lives = 3;
    private static int _blocksRemaining = 55;
    
    public static bool _gameOver = false;
    public static bool _gameIsPaused = false;
    public static bool _victory = false;

    public void PauseResumeGame()
    {
        _gameIsPaused = !_gameIsPaused;
    }



    public void ReduceBlock() 
        // when a block is hit by the ball it deactivates itself, and calls this function 
    {
        _blocksRemaining -= 1;
    }
    
    
    public void BallEscaped()
    // this function is called only by "invisible floor" object.
    //when the ball escapes it triggers the floor to reset the game
    {
        Debug.Log("ball escaped (manager)");
        _lives -= 1;
        if (_lives == 0)
        {
            Debug.Log(_lives);
            _gameOver = true;
            return;
        }

        PauseResumeGame();

        // TODO : when player still have some lives left:
        // 1. set "lives remaining" animation on screen
        // 2. reset ball to init position
        // 3. hold the ball still until space key is pressed



    }
        
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
