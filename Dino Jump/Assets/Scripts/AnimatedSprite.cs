using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;
        if (frame == sprites.Length)
        {
            //returns to the first sprite
            frame = 0;
        }
        //makes sure that no error msg will flag if no sprites has been initialize
        if (frame >= 0 && frame < sprites.Length) 
        {
            spriteRenderer.sprite = sprites[frame];
        }
        
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed); 
    }
}
 