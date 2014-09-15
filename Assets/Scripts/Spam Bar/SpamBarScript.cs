using UnityEngine;
using System.Collections;

public delegate void SpamFull();

public class SpamBarScript : MonoBehaviour
{
    public SpamFull spamFull;

    private Transform foregroundTexture;
    private Vector2 spamScale;
    public float animateUpStep = 0.01f;
    public float moveDownSpeed = 0.99999f;
    public float step = 0.2f;
    public float maxValue = 1f;
    private float addValue = 0f;

    void Start()
    {
        foregroundTexture = GameObject.FindWithTag("spam_bar").transform;

        Reset();
    }

    void Update()
    {
        if (addValue > 0)
        {
            spamScale.x += animateUpStep;
            addValue -= animateUpStep;
        }

        if (spamScale.x > 0)
        {
            spamScale.x -= moveDownSpeed;
        }

        spamScale.x = Mathf.Clamp(spamScale.x, 0, 1);
        foregroundTexture.localScale = spamScale;

        if (spamScale.x == 1)
        {
            spamFull();
        }
    }

    public void Reset()
    {
        spamScale = new Vector2(0, 1);
    }

    public void Increase()
    {
        addValue += step;
    }
}