using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SettingsPlayer",menuName = "ScriptableObjects/SettingsPlayer")]
public class PlayerControllerSettings : ScriptableObject
{
    [SerializeField] private float smoothAmount;
    public float SmoothAmount { get => smoothAmount; private set => smoothAmount = value;}

    [SerializeField] private float maxRotation;
    public float MaxRotation { get => maxRotation; private set => maxRotation = value; }
}
