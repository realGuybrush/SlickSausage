using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform toFollow;
    float diffX;
    // Start is called before the first frame update
    void Start()
    {
        diffX = transform.position.x - toFollow.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(toFollow.position.x + diffX, toFollow.position.y, toFollow.position.z);
    }
}
