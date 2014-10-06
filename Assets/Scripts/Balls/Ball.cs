using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Color realColor { get; private set; }
    public int id;
    public Floor floor;
    public int points = 100;
    public bool specialBall = false;
    float timeCreated = 0;
    float timeToLive = 8f;

    bool deadBall = false;

    private ISpecialBall specialBallType;

    public void SetSpecialBallType(ISpecialBall _specialBallType)
    {
        specialBallType = _specialBallType;
        specialBallType.Initialize(this.timeToLive, this.timeCreated, this.gameObject);
    }

    void Start()
    {
    }

    void Update()
    {
        //Acelerometro
        transform.Translate(Accelerometer.X(), 0, 0);

        if (specialBall && !deadBall)
        {
            this.specialBallType.CheckLifeTimer();
        }
    }

    public void SetNewColor(Color newColor)
    {
        realColor = newColor;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = realColor;
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
        timeCreated = Time.time;

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

    public bool DestroyYourself()
    {
        deadBall = true;
        if (specialBall)
        {
            //Do Bonus Speciallity
            deadBall = specialBallType.MakeSpeciality(this.realColor);
        }

        if (deadBall)
        {
            Destroy(this.gameObject);            
        }
        return deadBall;
    }

    void OnDestroy()
    {
        floor.NotifyBallExitCollision(this);
    }

}
