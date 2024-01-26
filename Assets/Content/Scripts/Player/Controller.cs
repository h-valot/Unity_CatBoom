using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RSO_RotationPlayer _rsoRotationPlayer;
        [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private GameObject _graphicsParent;

        public Rigidbody2D rigidbody2D;
        
        public void OnEnable() =>  _rsoRotationPlayer.OnChanged += ApplyDirectionGravity;
        public void OnDisable() => _rsoRotationPlayer.OnChanged -= ApplyDirectionGravity;
        private void ApplyDirectionGravity()
        {
            Physics2D.gravity = new Vector2(_rsoRotationPlayer.value * _gameSettings.speedMultiplier, -_gameSettings.fallingSpeed);
        }

        private void UpdateRigidbody()
        {
            rigidbody2D.drag = _gameSettings.drag;
            rigidbody2D.mass = _gameSettings.mass;
        }
        
        private void FixedUpdate()
        {
            _rsoPositionPlayer.value = transform.position;
            UpdateRigidbody();
        }
    
        public void HandleDeath()
        {
            Destroy(_graphicsParent);
        }
    }
}