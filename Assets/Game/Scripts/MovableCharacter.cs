using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableCharacter : MonoBehaviour
{
    public virtual void onSwipeLeft(){ }
    public virtual void onSwipeRight(){ }
    public virtual void onSwipeUp(){ }
    public virtual void onSwipeDown(){ }
    public virtual void onTap(){ }
    public virtual void onDoubleTap(){ }
}
