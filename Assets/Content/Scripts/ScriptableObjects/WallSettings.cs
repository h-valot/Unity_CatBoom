using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallSettings", menuName = "Configs/Wall settings")]
public class WallSettings : ScriptableObject
{
    [Header("Graphics")]
    public List<Sprite> rightWalls;
    public List<Sprite> leftWalls;
}