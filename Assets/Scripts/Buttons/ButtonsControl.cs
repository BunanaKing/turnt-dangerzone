using UnityEngine;
using System.Collections;

public class ButtonsControl : MonoBehaviour
{
    public Button button_red;
    public Button button_yellow;
    public Button button_blue;
    public Floor floor;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            floor.AddColor(Button.ButtonColor.Red);
            button_red.PressButton();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            floor.RemoveColor(Button.ButtonColor.Red);
            button_red.ReleaseButton();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            floor.AddColor(Button.ButtonColor.Yellow);
            button_yellow.PressButton();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            floor.RemoveColor(Button.ButtonColor.Yellow);
            button_yellow.ReleaseButton();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            floor.AddColor(Button.ButtonColor.Blue);
            button_blue.PressButton();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            floor.RemoveColor(Button.ButtonColor.Blue);
            button_blue.ReleaseButton();
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

                foreach (Button button in GetComponentsInChildren<Button>())
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

                foreach (Button button in GetComponentsInChildren<Button>())
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
