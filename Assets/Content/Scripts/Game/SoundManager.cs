using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSourceList> _audioSources;
    [SerializeField] private RSE_Sound _rseSoundLaunch;
    [SerializeField] private RSE_Sound _rseSoundStop;

    private void OnEnable()
    {
        _rseSoundLaunch.action += LauchSound;
        _rseSoundStop.action += StopSound;
    }

    private void OnDisable()
    {
        _rseSoundLaunch.action -= LauchSound;
        _rseSoundStop.action -= StopSound;

    }

    private bool IsSoundPlaying(TypeSound typeSound, ref AudioClip audio)
    {
        switch (typeSound)
        {
            case TypeSound.VFX:
                return _audioSources.Find(o => o.typeSound == typeSound).audioSources[0].clip == audio;
            default:
                return false;
        }
    }

    private AudioSource GetSourceTarget(TypeSound typeSound, AudioClip audio)
    {
        switch (typeSound)
        {
            case TypeSound.VFX:
                return _audioSources.Find(o => o.typeSound == typeSound).audioSources[0];
            case TypeSound.Background:
                var source = _audioSources.Find(o =>o.typeSound == typeSound).audioSources.Find(o => o.clip == audio);
                return (source == null)? _audioSources.Find(o => o.typeSound == typeSound).audioSources.Find(o=> o.clip == null):source;
            default:
                return null;
        }
    }

    private void StopSound(TypeSound typeSound, AudioClip audio, bool isLoop) 
    { 
        var audioSource = GetSourceTarget(typeSound, audio);
        if (audioSource != null)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
    private void LauchSound(TypeSound typeSound, AudioClip audio, bool isLoop) 
    { 
        if (!IsSoundPlaying(typeSound, ref audio)) 
        { 
            var audioSource = GetSourceTarget(typeSound, audio); 
            audioSource.clip = audio; 
            audioSource.Play();
            audioSource.loop = isLoop; 
        } 
    }

}

[System.Serializable]
public enum TypeSound
{
    Background,
    VFX,
}

[System.Serializable]
public class AudioSourceList
{
    public TypeSound typeSound;
    public List<AudioSource> audioSources;
}