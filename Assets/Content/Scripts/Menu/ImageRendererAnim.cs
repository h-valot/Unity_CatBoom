using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRendererAnim : MonoBehaviour
{
    public float duration;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    private int index = 0;
    private float timer = 0;

    private void Awake()
    {
        image.sprite = sprites[index];
    }

    private void Update()
    {
        if ((timer += Time.deltaTime) >= (duration / sprites.Length))
        {
            timer = 0;
            image.sprite = sprites[index];
            index = (index + 1) % sprites.Length;
        }
    }
}
