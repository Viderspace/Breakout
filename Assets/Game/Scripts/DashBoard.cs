using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class DashBoard : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text speedText;
        
        private string _text;
        private float _refreshTime = 0.3f;
        private float _time = 3f;


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
                _text = "Speed: " + (int)gameManager.ballSpeedFactor +"X";
            }
            
        }

        private void DisplayStats()
        {
            speedText.text = _text;

        }

        private void FixedUpdate()
        {
            if (_time <= 0)
            {
                GetStats();
                DisplayStats();
                _time = _refreshTime;
            }
            else
            {
                _time -= Time.deltaTime;
            }
            
        }
    }
    
    
    
    
    
}