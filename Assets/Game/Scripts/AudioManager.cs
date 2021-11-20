using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        #region Inspector + Fields

        [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    
        private AudioSource _audioSource;
    
        #endregion
    
        #region Methods

        private AudioClip GetClip(string clipName)
        {
            return sounds.FirstOrDefault(t => t.name == clipName);
        }

        public void PlaySound(string soundName)
        {
            var sound = GetClip(soundName);
            _audioSource.clip = sound;
            _audioSource.Play();
        }

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        #endregion
    }
}