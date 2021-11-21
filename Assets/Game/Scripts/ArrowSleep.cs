using UnityEngine;

namespace Game.Scripts
{
    public class ArrowSleep : MonoBehaviour
    {
        public void ArrowSetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}