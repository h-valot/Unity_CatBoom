using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip _musicMenu;
    [SerializeField] private AudioClip _introSound;

    [SerializeField] private RSE_Sound _rseSoundLaunch;
    [SerializeField] private RSE_Sound _rseSoundStop;

    private void Start()
    {
        _rseSoundLaunch.Call(TypeSound.Background, _musicMenu, true);
    }

    public void PlaySoundPlay()
    {
        _rseSoundStop.Call(TypeSound.Background, _musicMenu, true); 
        _rseSoundLaunch.Call(TypeSound.Background, _introSound, true);
    }
}