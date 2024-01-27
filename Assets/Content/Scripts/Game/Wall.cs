using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Tweakable values")] 
    [SerializeField] private bool _onLeft;
    
    [Header("References")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private WallSettings _wallSettings;
    
    private void Start()
    {
        _spriteRenderer.sprite = _onLeft
            ? _wallSettings.leftWalls[Random.Range(0, _wallSettings.leftWalls.Count - 1)]
            : _wallSettings.rightWalls[Random.Range(0, _wallSettings.rightWalls.Count - 1)];
    }
}