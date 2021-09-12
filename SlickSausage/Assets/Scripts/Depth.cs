using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour
{
    public Transform player;
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
        if(manager == null)
            manager = GameObject.Find("Canvas").GetComponent<ScManager>();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sausageLayer == (sausageLayer | (1 << other.gameObject.layer)))
        {
            manager.Fail();
        }
    }
}
