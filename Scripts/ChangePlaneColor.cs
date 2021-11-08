using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneColor : MonoBehaviour
{
    void Start()
    {
        GameManager.eventLoudAudio += changeColor;
    }

    void changeColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
    }
}
