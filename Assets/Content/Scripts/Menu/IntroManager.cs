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
    [SerializeField] private IntroSettings _introSettings;
    
    public async void PlayIntro()
    {
        // black fade in
        _blackImage.DOFade(1, _introSettings.fadeInDuration).SetEase(Ease.OutCirc);
        await Task.Delay(Mathf.RoundToInt(_introSettings.fadeInDuration) * 1000);
        
        _illustration.gameObject.SetActive(true);
        // _introSettings.voiceOver.Play;

        // slides
        foreach (IntroImage introImage in _introSettings.introImages)
        {
            _illustration.sprite = introImage.image;
            await Task.Delay(Mathf.RoundToInt(introImage.duration) * 1000);
        }

        // reset before load game scene at the end
        _illustration.gameObject.SetActive(false);
        _blackImage.DOFade(0, 0);
        SceneManager.LoadScene(_introSettings.sceneToLoad);
    }
}