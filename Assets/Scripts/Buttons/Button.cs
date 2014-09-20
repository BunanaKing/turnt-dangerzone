using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public enum BallColor { Red, Yellow, Blue, Green, Orange, Purple }

    public BallColor color;

    public Sprite buttonImage;
    public Sprite pressedButtonImage;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    }

    public void PressButton()
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.sprite = pressedButtonImage;
    }

    public void ReleaseButton()
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.sprite = buttonImage;
    }
}
