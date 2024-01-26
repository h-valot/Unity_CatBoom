using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSO_RotationPlayer _rsoRotationPlayer;
    [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
    [SerializeField] private float _speedFall;

    public void OnEnable() =>  _rsoRotationPlayer.OnChanged += ApplyDirectionGravity;
    public void OnDisable() => _rsoRotationPlayer.OnChanged -= ApplyDirectionGravity;
    private void ApplyDirectionGravity() => Physics2D.gravity = new Vector2(_rsoRotationPlayer.value,-_speedFall);
    private void FixedUpdate() => _rsoPositionPlayer.value = transform.position;
}