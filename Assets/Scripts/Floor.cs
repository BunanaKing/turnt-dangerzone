using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour
{
    public GameObject ball_go;
    public int ballDestroyedCounter = 0;
    public int ballCreatedCounter = 0;
    public int[] spawnLevels;
    private int currentIncrement = 0;
    private int amountOfBallsToSpawn = 1;
    public int initAmountOfBalls = 5;
    //public bool changingColor = false;
    private SpriteRenderer spriteRenderer;
    private Timer timer;
    private int ballsIdIndex = 1;
    private Dictionary<int, Color> ballsColliding = new Dictionary<int, Color>();
    bool existsError = false;
    private bool ended = false;

    public int colorButtonsSum = 0;
    public float timeUntilError = 0f;

    // Spam
    public SpamBarScript spam;

    // Use this for initialization
    void Start()
    {
        spam = FindObjectOfType<SpamBarScript>();
        spam.spamFull = SpamFull;

        ballCreatedCounter += initAmountOfBalls;
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = GetComponent<Timer>();

        for (int i = 0; i < initAmountOfBalls; i++)
        {
            GameObject new_ball = (GameObject)GameObject.Instantiate(ball_go);
            Ball ballScript = new_ball.GetComponent<Ball>();
            ballScript.id = ballsIdIndex++;
            ballScript.floor = this;
            ballScript.Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.enabled)
        {
            ended = false;
        }

        Color aux = Color.black;
        if (!ended)
        {

            if (timeUntilError > 0)
                timeUntilError -= Time.time;
            if (timeUntilError <= 0)
            {
                if (existsError)
                {
                    existsError = false;
                    IncrementSpamMeter();
                }
            }

            switch (colorButtonsSum)
            {
                case 1:
                    aux = Color.red;
                    break;
                case 33:
                    aux = Color.yellow;
                    break;
                case 77:
                    aux = Color.blue;
                    break;
                case 34:
                    aux = new Color(255 / 255F, 120 / 255F, 0F);
                    break;
                case 78:
                    aux = new Color(178 / 255F, 0F, 255 / 255F);
                    break;
                case 110:
                    aux = Color.green;
                    break;
                case 111:
                    aux = Color.white;
                    break;
            }

        }
        spriteRenderer.color = aux;

    }

    public void IncrementSpamMeter()
    {
        spam.Increase();
    }

    public void AddColor(Button.BallColor color)
    {
        switch (color)
        {
            case Button.BallColor.Red:
                colorButtonsSum += 1;
                break;
            case Button.BallColor.Blue:
                colorButtonsSum += 77;
                break;
            case Button.BallColor.Yellow:
                colorButtonsSum += 33;
                break;
        }

        Color aux = Color.black;
        switch (colorButtonsSum)
        {
            case 1:
                aux = Color.red;
                break;
            case 33:
                aux = Color.yellow;
                break;
            case 77:
                aux = Color.blue;
                break;
            case 34:
                aux = new Color(255 / 255F, 120 / 255F, 0F);
                break;
            case 78:
                aux = new Color(178 / 255F, 0F, 255 / 255F);
                break;
            case 110:
                aux = Color.green;
                break;
            case 111:
                aux = Color.white;
                break;
        }

        timeUntilError = 0.2f;
        if (IsColorColliding(aux))
        {
            if (existsError)
            {
                existsError = false;
            }
        }
        else
        {
            existsError = true;
        }
    }

    private bool IsColorColliding(Color barColor)
    {
        foreach (Color item in ballsColliding.Values)
        {
            if (barColor == item)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveColor(Button.BallColor color)
    {
        switch (color)
        {
            case Button.BallColor.Red:
                colorButtonsSum -= 1;
                break;
            case Button.BallColor.Blue:
                colorButtonsSum -= 77;
                break;
            case Button.BallColor.Yellow:
                colorButtonsSum -= 33;
                break;
        }
    }

    public void NotifyBallCollision(int id, Color ballColor)
    {
        //if (!ended)
        //{            
        if (!ballsColliding.ContainsKey(id))
        {
            ballsColliding[id] = ballColor;
        }
        //}
    }

    public void NotifyBallExitCollision(int id)
    {
        //if (!ended)
        //{            
        if (ballsColliding.ContainsKey(id))
        {
            ballsColliding.Remove(id);
            //Debug.Log("Remuevo del dicc, iD: " + id);
        }
        //}
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (!ended)
        {
            if (collider.gameObject.tag == "Ball")
            {
                Ball ball = collider.gameObject.GetComponent<Ball>();

                // && esto quiere decir que esta cambiando de color
                if (ball.GetComponent<SpriteRenderer>().color == spriteRenderer.color && timeUntilError <= 0)
                {
                    Destroy(collider.gameObject);
                    ballsColliding.Remove(ball.id);
                    ballDestroyedCounter++;
                    ballCreatedCounter--;

                    if (ballCreatedCounter < 40)
                    {
                        if (currentIncrement < spawnLevels.Length - 1)
                        {
                            if (ballDestroyedCounter > spawnLevels[currentIncrement])
                            {
                                currentIncrement++;
                                amountOfBallsToSpawn++;
                            }
                        }

                        for (int i = 0; i < amountOfBallsToSpawn; i++)
                        {
                            GameObject new_ball = (GameObject)GameObject.Instantiate(ball_go);
                            Ball ballScript = new_ball.GetComponent<Ball>();
                            ballScript.id = ballsIdIndex++;
                            ballScript.floor = this;
                            new_ball.GetComponent<Ball>().Reset();
                        }
                        ballCreatedCounter += amountOfBallsToSpawn;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(120, 20, 100, 30), ballDestroyedCounter + " balls");
    }

    private void SpamFull()
    {
        Debug.Log("Spam full!");
    }
}
