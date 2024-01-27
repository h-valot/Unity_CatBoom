using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private IntroManager _introManager;

    public void Play()
    {
        print("MENU UI MANAGER: button pressed");
        _introManager.PlayIntro();
    }
}