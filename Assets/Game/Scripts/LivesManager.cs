using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private int _livesCount = 3;

    public void ReduceLife()
    {
        _livesCount -= 1;
        gameObject.transform.GetChild(_livesCount).gameObject.SetActive(false);
        if (_livesCount == 0)
        {
            Debug.Log("GAME OVER!  (LivesManager)");
            NewGame();
            
        }
    }

    public void NewGame()
    {
        _livesCount = 3;
        for (int lifeObject = 0; lifeObject < _livesCount; lifeObject++)
        {
            gameObject.transform.GetChild(lifeObject).gameObject.SetActive(true);
            FindObjectOfType<BricksManager>().ResetLevel();
        }
    }
    
    
    
}
