using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<AudioSourceList> _audioSources;
    [SerializeField] private RSE_Sound _rseSoundLaunch;
    [SerializeField] private RSE_Sound _rseSoundStop;
    [SerializeField] private AudioMixerGroup _musicAudioMixer;
    [SerializeField] private AudioMixerGroup _vfxAudioMixer;
    [SerializeField] private RSO_MusicVolume _rsoMusicVolume;
    [SerializeField] private RSO_VfxVolume _rsoVfxVolume;
    
    private void OnEnable()
    {
        _rseSoundLaunch.action += LaunchSound;
        _rseSoundStop.action += StopSound;
        
        _rsoMusicVolume.OnChanged += UpdateMusicVolume;
        _rsoVfxVolume.OnChanged += UpdateVfxVolume;
    }

    private void OnDisable()
    {
        _rseSoundLaunch.action -= LaunchSound;
        _rseSoundStop.action -= StopSound;
        
        _rsoMusicVolume.OnChanged -= UpdateMusicVolume;
        _rsoVfxVolume.OnChanged -= UpdateVfxVolume;
    }

    private bool IsSoundPlaying(TypeSound typeSound, ref AudioClip audioClip)
    {
        return _audioSources.Find(o => o.typeSound == typeSound).audioSources[0].clip == audioClip;
    }

    private AudioSource GetSourceTarget(TypeSound typeSound, AudioClip audioClip)
    {
        var source = _audioSources.Find(o => o.typeSound == typeSound).audioSources.Find(o => o.clip == audioClip);
        return (source == null) ? _audioSources.Find(o => o.typeSound == typeSound).audioSources.Find(o => o.clip == null) : source;
    }

    private void StopSound(TypeSound typeSound, AudioClip audioClip, bool isLoop) 
    { 
        var audioSource = GetSourceTarget(typeSound, audioClip);
        if (audioSource != null)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
    private void LaunchSound(TypeSound typeSound, AudioClip audioClip, bool isLoop)
    {
        if (!IsSoundPlaying(typeSound, ref audioClip)) 
        { 
            var audioSource = GetSourceTarget(typeSound, audioClip); 
            audioSource.clip = audioClip; 
            audioSource.Play();
            audioSource.loop = isLoop;
            if (!isLoop) StartCoroutine(UnlockSourceAudio(audioSource, audioClip.length));
        } 
    }

    private void UpdateMusicVolume() => _musicAudioMixer.audioMixer.SetFloat("Music Volume", Mathf.Log10(_rsoMusicVolume.value) * 20);
    private void UpdateVfxVolume() => _vfxAudioMixer.audioMixer.SetFloat("Vfx Volume", Mathf.Log10(_rsoVfxVolume.value) * 20);

    private IEnumerator UnlockSourceAudio(AudioSource source,float delay)
    {
        yield return new WaitForSeconds(delay);
        source.clip = null;
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