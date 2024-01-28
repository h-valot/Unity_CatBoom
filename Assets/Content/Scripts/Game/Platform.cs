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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Controller>(out var player)) return;

            player.GetDamaged();

            if (-player.rigidbody2D.velocity.y > _velocityThreshold)
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