using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{

    public float speed = 2.0f;
    public Transform TopBound;
    public Transform BottomBound;
    private bool TopBottomSwitch = false;


    private SpriteRenderer rendererComponent;
    private bool IsVisible = false;
    private bool WasVisible = false;

    void Start()
    {
        rendererComponent = GetComponent<SpriteRenderer>();

        if (TopBound.position.x < this.transform.position.x)
        {
            GetComponent<ScrollingScript>().enabled = false;
        }

        if (this.transform.position.y > TopBound.position.y)
            TopBottomSwitch = false;

        if (this.transform.position.y < BottomBound.position.y)
            TopBottomSwitch = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.y >= BottomBound.position.y && !TopBottomSwitch)
            godown();

        if (this.transform.position.y <= TopBound.position.y && TopBottomSwitch)
            goup();

        if (TopBound.position.x >= this.transform.position.x && !IsVisible)
        {
            GetComponent<ScrollingScript>().enabled = true;
            IsVisible = true;
        }

        if (rendererComponent.IsVisibleFrom(Camera.main) && !WasVisible)
            WasVisible = true;


    }

    void godown()
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y <= BottomBound.position.y)
            TopBottomSwitch = true;

    }

    void goup()
    {
        this.transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y >= TopBound.position.y)
            TopBottomSwitch = false;
    }

    void FixOutOfScreenGlitch()
    {
        if (rendererComponent.IsVisibleFrom(Camera.main) == false && WasVisible)
        {
            if (this.transform.position.y > TopBound.position.y)
                TopBottomSwitch = false;

            if (this.transform.position.y < BottomBound.position.y)
                TopBottomSwitch = true;
        }
    }
}
