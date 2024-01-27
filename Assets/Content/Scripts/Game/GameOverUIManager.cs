using System.Threading.Tasks;
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
    

    private void OnEnable() => _rsePlayerDeath.action += PlayGameOver;
    private void OnDisable() => _rsePlayerDeath.action -= PlayGameOver;

    private async void PlayGameOver()
    {
        // black fade in
        _blackImage.DOFade(1, _gameOverSettings.fadeInDuration).SetEase(Ease.OutCirc);
        await Task.Delay(Mathf.RoundToInt(_gameOverSettings.fadeInDuration * 1000));
        _illustration.gameObject.SetActive(true);

        // cat's face
        _illustration.sprite = _gameOverSettings.introImages[0].image;
        if (_gameOverSettings.introImages[0].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[0].audioClip, false);
        await Task.Delay(Mathf.RoundToInt(_gameOverSettings.introImages[0].duration * 1000));
        
        // flash bang
        _illustration.sprite = _gameOverSettings.introImages[1].image;
        if (_gameOverSettings.introImages[1].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[1].audioClip, false);
        await Task.Delay(Mathf.RoundToInt(_gameOverSettings.introImages[1].duration * 1000));
        
        // display nuke
        if (_gameOverSettings.introImages[2].audioClip != null) _rseSound.Call(TypeSound.VFX, _gameOverSettings.introImages[2].audioClip, false);
        _nukeImage.gameObject.SetActive(true);
        
        // fade out flash bang
        _illustration.DOFade(0, _flashBangFadeOutDuration).SetEase(Ease.OutCirc);
        
        // display nuke for x seconds
        await Task.Delay(Mathf.RoundToInt(_gameOverSettings.introImages[2].duration * 1000));

        _retryBackground.DOFade(_retryBackgroundFadeAmount, _retryFadeIntDuration);
        await Task.Delay(Mathf.RoundToInt(_retryFadeIntDuration * 1000));
        
        _retryMenu.SetActive(true);
    }

    public void Retry()
    {
        _illustration.gameObject.SetActive(false);
        _nukeImage.gameObject.SetActive(false);
        _retryMenu.SetActive(false);
        _blackImage.DOFade(0, 0);
        _retryBackground.DOFade(0, 0);
        
        SceneManager.LoadScene("Content/Scenes/LevelDesignTestLoic");
    }

    public void Exit()
    {
        _illustration.gameObject.SetActive(false);
        _nukeImage.gameObject.SetActive(false);
        _retryMenu.SetActive(false);
        _blackImage.DOFade(0, 0);
        _retryBackground.DOFade(0, 0);
        
        SceneManager.LoadScene("Content/Scenes/TestMenuHugo");
    }
}
