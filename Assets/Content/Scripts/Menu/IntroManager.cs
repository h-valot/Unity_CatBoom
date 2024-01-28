using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _illustration;
    [SerializeField] private AnimationSettings _animationSettings;
    
    
    [Header("External references")] 
    [SerializeField] private RSE_Sound _rseSound;
    

    private bool _canLoadScene;
    private AsyncOperation _asyncOperation;
    
    public async void PlayIntro()
    {
        LoadSceneAsync(_animationSettings.sceneToLoad);
        
        // black fade in
        _blackImage.DOFade(1, _animationSettings.fadeInDuration).SetEase(Ease.OutCirc);
        await Task.Delay(Mathf.RoundToInt(_animationSettings.fadeInDuration * 1000));
        
        _illustration.gameObject.SetActive(true);
        // _introSettings.voiceOver.Play;

        // slides
        foreach (IntroImage introImage in _animationSettings.introImages)
        {
            if (introImage.audioClip != null) _rseSound.Call(TypeSound.VFX, introImage.audioClip, false);
            _illustration.sprite = introImage.image;
            await Task.Delay(Mathf.RoundToInt(introImage.duration * 1000));
        }

        // load scene
        _asyncOperation.allowSceneActivation = true;
    }

    private async void LoadSceneAsync(string sceneToLoad)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (_asyncOperation.progress < 0.9f) await Task.Delay(100);
        _asyncOperation.allowSceneActivation = false;
    }
}