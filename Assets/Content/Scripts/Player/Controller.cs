using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RSO_RotationPlayer _rsoRotationPlayer;
        [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
        [SerializeField] private RSE_Sound _rseSoundPlay;
        [SerializeField] private RSE_Sound _rseSoundStop;
        [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private GameObject _graphicsParent;

        [SerializeField] private AudioClip _audioRolling;

        public Rigidbody2D rigidbody2D;
        public bool onGround;
        public bool nearPlatform;
        
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
            //if (onGround && (rigidbody2D.velocity.x < -0.6f || rigidbody2D.velocity.x > 0.6f)) _rseSoundPlay.Call(TypeSound.VFX, _audioRolling, true);
            //else if (!onGround || (onGround && (rigidbody2D.velocity.x < -0.2f || rigidbody2D.velocity.x > 0.2f))) _rseSoundStop.Call(TypeSound.VFX, _audioRolling, false);
            UpdateRigidbody();
        }
    
        public void HandleDeath()
        {
            
            
            _rsePlayerDeath.Call();
            Destroy(_graphicsParent);
        }
    }
}