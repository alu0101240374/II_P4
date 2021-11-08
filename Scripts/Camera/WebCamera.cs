using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    void Start()
    {   
        WebCamDevice[] devices = WebCamTexture.devices;
        webCamTexture = new WebCamTexture();
        webCamTexture.deviceName = devices[1].name;

        GameManager.eventPlayCamera += playCamera;
        GameManager.eventPauseCamera += pauseCamera;
        GameManager.eventStopCamera += stopCamera;
    }

    void playCamera()
    {
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    void pauseCamera()
    {
        webCamTexture.Pause();
    }

    void stopCamera()
    {
        webCamTexture.Stop();
        GetComponent<Renderer>().material.mainTexture = default;
    }
}
