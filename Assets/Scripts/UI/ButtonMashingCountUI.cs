using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonMashingCountUI : MonoBehaviour
{
    private BossFightManager _bossFightManager;
    // Start is called before the first frame update
    void Start()
    {
        _bossFightManager  = GameObject.Find("BossFightManager").GetComponent<BossFightManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //update text
        GetComponent<TextMeshProUGUI>().text = "Count: " + _bossFightManager.ButtonPressCount.ToString();

    }
}

