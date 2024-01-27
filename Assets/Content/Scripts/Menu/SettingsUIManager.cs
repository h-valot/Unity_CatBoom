using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _graphicsParent;
    [SerializeField] private RSO_VfxVolume _rsoVfxVolume;
    [SerializeField] private RSO_MusicVolume _rsoMusicVolume;
    [SerializeField] private Slider _vfxSlider;
    [SerializeField] private Slider _musicSlider;

    public void Show()
    {
        _graphicsParent.SetActive(true);
        _vfxSlider.value = _rsoVfxVolume.value;
        _musicSlider.value = _rsoMusicVolume.value;
    }

    public void Hide()
    {
        _graphicsParent.SetActive(false);
    }
    
    public void OnChangeVfxVolume(float value)
    {
        _rsoVfxVolume.value = value;
    }

    public void OnChangeMusicVolume(float value)
    {
        _rsoMusicVolume.value = value;
    }
}