using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private RSO_RotationPlayer _rsoRotationPlayer;
        [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
        [SerializeField] private RSE_PlayerDeath _rsePlayerDeath;
        [SerializeField] private GameSettings _gameSettings;
        
        [Header("References")]
        [SerializeField] private GameObject _graphicsParent;
        public Rigidbody2D rigidbody2D;
        
        [Header("Audio")]
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private AudioClip[] _cancelledClips;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private RSE_Sound _rseSoundPlay;
        [SerializeField] private RSE_Sound _rseSoundStop;
        
        [Header("Damaged")] 
        [SerializeField] private SpriteRenderer _catSpriteRenderer;
        [SerializeField] private Sprite _catAlmostDead, _catAlive;
        [SerializeField] private float _damageAnimationDuration;
        
        [Header("Killed")]
        [SerializeField] private GameObject _explosionGameObject;
        [SerializeField] private float _preAnimationDuration;
        

        [Header("Public variables")]
        public bool onGround;
        public bool nearPlatform;
        public bool canMove = true;
        
        public void OnEnable() =>  _rsoRotationPlayer.OnChanged += ApplyDirectionGravity;
        public void OnDisable() => _rsoRotationPlayer.OnChanged -= ApplyDirectionGravity;
        
        private void ApplyDirectionGravity()
        {
            if (!canMove) return;
            
            Physics2D.gravity = new Vector2(_rsoRotationPlayer.value * _gameSettings.speedMultiplier, -_gameSettings.fallingSpeed);
        }
        
        private void FixedUpdate()
        {
            if (!canMove) return;
            
            _rsoPositionPlayer.value = transform.position;
            // if (onGround && (rigidbody2D.velocity.x < -0.6f || rigidbody2D.velocity.x > 0.6f)) _rseSoundPlay.Call(TypeSound.VFX, _audioRolling, true);
            // else if (!onGround || (onGround && (rigidbody2D.velocity.x < -0.2f || rigidbody2D.velocity.x > 0.2f))) _rseSoundStop.Call(TypeSound.VFX, _audioRolling, false);
            UpdateRigidbody();
        }
        
        private void UpdateRigidbody()
        {
            rigidbody2D.drag = _gameSettings.drag;
            rigidbody2D.mass = _gameSettings.mass;
        }

        public async void GetDamaged()
        {
            if (!canMove) return;
            
            if (!onGround) _rseSoundPlay.Call(TypeSound.VFX, _audioClips[Random.Range(0, _audioClips.Length - 1)], false);
            
            onGround = true;
            nearPlatform = true;
            _catSpriteRenderer.sprite = _catAlmostDead;
            
            await Task.Delay(Mathf.RoundToInt(_damageAnimationDuration * 1000));
            _catSpriteRenderer.sprite = _catAlive;
        }
        
        public async void HandleDeath()
        {
            if (!canMove) return;
            
            foreach (AudioClip audioClip in _cancelledClips)
                _rseSoundStop.Call(TypeSound.Background, audioClip, false);
            
            _rseSoundPlay.Call(TypeSound.VFX, _deathSound, false);
            
            Time.timeScale = 0;
            _explosionGameObject.SetActive(true);
            await Task.Delay(Mathf.RoundToInt(_preAnimationDuration * 1000));
            Time.timeScale = 1;
            
            _rsePlayerDeath.Call();
            
            rigidbody2D.velocity = Vector2.zero;
            canMove = false;
        }
    }
}