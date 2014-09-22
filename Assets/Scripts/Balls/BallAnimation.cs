using UnityEngine;
using System.Collections;

public class BallAnimation : MonoBehaviour
{
    private Animator animator;
    public float sleepTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        sleepTime = Random.Range(5, 30);
    }

    void Update()
    {
        sleepTime -= Time.deltaTime;

        if (sleepTime <= 0)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    {
                        animator.SetTrigger("Happy");
                        break;
                    }
                case 1:
                    {
                        animator.SetTrigger("Wow");
                        break;
                    }
                case 2:
                    {
                        animator.SetTrigger("Wierd");
                        break;
                    }
            }

            sleepTime = Random.Range(5, 30);
        }
    }
}
