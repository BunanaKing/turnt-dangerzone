using UnityEngine;
using System.Collections;

public class ButtonsControl : MonoBehaviour
{
    public Button[] buttons;
    public Floor floor;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            floor.AddColor(Button.BallColor.Red);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            floor.AddColor(Button.BallColor.Yellow);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            floor.AddColor(Button.BallColor.Blue);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            floor.RemoveColor(Button.BallColor.Red);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            floor.RemoveColor(Button.BallColor.Yellow);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            floor.RemoveColor(Button.BallColor.Blue);
        }

        // Look for all fingers
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Debug.Log("Touch");

            if (touch.phase == TouchPhase.Began)
            {
                //Touch are screens location. Convert to world
                Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

                foreach (Button button in buttons)
                {
                    if (button.collider2D.OverlapPoint(position))
                    {
                        floor.AddColor(button.color);
                        break;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //Touch are screens location. Convert to world
                Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

                foreach (Button button in buttons)
                {
                    if (button.collider2D.OverlapPoint(position))
                    {
                        floor.RemoveColor(button.color);
                        break;
                    }
                }
            }
        }
    }
}
