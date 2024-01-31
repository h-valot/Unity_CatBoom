using System.Collections;
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
        [SerializeField] private Sprite _catDead;
        [SerializeField] private float _preAnimationDuration;
        

        [Header("Public variables")]
        public bool onGround;
        public bool nearPlatform;
        public bool canMove = true;
        
        public void OnEnable() =>  _rsoRotationPlayer.OnChanged += ApplyDirectionGravity;
        public void OnDisable() => _rsoRotationPlayer.OnChanged -= ApplyDirectionGravity;
        
        private void ApplyDirectionGravity()
        {
            if (!canMove) 
                return;
            
            Physics2D.gravity = new Vector2(_rsoRotationPlayer.value * _gameSettings.speedMultiplier, -_gameSettings.fallingSpeed);
        }
        
        private void FixedUpdate()
        {
            if (!canMove) 
                return;
            
            _rsoPositionPlayer.value = transform.position;
            
            rigidbody2D.drag = _gameSettings.drag;
            rigidbody2D.mass = _gameSettings.mass;
        }

        public void GetDamaged()
        {
            if (!canMove) 
                return;
            
            StartCoroutine(AnimateDamage());
        }

        private IEnumerator AnimateDamage()
        {
            if (!onGround) 
                _rseSoundPlay.Call(TypeSound.VFX, _audioClips[Random.Range(0, _audioClips.Length - 1)], false);
            
            onGround = true;
            nearPlatform = true;
            _catSpriteRenderer.sprite = _catAlmostDead;
            yield return new WaitForSeconds(_damageAnimationDuration);
            _catSpriteRenderer.sprite = _catAlive;
        }
        
        public void HandleDeath()
        {
            if (!canMove) 
                return;

            StartCoroutine(AnimateDeath());
        }

        private IEnumerator AnimateDeath()
        {
            foreach (AudioClip audioClip in _cancelledClips)
                _rseSoundStop.Call(TypeSound.Background, audioClip, false);
            
            canMove = false;
            rigidbody2D.velocity = Vector2.zero;
            _rseSoundPlay.Call(TypeSound.VFX, _deathSound, false);
            _catSpriteRenderer.sprite = _catDead;
            _explosionGameObject.SetActive(true);

            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(_preAnimationDuration);
            Time.timeScale = 1;
            _rsePlayerDeath.Call();
        }
    }
}