using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private IntroManager _introManager;

    public void Play()
    {
        _introManager.PlayIntro();
    }
}