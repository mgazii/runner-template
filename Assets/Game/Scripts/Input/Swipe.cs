using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe
{
    public enum SwipeAct
    {
        VERTICAL_UP, VERTICAL_DOWN, HORIZONTAL_LEFT, HORIZONTAL_RIGHT
    }

    private SwipeAct action;

    public SwipeAct Action
    {
        get => action;
        set => action = value;
    }

    private Swipe() { }

    public Swipe(SwipeAct action)
    {
        Action = action;
    }
}
