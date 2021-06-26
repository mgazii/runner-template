using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static InputManager manager;

    private InputManager(){}

    public static InputManager Instance
    {
        get => manager;
    }

    private void Awake()
    {
        manager = this;
    }

    public float SWIPE_THRESHOLD = 20f;
    public bool detectObjectTap = true;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    private float DOUBLE_TAP_TIME = .2f;
    private bool sliding;
    

    private float lastTap;
    private float secsSinceLastTap;

    private float fingerDownX;
    private float fingerUpX;
    private float fingerDownY;
    private float fingerUpY;

    private UnityAction<Swipe> swipeAction;
    private UnityAction<Slide> slideAction;
    private UnityAction<Tap> tapAction;

    public void RegisterSwipe(UnityAction<Swipe> swipe)
    {
        swipeAction += swipe;
    }

    public void RegisterSlide(UnityAction<Slide> slide)
    {
        slideAction += slide;
    }

    public void RegisterTap(UnityAction<Tap> tap)
    {
        tapAction += tap;
    }

    public void UnRegisterSwipe(UnityAction<Swipe> swipe)
    {
        swipeAction -= swipe;
    }

    public void UnRegisterSlide(UnityAction<Slide> slide)
    {
        slideAction -= slide;
    }

    public void UnRegisterTap(UnityAction<Tap> tap)
    {
        tapAction -= tap;
    }

    private void Start()
    {
        GameManager.Instance.AddGameStateListener(this);
        tapAction += OnPrepTap;
    }

    public void PREP()
    {
        foreach (Touch touch in Input.touches)
        {
            CheckTap(touch);
        }
    }

    private void OnPrepTap(Tap tap)
    {
        if (tap.Type == Tap.TapType.TAP)
        {
            Destroy(GameObject.FindGameObjectWithTag("UIStart"));
            GameManager.Instance.CurrentGameState = GameManager.GameState.MID;
            tapAction -= OnPrepTap;
        }
    }

    public void MID()
    {
        foreach (Touch touch in Input.touches)
        {
            CheckTap(touch);
            CheckSlide(touch);
            CheckSwipe(touch);
        }
    }

    #region Slide
    private void CheckSlide(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 pointUp = GameManager.cam.ScreenToViewportPoint(touch.position);
            fingerUpX = pointUp.x;
            fingerUpY = pointUp.y;
            fingerDownX = pointUp.x;
            fingerDownY = pointUp.y;
            slideAction.Invoke(new Slide(Slide.SlideAct.START));
            sliding = true;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            Vector3 pointDown = GameManager.cam.ScreenToViewportPoint(touch.position);
            fingerDownX = pointDown.x;
            fingerDownY = pointDown.y;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            sliding = false;
            Vector3 pointDown = GameManager.cam.ScreenToViewportPoint(touch.position);
            fingerDownX = pointDown.x;
            fingerDownY = pointDown.y;
            slideAction.Invoke(new Slide(Slide.SlideAct.END));
        }

        if (sliding)
        {
            slideAction.Invoke(new Slide(new Vector2(fingerDownX - fingerUpX, fingerDownY - fingerUpY)));
        }
    }
    #endregion


    #region  Tap
    private void CheckTap(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended)
        {
            Tap tap;
            secsSinceLastTap = Time.time - lastTap;
            if (secsSinceLastTap <= DOUBLE_TAP_TIME)
            {
                tap = new Tap(Tap.TapType.DOUBLE_TAP);
            }
            else
            {
                tap = new Tap(Tap.TapType.TAP);
                if (detectObjectTap)
                {
                    Ray obj = GameManager.cam.ScreenPointToRay(fingerDown);
                    RaycastHit hit;
                    if (Physics.Raycast(obj, out hit))
                    {
                        if (hit.collider != null)
                        {
                            Color col = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                            hit.collider.GetComponent<MeshRenderer>().material.color = col;
                        }
                    }
                }
            }
            lastTap = Time.time;
            tapAction.Invoke(tap);
        }
    }
    #endregion

    #region Swipe
    private void CheckSwipe(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            fingerUp = touch.position;
            fingerDown = touch.position;
        }

        //Detects swipe after finger is released
        if (touch.phase == TouchPhase.Ended)
        {
            fingerDown = touch.position;

            if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
            {
                //Debug.Log("Vertical");
                if (fingerDown.y - fingerUp.y > 0)//up swipe
                {
                    swipeAction.Invoke(new Swipe(Swipe.SwipeAct.VERTICAL_UP));
                }
                else if (fingerDown.y - fingerUp.y < 0)//Down swipe
                {
                    swipeAction.Invoke(new Swipe(Swipe.SwipeAct.VERTICAL_DOWN));
                }
                fingerUp = fingerDown;
            }

            //Check if Horizontal swipe
            else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
            {
                //Debug.Log("Horizontal");
                if (fingerDown.x - fingerUp.x > 0)//Right swipe
                {
                    swipeAction.Invoke(new Swipe(Swipe.SwipeAct.HORIZONTAL_RIGHT));
                }
                else if (fingerDown.x - fingerUp.x < 0)//Left swipe
                {
                    swipeAction.Invoke(new Swipe(Swipe.SwipeAct.HORIZONTAL_LEFT));
                }
                fingerUp = fingerDown;
            }

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
