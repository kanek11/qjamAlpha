using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public GameObject target;
    
    private BossFightManager _bossFightManager;

    private float _progress;

    //on float point

    void Start()
    {

        slider = GetComponent<Slider>();

        _bossFightManager = GameObject.Find("BossFightManager").GetComponent<BossFightManager>();
        if (_bossFightManager == null)
        {
            Debug.Log("ProgressBar: BossFightManager not found!");
        }

        target = _bossFightManager.CurrentEnemy;

        target.GetComponent<EnemyHealth>().OnHealthChanged.AddListener(SetProgressBar);
        if (target == null)
        {
            Debug.Log("ProgressBar: Target not found!");
        }
      

    }

    public void SetProgressBar()
    {
        _progress = 1 - target.GetComponent<EnemyHealth>().Health / target.GetComponent<EnemyHealth>().MaxHealth;
        if(_progress < 0)
        {
            _progress = 0;
            Debug.Log("ProgressBar: Progress is below 0!");
        }
        if(_progress > 1)
        {
            _progress = 1;
            Debug.Log("ProgressBar: Progress is over 1!");
        }
        slider.value = _progress;

        Debug.Log("ProgressBar: Progress changed: " + _progress);




    }

}
