using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class FadeInManager : MonoBehaviour
{
    DefaultObserverEventHandler defaultObserverEventHandler;

    bool isTracked = false;
    bool fadeIsOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Color c = GetComponent<MeshRenderer>().material.color;
        c.a = 0f;
        defaultObserverEventHandler = GetComponentInParent<DefaultObserverEventHandler>();
    }

    IEnumerator FadeIn()
    {
        if (isTracked == true && fadeIsOver == false)
        {
            for (float f = 0.05f; f <= 1; f += 0.05f)
            {
                Color c = GetComponent<MeshRenderer>().material.color;
                c.a = f;
                yield return new WaitForSeconds(0.05f);               
            }
            fadeIsOver = true;
        }
    }

    public void startFading()
    {
        StartCoroutine("FadeIn");
    }

    public void stopFading()
    {
        StopCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<DefaultObserverEventHandler>().trackingStatus == DefaultObserverEventHandler.TrackingStatus.Tracked)
        {
            GetComponent<VideoPlayer>().Play();
            isTracked = true;
            startFading();
        }
        else
        {
            GetComponent<VideoPlayer>().Stop();
            isTracked = false;
            stopFading();
        }        
    }
}
