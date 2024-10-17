using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] spriteArray;
    [SerializeField]
    public float sceneWidth = 13.0f;
    [SerializeField]
    public float sceneHeight = 10.0f;
}
