using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RSO_VfxVolume _rsoVfxVolume;
    [SerializeField] private RSO_MusicVolume _rsoMusicVolume;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _vfxSlider;
    [SerializeField] private GameObject _graphicsParent;

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
    
    public void UpdateVfxVolume()
    {
        // updates rso music volume
        _rsoVfxVolume.value = _vfxSlider.value;
    }

    public void UpdateMusicVolume()
    {
        // updates rso sound volume
        _rsoMusicVolume.value = _musicSlider.value;
    }
}