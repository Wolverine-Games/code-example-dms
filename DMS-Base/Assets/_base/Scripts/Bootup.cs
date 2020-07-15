using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootup : MonoBehaviour
{
    public float maxLoadingTime = 2f;
    private float loadTimer = 2f;
    void Start()
    {
        loadTimer = maxLoadingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadTimer < 0f)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            loadTimer -= Time.deltaTime;
        }
    }
}
