using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppInit : MonoBehaviour
{
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void onBeforeSceneLoadRuntimeMethod()
    {
        Application.targetFrameRate = 144;
        //Debug.Log("Before scene loaded");
        //Debug.Log("Before scene loaded and the time is: " + Time.time);
    }
}
