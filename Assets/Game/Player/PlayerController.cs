using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float runSpeed = 30f;
    public float pathWidth = 8f;
    public float sideWaySpeed = 8f;

    private Vector3 slideStartPos;

    // RegisterSwipe : registers for swipe actions
    // AddGameStateListener : registers for gamestate events
    void Start()
    {
        GameManager.Instance.AddGameStateListener(this);
        InputManager.Instance.RegisterSlide(OnSlide);
        InputManager.Instance.RegisterTap(OnTap);
        InputManager.Instance.RegisterSwipe(OnSwipe);
    }

    // only runs on GameState.MID
    public void MID()
    {
        transform.localPosition += Vector3.forward * runSpeed * Time.deltaTime;
    }

    // only runs if InputManager.tapAction invoked
    private void OnTap(Tap tap)
    {
        if(tap.Type == Tap.TapType.TAP)
        {
            //Debug.Log("TAP");
        }
        else
        {
            //Debug.Log("DOUBLE TAP");
        }
    }

    // only runs if InputManager.swipeAction invoked
    private void OnSwipe(Swipe swipe)
    {
        switch (swipe.Action)
        {
            case Swipe.SwipeAct.VERTICAL_UP:
                //Debug.Log("SWIPE VERTICAL UP");
                break;
            case Swipe.SwipeAct.VERTICAL_DOWN:
                //Debug.Log("SWIPE VERTICAL DOWN");
                break;
            case Swipe.SwipeAct.HORIZONTAL_LEFT:
                //Debug.Log("SWIPE HORIZONTAL LEFT");
                break;
            case Swipe.SwipeAct.HORIZONTAL_RIGHT:
                //Debug.Log("SWIPE HORIZONTAL RIGHT");
                break;
        }
    }

    // only runs if InputManager.slideAction invoked
    private void OnSlide(Slide slide)
    {
        if(slide.Action == Slide.SlideAct.START)
        {
            slideStartPos = player.transform.localPosition;
            //Debug.Log("SLIDE START");
        }
        else if(slide.Action == Slide.SlideAct.END)
        {
            //Debug.Log("SLIDE END");
        }
        else
        {
            Vector3 pos = player.transform.localPosition;
            float posX = slideStartPos.x + (slide.Distance.x * sideWaySpeed);
            float posY = slideStartPos.y + (slide.Distance.y * sideWaySpeed);
            pos.x = posX;
            pos.z = posY;
            pos.x = Mathf.Clamp(pos.x, -pathWidth, pathWidth);
            player.transform.localPosition = pos;
            switch (slide.Action)
            {
                case Slide.SlideAct.VERTICAL_UP:
                    //Debug.Log("SLIDE VERTICAL UP");
                    break;
                case Slide.SlideAct.VERTICAL_DOWN:
                    //Debug.Log("SLIDE VERTICAL DOWN");
                    break;
                case Slide.SlideAct.HORIZONTAL_LEFT:
                    //Debug.Log("SLIDE HORIZONTAL LEFT");
                    break;
                case Slide.SlideAct.HORIZONTAL_RIGHT:
                    //Debug.Log("SLIDE HORIZONTAL RIGHT");
                    break;
            }
        }
    }

}
