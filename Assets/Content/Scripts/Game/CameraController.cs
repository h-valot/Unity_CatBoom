using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private float _distanceOffsetY;
    [SerializeField] private float _smoothTimeMove;

    private float _smoothRotate;

    [Header("ScriptableObjects")]
    [SerializeField] private RSE_Input _inputArrow;
    [SerializeField] private RSO_RotationPlayer _rsoPlayerRotation;
    [SerializeField] private RSO_PositionPlayer _rsoPositionPlayer;
    [SerializeField] private PlayerControllerSettings _settingsCamera;

    private void OnEnable()
    {
        _inputArrow.action += RotateCamera;
        _rsoPositionPlayer.OnChanged += FollowPlayer;
    }

    private void OnDisable()
    {
        _inputArrow.action -= RotateCamera;
        _rsoPositionPlayer.OnChanged -= FollowPlayer;
    }

    private void RotateCamera(int inputRotationID) //correspond to -1 for left, 0 for nothing and 1 for right
    {
        _rsoPlayerRotation.value = Mathf.SmoothDamp(_rsoPlayerRotation.value, inputRotationID * _settingsCamera.MaxRotation, ref _smoothRotate, _settingsCamera.SmoothAmount);
        transform.rotation = Quaternion.Euler(0, 0, _rsoPlayerRotation.value);
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, _rsoPositionPlayer.value, _smoothTimeMove).y + _distanceOffsetY, transform.position.z);
    }

}
