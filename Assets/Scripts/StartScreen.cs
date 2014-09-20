using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject startButton;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Look for all fingers
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Debug.Log("Touch");

            if (touch.phase == TouchPhase.Ended)
            {
                //Touch are screens location. Convert to world
                Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

                if (startButton.collider2D.OverlapPoint(position))
                {
                    StartGame();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        if (gameScreen != null)
        {
            gameScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
