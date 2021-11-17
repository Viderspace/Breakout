using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;
    #endregion


    #region Fields
    
    private static LivesManager _shared;

    #endregion

    #region Properties
    public int LivesCount { get; set; }

    #endregion
    
    
    
    
    
    public void ActivateLives()
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        LivesCount = 3;
    }




    public void ReduceLife()
    {
        switch (LivesCount)
        {
            case 3:
                life3.SetActive(false);

                break;
            case 2:
                life2.SetActive(false);
                break;
            case 1:
                life1.SetActive(false);
                Debug.Log("GAME OVER!  (LivesManager)");
                break;
        }
        LivesCount -= 1;
    }



    private void Awake()
    {
        _shared = this;
    }
}
