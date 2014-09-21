using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Color realColor { get; private set; }
    public int id;
    public Floor floor;

    void Start()
    {
    }

    void Update()
    {
        //Acelerometro
        transform.Translate(Accelerometer.X(), 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Floor")
        {
            Color ballColor = this.GetComponent<SpriteRenderer>().color;

            floor.NotifyBallCollision(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Floor")
            floor.NotifyBallExitCollision(this);
    }

    public void Reset()
    {
        transform.position = new Vector2(Random.Range(-2, 2), Random.Range(8, 14));
        float scale = Random.Range(0.4f, 0.8f);
        transform.localScale = new Vector3(scale, scale);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float r = Random.value;
        if (r < 0.25)
        {
            realColor = BallColor.Red;
        }
        else if (r < 0.5)
        {
            realColor = BallColor.Yellow;
        }
        else if (r < 0.75)
        {
            realColor = BallColor.Blue;
        }
        else if (r < 0.833333)
        {
            realColor = BallColor.Purple;
        }
        else if (r < 0.9166666)
        {
            realColor = BallColor.Green;
        }
        else
        {
            realColor = BallColor.Orange;
        }
        renderer.color = realColor;
    }
}
