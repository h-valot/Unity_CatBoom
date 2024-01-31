using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private StartingBox _startingBox;
    [SerializeField] private VictoryBox _victoryBox;
    [SerializeField] private TextMeshProUGUI _fillTM;
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _uiParent;

    [Header("External references")] 
    [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
    [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;
    [SerializeField] private RSE_VictoryLineReached _rseVictoryLineReached;
    
    private float _totalDistance;
    private float _remainingDistance;

    private void OnEnable()
    {
        _rsePlayerDeath.action += HideHUD;
        _rseVictoryLineReached.action += HideHUD;
    }
    
    private void OnDisable()
    {
        _rsePlayerDeath.action -= HideHUD;
        _rseVictoryLineReached.action -= HideHUD;
    }

    private void HideHUD()
    {
        _uiParent.SetActive(false);
    }

    private void Update()
    {
        _totalDistance = Mathf.Abs(_victoryBox.transform.position.y - _startingBox.transform.position.y);
        _remainingDistance = Mathf.Abs(_totalDistance - _rsoPositionPlayer.value.y);
        
        float fillAmount = (_remainingDistance / _totalDistance) - 1;
        if (fillAmount > 1) fillAmount = 1;
        if (fillAmount < 0) fillAmount = 0;
        _fillImage.fillAmount = fillAmount;
        _fillTM.text = $"{Mathf.RoundToInt(fillAmount * 100)} %";
    }
}