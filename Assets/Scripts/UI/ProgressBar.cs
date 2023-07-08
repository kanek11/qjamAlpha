using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public GameObject target; 

    private float _progress;

    //on float point

    void Start()
    {

        slider = GetComponent<Slider>();

        target = GameObject.Find("Enemy");

        target.GetComponent<EnemyHealth>().OnHealthChanged.AddListener(SetProgressBar);
        if (target == null)
        {
            Debug.Log("ProgressBar: Target not found!");
        }
      

    }

    public void SetProgressBar()
    {
        _progress = 1 - target.GetComponent<EnemyHealth>().Health / target.GetComponent<EnemyHealth>().MaxHealth;
        slider.value = _progress;

        Debug.Log("ProgressBar: Progress changed: " + _progress);




    }

}
