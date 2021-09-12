using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public LayerMask sausageLayer;
    ScManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Canvas").GetComponent<ScManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager == null)
            manager = GameObject.Find("Canvas").GetComponent<ScManager>();
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (sausageLayer == (sausageLayer | (1 << collider.gameObject.layer)))
        {
            manager.Victory();
        }
    }
}
