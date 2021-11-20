using UnityEngine;

namespace Game.Scripts
{
    public class FloorTrigger : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            gameManager.BallEscaped();
        }
    }
}
