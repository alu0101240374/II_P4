using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMicrophone : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayRecordedSound);
    }

    public void PlayRecordedSound() 
    {
       GameManager.manager.playMic();
    }
}
