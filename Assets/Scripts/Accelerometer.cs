using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour
{
    private static float accel_scale = 10;

    public static float X()
    {
        return Input.acceleration.x / accel_scale;
    }
}
