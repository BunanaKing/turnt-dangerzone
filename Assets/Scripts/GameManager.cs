using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Timer timer;
    public Floor floor;
    private bool prevoiusEnabled = false;

    private bool gameEnded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!prevoiusEnabled && prevoiusEnabled != enabled)
        {
            //Empieza el juego
            Debug.Log("Start Game");
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
