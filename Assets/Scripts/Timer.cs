using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private float startTime;
    private float restSeconds;
    private float roundedRestSeconds;
    private float displaySeconds;
    private int displayMinutes;
    private float addedSeconds = 0;

    public int countDownSeconds;
    public GUIText timerText;

    void Start()
    {
    }

    public void StartTimer()
    {
        enabled = true;
        startTime = Time.time;
    }

    void OnGUI()
    {
        //make sure that your time is based on when this script was first called
        //instead of when your game started
        int guiTime = (int)(Time.time - startTime);

        restSeconds = countDownSeconds + addedSeconds - guiTime;

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

    public void Add(int seconds)
    {
        addedSeconds += seconds;
    }
}
