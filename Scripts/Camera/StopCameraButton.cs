using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopCameraButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(StopCamera);
    }

    public void StopCamera() 
    {
       GameManager.manager.stopCamera();
    }
}

