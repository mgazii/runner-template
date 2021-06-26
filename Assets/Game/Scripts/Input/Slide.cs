using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide
{
    public enum SlideAct
    {
        START, VERTICAL_UP, VERTICAL_DOWN, HORIZONTAL_LEFT, HORIZONTAL_RIGHT, END
    }

    private SlideAct action;

    public SlideAct Action
    {
        get => action;
        set => action = value;
    }

    private Vector2 distance;

    public Vector2 Distance
    {
        get => distance;
        set => distance = value;
    }

    private Slide() { }

    public Slide(SlideAct action)
    {
        Action = action;
    }

    public Slide(Vector2 distance)
    {
        if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            if(distance.x < 0)
            {
                Action = SlideAct.HORIZONTAL_LEFT;
            }
            else
            {
                Action = SlideAct.HORIZONTAL_RIGHT;
            }
        }else
        {
            if(distance.y < 0)
            {
                Action = SlideAct.VERTICAL_DOWN;
            }
            else
            {
                Action = SlideAct.VERTICAL_UP;
            }
        }
        Distance = distance;
    }

}
