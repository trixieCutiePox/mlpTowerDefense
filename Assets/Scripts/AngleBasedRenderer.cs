using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBasedRenderer : MonoBehaviour
{
    public Sprite[] sprites;
    public int activeSprite;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      spriteRenderer.sprite = sprites[activeSprite];
    }

    public void SetAngle(float angle) {
      Debug.Log(angle);
      float halfStep = 360 / 2 / sprites.Length;
      Debug.Log(halfStep);
      int index = Mathf.FloorToInt((angle / halfStep + 1) / 2);
      activeSprite = index;
      if(activeSprite == sprites.Length){
        activeSprite = 0;
      }
    }
}
