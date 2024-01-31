using System.Collections;
using Player;
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
            StartCoroutine(StartTimer(player));
            player.nearPlatform = false;
        }

        private IEnumerator StartTimer(Controller player)
        {
            yield return new WaitForSeconds(_gameSettings.onAirThreshold);
            if (!player.nearPlatform) player.onGround = false;
        }
    }
}