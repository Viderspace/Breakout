using UnityEngine;


public class BallTrail : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject echo;

    [Header("Animation Controls")]
    [SerializeField] private float startTimeBtwSpawns;

    #endregion
    
    private float _timeBtwSpawns;

    #region MonoBehaviour

    private void Update()
    {
        if (gameManager.ballSpeedFactor != GameManager.BallSpeedFactor.Max) return;

        if (_timeBtwSpawns <= 0)
        {
            //spawn echo ball object
            GameObject instance = (GameObject) Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, startTimeBtwSpawns);
            _timeBtwSpawns = startTimeBtwSpawns / 4;
            return;
        }

        _timeBtwSpawns -= Time.deltaTime;
    }

    #endregion
}