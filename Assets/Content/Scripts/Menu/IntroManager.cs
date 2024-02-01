using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _illustration;
    [SerializeField] private TextMeshProUGUI _subtitleTM;
    [SerializeField] private AnimationSettings _animationSettings;
    
    [Header("External references")] 
    [SerializeField] private RSE_Sound _rseSound;
    
    private bool _canLoadScene;
    private AsyncOperation _asyncOperation;

    public void PlayIntro() => StartCoroutine(PlayIntroAnimation());
    
    private IEnumerator PlayIntroAnimation()
    {
        StartCoroutine(LoadSceneAsync(_animationSettings.sceneToLoad));
        
        // black fade in
        _blackImage.DOFade(1, _animationSettings.fadeInDuration).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(_animationSettings.fadeInDuration);
        _illustration.gameObject.SetActive(true);
        _subtitleTM.gameObject.SetActive(true);

        // slides
        foreach (IntroImage introImage in _animationSettings.introImages)
        {
            if (introImage.audioClip != null) _rseSound.Call(TypeSound.VFX, introImage.audioClip, false);
            _subtitleTM.text = introImage.subtitle;
            _illustration.sprite = introImage.image;
            yield return new WaitForSeconds(introImage.duration);
        }

        // load scene
        _asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (_asyncOperation.progress < 0.9f) yield return null;
        _asyncOperation.allowSceneActivation = false;
    }
}