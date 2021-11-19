using UnityEngine;

public class LivesManager : MonoBehaviour
{
    #region Inspector

    // [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;
    [SerializeField] private GameObject text;

    #endregion


    #region Fields

    private static LivesManager _shared;

    #endregion

    #region Properties

    public int LivesCount { get; set; }

    #endregion


    #region Methods

    public void ActivateAllLives()
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        LivesCount = 3;
    }


    private void ReduceLifeAnimation()
    {
        life1.GetComponent<Animator>().SetTrigger("LoseLife");
        life2.GetComponent<Animator>().SetTrigger("LoseLife");
        life3.GetComponent<Animator>().SetTrigger("LoseLife");
        text.GetComponent<Animator>().SetTrigger("LoseLife");
    }


    public void ReduceLife()
    {
        // _animationStart = true;
        ReduceLifeAnimation();
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

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        _shared = this;
    }

    #endregion
}