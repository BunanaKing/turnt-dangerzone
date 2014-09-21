using UnityEngine;
using System.Collections.Generic;

public class Floor : MonoBehaviour
{
    public delegate void ScoreAdded(int points);
    public static event ScoreAdded OnScoreAdding;

    public GameObject ball_go;
    public int ballDestroyedCounter = 0;
    public int ballCreatedCounter = 0;
    public int maxAmountOfBalls = 30;
    public int[] spawnLevels;

    public int initAmountOfBalls = 5;

    private int currentIncrement = 0;
    private int amountOfBallsToSpawn = 1;

    private SpriteRenderer spriteRenderer;
    private int ballsIdIndex = 1;
    private List<Ball> ballsColliding;
    private List<Ball> ballsToKill;
    bool existsError = false;
    bool spamOverHeated = false;
    private bool ended = false;

    public int colorButtonsSum = 0;
    public float timeUntilError = 0f;

    // Spam
    public SpamBarScript spam;

    void Start()
    {
        spam = FindObjectOfType<SpamBarScript>();
        spam.spamFull = SpamFull;
        spam.spamEmpty = SpamEmpty;

        ballsColliding = new List<Ball>();
        ballsToKill = new List<Ball>();
        ballCreatedCounter += initAmountOfBalls;
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < initAmountOfBalls; i++)
        {
            GameObject new_ball = (GameObject)GameObject.Instantiate(ball_go);
            Ball ballScript = new_ball.GetComponent<Ball>();
            ballScript.id = ballsIdIndex++;
            ballScript.floor = this;
            ballScript.Reset();
        }
    }

    void Update()
    {
        if (!ended)
        {
            if (timeUntilError > 0)
            {
                timeUntilError -= Time.time;
            }

            if (timeUntilError <= 0)
            {
                if (existsError)
                {
                    existsError = false;
                    IncrementKarmaMeter();
                }
            }
        }
    }

    public void IncrementKarmaMeter()
    {
        spam.Increase();
    }

    public void AddColor(Button.ButtonColor color)
    {
        if (spamOverHeated)
            return;

        UpdateColor(color, true);

        KillBalls();
    }

    public void RemoveColor(Button.ButtonColor color)
    {
        if (spamOverHeated)
            return;

        UpdateColor(color, false);

        if (colorButtonsSum != 0)
            KillBalls();
    }

    private void UpdateColor(Button.ButtonColor color, bool add)
    {
        switch (color)
        {
            case Button.ButtonColor.Red:
                colorButtonsSum += add ? 1 : -1;
                break;
            case Button.ButtonColor.Blue:
                colorButtonsSum += add ? 77 : -77;
                break;
            case Button.ButtonColor.Yellow:
                colorButtonsSum += add ? 33 : -33;
                break;
        }
        spriteRenderer.color = BallColor.ColorByValue(colorButtonsSum);
    }

    private bool IsColorColliding(Color barColor)
    {
        foreach (Ball ball in ballsColliding)
        {
            if (ball.realColor == barColor)
            {
                return true;
            }
        }
        return false;
    }

    public void NotifyBallCollision(Ball ball)
    {
        if (!ended)
        {
            if (!ballsColliding.Contains(ball))
            {
                ballsColliding.Add(ball);
                TryKillBall(ball);
            }
        }
    }

    public void NotifyBallExitCollision(Ball ball)
    {
        if (!ended)
            ballsColliding.Remove(ball);
    }

    private void KillBalls()
    {
        if (!ended)
        {
            if (colorButtonsSum == 0)
            {
                timeUntilError = 0.2f;
                if (existsError)
                {
                    IncrementKarmaMeter();
                    existsError = false;
                }
            }

            if (CanKillBalls())
            {
                bool anyKill = false;
                ballsToKill.Clear();
                foreach (Ball ball in ballsColliding)
                {
                    if (ball.realColor == spriteRenderer.color)
                    {
                        anyKill = true;

                        ballsToKill.Add(ball);
                        ballDestroyedCounter++;
                        ballCreatedCounter--;

                        if (OnScoreAdding != null)
                            OnScoreAdding(100);

                        CreateNewBalls();
                    }
                }
                foreach (Ball ball in ballsToKill)
                {
                    Destroy(ball.gameObject);
                    ballsColliding.Remove(ball);
                }

                if (anyKill)
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
        }
    }
    private bool CanKillBalls()
    {
        return timeUntilError <= 0;
    }

    private void TryKillBall(Ball ball)
    {
        if (!ended && CanKillBalls() && ball.realColor == spriteRenderer.color)
        {
            ballsToKill.Add(ball);
            ballDestroyedCounter++;
            ballCreatedCounter--;

            if (OnScoreAdding != null)
                OnScoreAdding(100);

            CreateNewBalls();

            Destroy(ball.gameObject);
            ballsColliding.Remove(ball);
        }
    }

    private void CreateNewBalls()
    {
        if (ballCreatedCounter < maxAmountOfBalls)
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

    void OnGUI()
    {
        GUI.Label(new Rect(120, 20, 100, 30), ballDestroyedCounter + " balls");
    }

    public void EndGame()
    {
        ended = true;
    }

    private void SpamFull()
    {
        Debug.Log("Spam full!");
        colorButtonsSum = 0;
        spamOverHeated = true;
    }

    private void SpamEmpty()
    {
        spamOverHeated = false;
    }
}
