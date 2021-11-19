using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Inspector + Fields

    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    
    private AudioSource _audioSource;
    
    #endregion
    
    #region Methods

    private AudioClip GetClip(string clipName)
    {
        for (var i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == clipName)
            {
                return sounds[i];
            }
        }

        return null;
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