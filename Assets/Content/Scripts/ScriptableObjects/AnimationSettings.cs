using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSettings", menuName = "Configs/animation settings")]
public class AnimationSettings : ScriptableObject
{
    [Header("Tweakable values")] 
    public string sceneToLoad;
    
    [Header("Graphics")] 
    public float fadeInDuration;
    public List<IntroImage> introImages;
}

[System.Serializable]
public class IntroImage
{
    public AudioClip audioClip;
    public Sprite image;
    public float duration;
}