using Player;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Platform : MonoBehaviour
    {
        [Header("Tweakable values")]
        [SerializeField] private float _velocityThreshold;

        [Header("References")]
        [SerializeField] private GameSettings _gameSettings;

        [Header("Data")]
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private RSE_Sound _rseSoundPlay;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Controller>(out var player)) return;


            if (!player.onGround) _rseSoundPlay.Call(TypeSound.VFX, _audioClips[UnityEngine.Random.Range(0, _audioClips.Length)], false);
            player.onGround = true;
            player.nearPlatform = true;

            if (-player.GetComponent<Rigidbody2D>().velocity.y > _velocityThreshold)
            {
                player.HandleDeath();
                return;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Controller>(out var player)) return;
            StartTimer(player);
            player.nearPlatform = false;
        }

        private async void StartTimer(Controller player)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * _gameSettings.onAirThreshold));
            if (!player.nearPlatform) player.onGround = false;
        }
    }
}