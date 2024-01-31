using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryUIManager : MonoBehaviour
{
    [Header("External references")]
    [SerializeField] private AnimationSettings _victorySettings;
    [SerializeField] private RSE_VictoryLineReached _rseVictoryLineReached;
    [SerializeField] private RSE_Sound _rseSound;
    
    [Header("References")] 
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _illustration;
    [SerializeField] private Image _plane;

    [Header("Menu")]
    [SerializeField] private Image _menuBackground;
    [SerializeField] private float _menuBackgroundFadeAmount = 0.9f;
    [SerializeField] private float _menuFadeOutDuration;
    [SerializeField] private GameObject _menuGameObject;
    
    [Header("Easter egg")] 
    [SerializeField] private float _durationBeforePlane;
    [SerializeField] private float _translationDuration;
    [SerializeField] private GameObject _endingPoint;
    

    private void OnEnable() => _rseVictoryLineReached.action += StartAniamtion;
    private void OnDisable() => _rseVictoryLineReached.action -= StartAniamtion;
    private void StartAniamtion() => StartCoroutine(PlayVictory());
    private IEnumerator PlayVictory()
    {
        // black fade in
        _blackImage.DOFade(1, _victorySettings.fadeInDuration).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(_victorySettings.fadeInDuration);
        
        _illustration.gameObject.SetActive(true);

        // slides
        foreach (var introImage in _victorySettings.introImages)
        {
            if (introImage.audioClip != null) _rseSound.Call(TypeSound.VFX, introImage.audioClip, false);
            _illustration.sprite = introImage.image;
            yield return new WaitForSeconds(introImage.duration);
        }
        // black fade in 
        _menuBackground.DOFade(_menuBackgroundFadeAmount, _menuFadeOutDuration);
        yield return new WaitForSeconds(_menuFadeOutDuration);
        _menuGameObject.SetActive(true);
        
        // plane waiting time
        yield return new WaitForSeconds(_durationBeforePlane);
        
        // plane translation
        _plane.transform.DOMove(_endingPoint.transform.position, _translationDuration);
        yield return new WaitForSeconds(_translationDuration);

        SceneManager.LoadScene("Menu");
    }
}