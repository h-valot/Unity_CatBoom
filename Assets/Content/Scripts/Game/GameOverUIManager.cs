using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [Header("External references")]
    [SerializeField] private AnimationSettings _gameOverSettings;
    [SerializeField] private RSE_Sound _rseSound;
    [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;
    
    [Header("References")] 
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _illustration;
    [SerializeField] private Image _nukeImage;
    
    [Header("Animation settings")]
    [SerializeField] private float _flashBangFadeOutDuration;

    [Header("Retry menu")] 
    [SerializeField] private Image _retryBackground;
    [SerializeField] private float _retryBackgroundFadeAmount = 0.9f;
    [SerializeField] private GameObject _retryMenu;
    [SerializeField] private float _retryFadeIntDuration;
    

    private void OnEnable() => _rsePlayerDeath.action += StartAnimation;
    private void OnDisable() => _rsePlayerDeath.action -= StartAnimation;
    private void StartAnimation() => StartCoroutine(PlayGameOver());
    private IEnumerator PlayGameOver()
    {
        // black fade in
        _blackImage.DOFade(1, _gameOverSettings.fadeInDuration).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(_gameOverSettings.fadeInDuration);
        _illustration.gameObject.SetActive(true);

        // cat's face
        _illustration.sprite = _gameOverSettings.introImages[0].image;
        if (_gameOverSettings.introImages[0].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[0].audioClip, false);
        yield return new WaitForSeconds(_gameOverSettings.introImages[0].duration);
        
        // flash bang
        _illustration.sprite = _gameOverSettings.introImages[1].image;
        if (_gameOverSettings.introImages[1].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[1].audioClip, false);
        yield return new WaitForSeconds(_gameOverSettings.introImages[1].duration);
        
        // display nuke
        if (_gameOverSettings.introImages[2].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[2].audioClip, false);
        _nukeImage.gameObject.SetActive(true);
        
        // fade out flash bang
        _illustration.DOFade(0, _flashBangFadeOutDuration).SetEase(Ease.OutCirc);
        
        // display nuke for x seconds
        yield return new WaitForSeconds(_gameOverSettings.introImages[2].duration);

        _retryBackground.DOFade(_retryBackgroundFadeAmount, _retryFadeIntDuration);
        yield return new WaitForSeconds(_retryFadeIntDuration);
        
        _retryMenu.SetActive(true);
    }

    public void Retry()
    {
        _illustration.gameObject.SetActive(false);
        _nukeImage.gameObject.SetActive(false);
        _retryMenu.SetActive(false);
        _blackImage.DOFade(0, 0);
        _retryBackground.DOFade(0, 0);
        
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        _illustration.gameObject.SetActive(false);
        _nukeImage.gameObject.SetActive(false);
        _retryMenu.SetActive(false);
        _blackImage.DOFade(0, 0);
        _retryBackground.DOFade(0, 0);
        
        SceneManager.LoadScene("Menu");
    }
}
