using UnityEngine;

namespace Game.Scripts
{
    public class ArrowRotation : MonoBehaviour
    {
        #region Fields

        private float _minAngle = -80.0f;
        private float _maxAngle = 80.0f;
        
        private const float Speed = 0.35f;

        private float _t;
        private Vector3 _vec;

        #endregion


        #region MonoBehaviour

        void Update()
        {
            _vec = new Vector3(0, 0, Mathf.SmoothStep(_minAngle, _maxAngle, _t));
            transform.eulerAngles = _vec;
            _t += Time.deltaTime * Speed;

            if (_t > 1.0f)
            {
                (_maxAngle, _minAngle) = (_minAngle, _maxAngle);
                _t = 0.0f;
            }
        }

        #endregion


        public Vector2 GetVelocity()
            // ball calls this function when launched to get a starting direction
        {
            var deg = (_vec.z + 90) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(deg), Mathf.Sin(deg)).normalized;
        }
    }
}