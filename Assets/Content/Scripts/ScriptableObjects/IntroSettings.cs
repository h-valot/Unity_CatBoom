using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntroSettings", menuName = "Configs/Intro settings")]
public class IntroSettings : ScriptableObject
{
    [Header("Tweakable values")] 
    public string sceneToLoad;
    
    [Header("Sound")]
    public AudioClip voiceOver;

    [Header("Graphics")] 
    public List<IntroImage> introImages;
}

[System.Serializable]
public class IntroImage
{
    public Sprite image;
    public float duration;
}