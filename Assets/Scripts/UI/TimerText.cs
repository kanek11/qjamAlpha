using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class TimerText : MonoBehaviour
{

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        //update text
        GetComponent<TextMeshProUGUI>().text = "Time: " +   _gameManager.ElapsedTime.ToString("F2");
        
    }
}
