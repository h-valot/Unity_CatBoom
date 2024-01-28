using Player;
using UnityEngine;

public class VictoryBox : MonoBehaviour
{
    [Header("External references")]
    [SerializeField] private RSE_VictoryLineReached _rseVictoryLineReached;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<Controller>(out var player)) 
            return;

        player.canMove = false;
        _rseVictoryLineReached.Call();
    }
}
