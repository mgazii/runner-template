using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableCharacter : MonoBehaviour
{
    public virtual void onSwipeLeft() { }
    public virtual void onSwipeRight() { }
    public virtual void onSwipeUp() { }
    public virtual void onSwipeDown() { }
    public virtual void onTap() { }
    public virtual void onDoubleTap() { }

    public virtual void onSlide(Vector2 distance)
    {
        onSlideHorizontal(distance.x);
        onSlideVertical(distance.y);
    }

    public virtual void onSlideHorizontal(float distance) { }

    public virtual void onSlideVertical(float distance) { }

    public virtual void onSlideStart() { }

    public virtual void onSlideEnded() { }
}
