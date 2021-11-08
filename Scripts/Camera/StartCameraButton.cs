using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCameraButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(playCamera);
    }

    public void playCamera() 
    {
       GameManager.manager.playCamera();
    }
}
