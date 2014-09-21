using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public TextMesh scoreText;
    private int score = 0;

    // Use this for initialization
    void Start()
    {
        score = 0;
        Floor.OnScoreAdding += new Floor.ScoreAdded(AddScore);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
