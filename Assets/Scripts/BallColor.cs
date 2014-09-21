using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BallColor
{
    public static Color Red = Color.red;
    public static Color Yellow = Color.yellow;
    public static Color Blue = Color.blue;
    public static Color Purple = new Color(178 / 255F, 0F, 255 / 255F);
    public static Color Green = Color.green;
    public static Color Orange = new Color(255 / 255F, 120 / 255F, 0F);
    public static Color White = Color.white;

    public static Color ColorByValue(int value)
    {
        switch (value)
        {
            case 1: return Red;
            case 33: return Yellow;
            case 34: return Orange;
            case 77: return Blue;
            case 78: return Purple;
            case 110: return Green;
            case 111: return White;
            default: return Color.black;
        }
    }
}
