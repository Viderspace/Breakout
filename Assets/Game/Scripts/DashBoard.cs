using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class DashBoard : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text speedText;

        #endregion

        #region Fields

        private string _text;
        private const float RefreshTime = 0.3f;
        private float _time = 4f;

        #endregion


        #region Methods

        private void GetStats()
        {
            if (gameManager.Win)
            {
                _text = "Win! You are a Cannon";
            }
            else if (gameManager.GameOver)
            {
                _text = "Game Over. Hit Space key to Restart";
            }
            else if (gameManager.ballBehaviour.ballIsActive == false)
            {
                _text = "Paused";
            }
            else
            {
                _text = "Speed: " + (int) gameManager.ballSpeedFactor + "X";
            }
        }

        private void DisplayStats()
        {
            speedText.text = _text;
        }

        #endregion


        #region MonoBehaviour

        private void FixedUpdate()
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
                return;
            }
            GetStats();
            DisplayStats();
            _time = RefreshTime;
        }

        #endregion
    }
    
    
    
    
    
}