using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScManager : MonoBehaviour
{
    public GameObject StartPlane;
    public GameObject VictoryPlane;
    public GameObject FailPlane;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScene()
    {
        StartPlane.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
    public void Victory()
    {
        VictoryPlane.SetActive(true);
    }

    public void Fail()
    {
        FailPlane.SetActive(true);
    }

    public void Restart()
    {
        FailPlane.SetActive(false);
        VictoryPlane.SetActive(false);
        StartScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
