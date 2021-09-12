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
    public float mass = 10f;
    private Vector3 launchForce;
    private bool pulling;
    private Vector3 pullStart, pullEnd;
    private List<GameObject> depictingLine = new List<GameObject>();

    private float denom, a, b, c;

    // Start is called before the first frame update
    void Start()
    {
        mass = 0;
        for (int i = 0; i < 50; i++)
        { 
            depictingLine.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            depictingLine[depictingLine.Count - 1].SetActive(false);
        }
        for (int i = 0; i < sausage.Count; i++)
        {
            mass += sausage[i].mass;
        }
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
                SetPullEnd(Input.mousePosition);
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
            for (int i = 0; i < depictingLine.Count; i++)
            {
                depictingLine[i].SetActive(false);
            }
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


    private float CalcAlpha()
    {
        float gipotenuza;
        float preAlpha;
        gipotenuza = (float)Mathf.Sqrt(launchForce.y * launchForce.y + launchForce.z * launchForce.z);
        if (gipotenuza != 0)
            preAlpha = (float)(Mathf.Rad2Deg * Mathf.Asin(launchForce.y / gipotenuza));
        else
            preAlpha = 0;
        if (launchForce.z < 0)
            preAlpha = Mathf.Sign(preAlpha) * 180 - preAlpha;
        return preAlpha;
    }

    void Depict()
    {
        float alpha = CalcAlpha()+180;
        Debug.Log(alpha.ToString());
        Vector3 V0 = (launchForce / mass) * Time.fixedDeltaTime;
        float v0 = Mathf.Sqrt(V0.y * V0.y + V0.z * V0.z);
        float t = 2 * V0.y / Physics.gravity.y;
        float hMax = 0.65f*v0*v0 * Mathf.Sin(Mathf.Deg2Rad * alpha) * Mathf.Sin(Mathf.Deg2Rad * alpha)/(2* Mathf.Abs(Physics.gravity.y));
        float s = 0.65f*Mathf.Abs(v0 * v0 * Mathf.Sin(2*Mathf.Deg2Rad*alpha) / Mathf.Abs(Physics.gravity.y));
        float sign = ((alpha%360>90) && (alpha % 360 < 270))?1:-1;
        CalcParabolaParams(sausage[0].transform.position.z, sausage[0].transform.position.y, sausage[0].transform.position.z + sign * s / 2, sausage[0].transform.position.y + hMax, sausage[0].transform.position.z + sign * s, sausage[0].transform.position.y);
        for (int i = 0; i < depictingLine.Count; i++)
        {
            depictingLine[i].SetActive(true);
            depictingLine[i].transform.position = new Vector3(sausage[0].transform.position.x, ParabolaPointY(sausage[0].transform.position.z+4*i), sausage[0].transform.position.z + 4*i);
        }
    }
    private void CalcParabolaParams(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
        a = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        b = (x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1) + x1 * x1 * (y2 - y3)) / denom;
        c = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;
    }

    private float ParabolaPointY(float parabolaPointX)
    {
        return a * parabolaPointX * parabolaPointX + b * parabolaPointX + c;
    }

    void ApplyForce()
    {
        foreach (Rigidbody saus in sausage)
            saus.AddForce(launchForce);
    }
}
