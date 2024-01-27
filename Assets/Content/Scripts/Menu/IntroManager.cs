using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Image _image;
    [SerializeField] private IntroSettings _introSettings;
    
    public async void PlayIntro()
    {
        _image.gameObject.SetActive(true);
        // _introSettings.voiceOver.Play;

        foreach (IntroImage introImage in _introSettings.introImages)
        {
            _image.sprite = introImage.image;
            await Task.Delay(Mathf.RoundToInt(introImage.duration) * 1000);
        }

        SceneManager.LoadScene(_introSettings.sceneToLoad);
    }
}
