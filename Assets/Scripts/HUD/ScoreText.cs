using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
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
        GetComponent<TextMeshProUGUI>().text = "Score: " + (_bossFightManager.CurrentDamage).ToString("F2");

    }
}
