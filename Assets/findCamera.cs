using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findCamera : MonoBehaviour
{
    public void Awake()
    {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.gameObject.GetComponent<Canvas>().worldCamera = camera;
    }
}
