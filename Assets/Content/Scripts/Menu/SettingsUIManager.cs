using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("External references")]
    [SerializeField] private RSO_VfxVolume _rsoVfxVolume;
    [SerializeField] private RSO_MusicVolume _rsoMusicVolume;
    [SerializeField] private RSE_VictoryLineReached _rseVictoryLineReached;
    [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;
    [SerializeField] private GameObject _buttonParent;
    
    [Header("References")]
    [SerializeField] private GameObject _graphicsParent;
    [SerializeField] private Slider _vfxSlider;
    [SerializeField] private Slider _musicSlider;

    private void OnEnable()
    {
        _rseVictoryLineReached.action += HideAll;
        _rsePlayerDeath.action += HideAll;
    }
    
    private void OnDisable()
    {
        _rseVictoryLineReached.action -= HideAll;
        _rsePlayerDeath.action -= HideAll;
    }

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

    public void HideAll()
    {
        _buttonParent.SetActive(false);
        Hide();
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