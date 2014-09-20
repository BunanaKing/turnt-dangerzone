using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Button.BallColor color;
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

            floor.NotifyBallCollision(this.id, ballColor);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Floor")
            floor.NotifyBallExitCollision(this.id);
    }

    public void Reset()
    {
        transform.position = new Vector2(Random.Range(-2, 2), Random.Range(8, 14));
        float scale = Random.Range(0.2f, 1f);
        transform.localScale = new Vector3(scale, scale);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float r = Random.value;
        if (r < 0.25)
        {
            color = Button.BallColor.Red;
            renderer.color = Color.red;
        }
        else if (r < 0.5)
        {
            color = Button.BallColor.Yellow;
            renderer.color = Color.yellow;
        }
        else if (r < 0.75)
        {
            color = Button.BallColor.Blue;
            renderer.color = Color.blue;
        }
        else if (r < 0.833333)
        {
            color = Button.BallColor.Purple;
            renderer.color = new Color(178 / 255F, 0F, 255 / 255F);
        }
        else if (r < 0.9166666)
        {
            color = Button.BallColor.Green;
            renderer.color = Color.green;
        }
        else
        {
            color = Button.BallColor.Orange;
            renderer.color = new Color(255 / 255F, 120 / 255F, 0F);
        }
    }
}
