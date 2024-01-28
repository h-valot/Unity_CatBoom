using Player;
using TMPro;
using UnityEngine;

public class StartingBox : MonoBehaviour
{
    [Header("External references")] 
    [SerializeField] private TextMeshProUGUI _timerTM;
    
    private float _time;
    private bool _timerOn;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<Controller>(out var player)) return;

        _timerOn = true;
    }

    private void Update()
    {
        if (!_timerOn) return;
        
        _time += Time.deltaTime;
        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);
        _timerTM.text = $"{minutes:00} : {seconds:00}";
    }
}
