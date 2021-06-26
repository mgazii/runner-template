using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap
{
    public enum TapType
    {
        TAP, DOUBLE_TAP
    }

    private TapType type;

    public TapType Type
    {
        get => type;
        set => type = value;
    }

    private Tap() { }
    public Tap(TapType type)
    {
        this.type = type;
    }

}
