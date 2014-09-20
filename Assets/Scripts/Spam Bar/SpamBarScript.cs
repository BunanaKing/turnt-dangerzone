using UnityEngine;
using System.Collections;

public delegate void SpamFull();
public delegate void SpamEmpty();

public class SpamBarScript : MonoBehaviour
{
    public SpamFull spamFull;
    public SpamEmpty spamEmpty;

    private Transform foregroundTexture;
    private SpriteRenderer foregroundTextureRender;
    private GameObject spamIconGO;

    private Vector2 spamScale;
    public float animateUpStep = 0.01f;
    public float moveDownSpeed = 0.99999f;
    public float step = 0.2f;
    public float maxValue = 1f;
    private float addValue = 0f;
    private float maxXScale = 0.91f;
    private bool isSpamFull = false;
    public float leftPosition = -1.45f;

    void Start()
    {
        foregroundTexture = GameObject.FindWithTag("spam_bar").transform;
        foregroundTextureRender = foregroundTexture.GetComponent<SpriteRenderer>();

        spamIconGO = GameObject.FindWithTag("spam_icon");
        spamIconGO.SetActive(false);

        Reset();
    }

    void Update()
    {
        if (!isSpamFull)
        {
            if (addValue > 0)
            {
                spamScale.x += animateUpStep;
                addValue -= animateUpStep;
            }
            else if (spamScale.x > 0)
            {
                spamScale.x -= moveDownSpeed;
            }

            if (spamScale.x >= maxXScale)
            {
                spamScale.x = maxXScale;
                isSpamFull = true;
                spamIconGO.SetActive(true);
                spamFull();
            }
        }
        else
        {
            if (spamScale.x > 0)
            {
                //Factor de enfriamiento
                spamScale.x -= moveDownSpeed;
            }
            else if (spamScale.x <= 0)
            {
                spamIconGO.SetActive(false);
                spamScale.x = 0;
                isSpamFull = false;
                spamEmpty();
            }
        }

        //Escalar la barra de Spam
        spamScale.x = Mathf.Clamp(spamScale.x, 0, 1);
        foregroundTexture.localScale = spamScale;

        //Corregir posicion izquierda
        float barWidth = foregroundTextureRender.sprite.bounds.size.x * foregroundTexture.localScale.x;
        Vector3 newPos = foregroundTexture.transform.localPosition;
        newPos.x = leftPosition + barWidth / 2;
        foregroundTexture.transform.localPosition = newPos;
    }

    public void Reset()
    {
        spamScale = new Vector2(0, 1);
    }

    public void Increase()
    {
        if (spamScale.x < maxXScale)
            addValue += step;
    }
}