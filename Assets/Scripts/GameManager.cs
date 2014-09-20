using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public Floor floor;
    public Score score;

    private bool prevoiusEnabled = false;
    private bool gameEnded = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!prevoiusEnabled && prevoiusEnabled != enabled)
        {
            //Empieza el juego
            prevoiusEnabled = enabled;
            timer.StartTimer();
        }

        if (enabled)
        {
            if (!timer.enabled && !gameEnded)
            {
                gameEnded = true;
                floor.EndGame();
            }
        }
    }
}
