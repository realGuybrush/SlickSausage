using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask land;
    public List<Rigidbody> sausage;
    public List<Toucher> touchers;
    public float oneLaunchForce = 25f;
    Vector3 launchForce;
    private bool pulling;
    private Vector3 pullStart, pullEnd;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CanJump())
        {
            CheckClicked();
            //CheckTouch();
            if (pulling)
            {
                CalculateForce();
                Depict();
            }
        }
    }

    void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            pulling = true;
            SetPullStart(Input.mousePosition);
        }
        if (Input.touchCount == 0)
        {
            pulling = false;
            SetPullEnd(Input.mousePosition);
            CalculateForce();
            ApplyForce();
        }
    }

    public void CheckClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pulling = true;
            SetPullStart(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            pulling = false;
            SetPullEnd(Input.mousePosition);
            CalculateForce();
            ApplyForce();
        }
    }

    bool CanJump()
    {
        foreach(Toucher t in touchers)
            if(t.touches > 0)
                return true;
        return false;
    }

    void SetPullStart(Vector3 point)
    {
        pullStart = point;
    }

    void SetPullEnd(Vector3 point)
    {
        pullEnd = point;
    }

    void CalculateForce()
    {
        launchForce = oneLaunchForce * new Vector3(0, pullStart.y - pullEnd.y, pullStart.x - pullEnd.x);
    }

    void Depict()
    { 
    }

    void ApplyForce()
    {
        foreach (Rigidbody saus in sausage)
            saus.AddForce(launchForce);
    }
}
