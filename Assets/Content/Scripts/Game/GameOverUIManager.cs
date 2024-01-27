using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _illustration;
    [SerializeField] private AnimationSettings _gameOverSettings;
    [SerializeField] private RSE_Sound _rseSound;
    [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;

    private void OnEnable() => _rsePlayerDeath.action += PlayGameOver;
    private void OnDisable() => _rsePlayerDeath.action -= PlayGameOver;

    private async void PlayGameOver()
    {
        print($"GAME OVER UI MANAGER: play game over");
        
        // black fade in
        _blackImage.DOFade(1, _gameOverSettings.fadeInDuration).SetEase(Ease.OutCirc);
        await Task.Delay(Mathf.RoundToInt(_gameOverSettings.fadeInDuration * 1000));
        _illustration.gameObject.SetActive(true);

        // slides
        foreach (IntroImage introImage in _gameOverSettings.introImages)
        {
            if (introImage.audioClip != null) _rseSound.Call(TypeSound.VFX, introImage.audioClip, false);
            _illustration.sprite = introImage.image;
            await Task.Delay(Mathf.RoundToInt(introImage.duration * 1000));
        }

        // reset before load game scene at the end
        _illustration.gameObject.SetActive(false);
        _blackImage.DOFade(0, 0);
    }
}
