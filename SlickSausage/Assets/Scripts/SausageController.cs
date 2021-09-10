using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageTailController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask land;
    public Rigidbody body;
    public Rigidbody tailestTail;
    bool follow;
    Vector3 pointToFollow;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        body.constraints = RigidbodyConstraints.None;
        {
            CheckClicked();
            //CheckTouch();
            if (follow)
            {
                //CastRayToTouchPoint();
                CastRayToClickPoint();
                Follow();
            }
        }
    }

    void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            follow = true;
        }
        if (Input.touchCount == 0)
        {
            follow = false;
        }
    }

    public void CheckClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            follow = true;
            tailestTail.constraints = RigidbodyConstraints.FreezePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            follow = false;
            tailestTail.constraints = RigidbodyConstraints.None;
            body.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    void CastRayToTouchPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f));
        RaycastHit[] hit = Physics.RaycastAll(ray, 100f, land);
        if (hit.Length > 0)
            SetFollowPoint(hit[0].point);
    }

    void CastRayToClickPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        RaycastHit[] hit = Physics.RaycastAll(ray, 100f, land);
        if (hit.Length > 0)
            SetFollowPoint(hit[0].point);
    }

    public void SetFollowPoint(Vector3 newFollow)
    {
        pointToFollow = newFollow;
    }

    internal virtual void Follow()
    {
        //float diff = transform.position.x - pointToFollow.x;
        //if (Mathf.Abs(diff) > 1.0f)
        //    diff = Mathf.Sign(diff);
        body.transform.position = new Vector3(pointToFollow.x, pointToFollow.y+0.2f, pointToFollow.z );
    }/*
    internal override void Follow()
    {
        float diffX = pointToFollow.x - transform.position.x;
        float diffZ = (pointToFollow.z - transform.position.z);
        if (Mathf.Abs(diffX) > 1.0f)
            diffX = Mathf.Sign(diffX);
        if (diffZ < baseDiffZ)
            diffZ = 0;
        body.velocity = new Vector3(velocityX * diffX, 0.0f, velocityZ * diffZ);
    }*/
}
