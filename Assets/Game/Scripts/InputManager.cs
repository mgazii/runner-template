using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = true;
    
    public float SWIPE_THRESHOLD = 20f;
    private float DOUBLE_TAP_TIME = .2f;
    private bool sliding;

    private float lastTap;
    private float secsSinceLastTap;

    private float fingerDownX;
    private float fingerUpX;
    private float fingerDownY;
    private float fingerUpY;
    
    // Update is called once per frame
    void Update()
    {
        if(GameManager.getInstance().CurrentGameState == GameManager.GameState.MainGame)
        {
            Debug.Log("MANAGER" + GameManager.getInstance());
            Debug.Log("Player" + GameManager.getInstance().player);
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                    Vector3 pointUp = GameManager.cam.ScreenToViewportPoint(fingerUp);
                    fingerUpX = pointUp.x;
                    fingerUpY = pointUp.y;
                    Vector3 pointDown = GameManager.cam.ScreenToViewportPoint(fingerDown);
                    fingerDownX = pointDown.x;
                    fingerDownY = pointDown.y;

                    GameManager.getInstance().player.onSlideStart();
                    sliding = true;
                }


                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 pointDown = GameManager.cam.ScreenToViewportPoint(touch.position);
                    fingerDownX = pointDown.x;
                    fingerDownY = pointDown.y;
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    sliding = false;
                    fingerDown = touch.position;
                    Vector3 pointDown = GameManager.cam.ScreenToViewportPoint(fingerDown);
                    fingerDownX = pointDown.x;
                    fingerDownY = pointDown.y;
                    checkSwipe();
                    checkTap();
                    GameManager.getInstance().player.onSlideEnded();
                }

                if (sliding)
                {
                    GameManager.getInstance().player.onSlide(new Vector2(fingerDownX-fingerUpX,fingerDownY-fingerUpY));
                }
            }
        }
        
    }

    #region  Tap

    private void checkTap()
    {
        secsSinceLastTap = Time.time - lastTap;
        if(secsSinceLastTap <= DOUBLE_TAP_TIME)
        {
            GameManager.getInstance().player.onDoubleTap();
        }
        else
        {
            GameManager.getInstance().player.onTap();
        }
        lastTap = Time.time;
    }
    #endregion

    #region Swipe
    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                GameManager.getInstance().player.onSwipeUp();
                //OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                GameManager.getInstance().player.onSwipeDown();
                //OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                GameManager.getInstance().player.onSwipeRight();
                //nSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                GameManager.getInstance().player.onSwipeLeft();
                //OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("Click!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }
    
    #endregion

}
