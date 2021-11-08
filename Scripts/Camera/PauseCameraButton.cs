using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCameraButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(pauseCamera);
    }

    public void pauseCamera() 
    {
       GameManager.manager.pauseCamera();
    }
}
