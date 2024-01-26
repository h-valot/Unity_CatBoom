using UnityEngine;

namespace Game
{
    public class Platform : MonoBehaviour
    {
        [Header("Tweakable values")]
        [SerializeField] private float _velocityThreshold;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent<Player>(out var player)) return;
            if (-player.rigidbody2D.velocity.y < _velocityThreshold) return;
            
            player.HandleDeath();
        }
    }
}