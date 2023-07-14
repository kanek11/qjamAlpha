using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.UI;
using TMPro;

public class BossFightTimerText : MonoBehaviour
{

    private BossFightManager _bossFightManager;
    // Start is called before the first frame update
    void Start()
    {
        _bossFightManager = GameObject.Find("BossFightManager").GetComponent<BossFightManager>();
        if (_bossFightManager == null)
            Debug.Log("BossFightTimerText: _bossFightManager is null");

    }

    // Update is called once per frame
    void Update()
    {
        //update text
        GetComponent<TextMeshProUGUI>().text = "Time Limit: " + (_bossFightManager.TimeLimit - _bossFightManager.ElapsedTime).ToString("F2");

    }
}

