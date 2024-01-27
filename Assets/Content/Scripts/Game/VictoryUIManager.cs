using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
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
    

    private void OnEnable() => _rseVictoryLineReached.action += PlayVictory;
    private void OnDisable() => _rseVictoryLineReached.action -= PlayVictory;
    
    private async void PlayVictory()
    {
        // black fade in
        _blackImage.DOFade(1, _victorySettings.fadeInDuration).SetEase(Ease.OutCirc);
        await Task.Delay(Mathf.RoundToInt(_victorySettings.fadeInDuration * 1000));
        
        _illustration.gameObject.SetActive(true);

        // slides
        foreach (var introImage in _victorySettings.introImages)
        {
            if (introImage.audioClip != null) _rseSound.Call(TypeSound.VFX, introImage.audioClip, false);
            _illustration.sprite = introImage.image;
            await Task.Delay(Mathf.RoundToInt(introImage.duration * 1000));
        }
        // black fade in 
        _menuBackground.DOFade(_menuBackgroundFadeAmount, _menuFadeOutDuration);
        await Task.Delay(Mathf.RoundToInt(_menuFadeOutDuration * 1000));
        _menuGameObject.SetActive(true);
        
        // plane waiting time
        await Task.Delay(Mathf.RoundToInt(_durationBeforePlane * 1000));
        
        // plane translation
        _plane.transform.DOMove(_endingPoint.transform.position, _translationDuration);
        await Task.Delay(Mathf.RoundToInt(_translationDuration * 1000));
        
        Application.Quit();
    }
}