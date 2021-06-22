using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovableCharacter
{
    public float pathWidth = 8f;
    public float sideWaySpeed = 8f;

    private Vector3 slideStartPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void onSlideStart()
    {
        slideStartPos = transform.localPosition;
    }

    public override void onSlide(Vector2 distance)
    {
        Vector3 pos = transform.localPosition;
        float posX = slideStartPos.x + (distance.x * sideWaySpeed);
        float posY = slideStartPos.y + (distance.y * sideWaySpeed);
        pos.x = posX;
        pos.z = posY;
        pos.x = Mathf.Clamp(pos.x, -pathWidth, pathWidth);
        transform.localPosition = pos;
        
        //transform.position = Camera.main.ScreenToWorldPoint(slide); // new Vector3(slide.x/100, transform.position.y, transform.position.z);
    }


    public override void onSwipeLeft()
    {
        /*Vector3 move = transform.position + Vector3.left * sideWaySpeed;
        if (Mathf.Abs(move.x - 0) <= pathWidth)
        {
            transform.position = move;
        }*/
    }

    public override void onSwipeRight()
    {
        /*Vector3 move = transform.position + Vector3.right * sideWaySpeed;
        if (Mathf.Abs(move.x - 0) <= pathWidth)
        {
            transform.position = move;
        }*/
    }

    public override void onTap()
    {
        Debug.Log("Tap");
    }

    public override void onDoubleTap()
    {
        Debug.Log("Dobuel Tap");
        transform.position += Vector3.up * 2;
    }
}
