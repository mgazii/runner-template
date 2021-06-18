using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovableCharacter
{
    public float sideWaySpeed = 1f;
    public float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

     
    public override void onSwipeLeft()
    {
        transform.position += Vector3.left * sideWaySpeed;
    }

    public override void onSwipeRight()
    {
        transform.position += Vector3.right * sideWaySpeed;
    }

    public override void onTap()
    {
        Debug.Log("Tap");
    }

    public override void onDoubleTap()
    {
        Debug.Log("Double Tap");
    }
}
