using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private float restSeconds;
    private float roundedRestSeconds;
    private float displaySeconds;
    private int displayMinutes;
    private float freezeLeftTime = 0f;

    public float countDownSeconds;
    public TextMesh timerText;

    void Start()
    {
    }

    public void StartTimer()
    {
        enabled = true;
        restSeconds = countDownSeconds;
    }

    void Update()
    {
        if (enabled)
        {
            float restingTime = Time.deltaTime;
            //Chequeamos el Freeze Time
            if (freezeLeftTime > 0)
            {
                freezeLeftTime -= Time.deltaTime;
                //Bajamos la velocidad del tiempo a la mitad. Visualmente estaría bueno que el reloj tuviera hielo o algo asi
                restingTime = restingTime / 2;
                if (freezeLeftTime <= 0) freezeLeftTime = 0;
            }
            restSeconds -= restingTime;
        }
    }

    void OnGUI()
    {        
        if (enabled)
        {
            //display messages or whatever here -->do stuff based on your timer
            if (restSeconds == 60)
            {
                //print("One Minute Left");
            }
            if (restSeconds <= 0)
            {
                print("Time is Over");
                enabled = false;
                //do stuff here
            }
        }

        //display the timer
        roundedRestSeconds = Mathf.CeilToInt(restSeconds);
        displaySeconds = roundedRestSeconds % 60;
        displayMinutes = (int)(roundedRestSeconds / 60);
        string text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);

        timerText.text = text;
        //GUI.Label(new Rect(20, 20, 100, 30), text);
    }

    public void Freeze()
    {
        //Tiempo que dura el efecto
        freezeLeftTime = 10f; 
    }
}
