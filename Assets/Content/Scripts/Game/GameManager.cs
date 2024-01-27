using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSE_Sound _rseSoundPlay;
    [SerializeField] private RSE_Sound _rseSoundStop;
    [SerializeField] private AudioClip[] _audioClips;
    private void Start() { foreach (AudioClip audio in _audioClips) _rseSoundPlay.Call(TypeSound.Background, audio, true); }
}
