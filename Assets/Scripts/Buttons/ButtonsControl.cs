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
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Red)
                {
                    button.PressButton();
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            floor.AddColor(Button.BallColor.Yellow);
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Yellow)
                {
                    button.PressButton();
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            floor.AddColor(Button.BallColor.Blue);
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Blue)
                {
                    button.PressButton();
                    break;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            floor.RemoveColor(Button.BallColor.Red);
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Red)
                {
                    button.ReleaseButton();
                    break;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            floor.RemoveColor(Button.BallColor.Yellow);
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Yellow)
                {
                    button.ReleaseButton();
                    break;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            floor.RemoveColor(Button.BallColor.Blue);
            foreach (Button button in buttons)
            {
                if (button.color == Button.BallColor.Blue)
                {
                    button.ReleaseButton();
                    break;
                }
            }
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
                        button.PressButton();
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
                        button.ReleaseButton();
                        break;
                    }
                }
            }
        }
    }
}
