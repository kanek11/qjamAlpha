using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIcon : MonoBehaviour
{

    public GameObject[] EnemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = EnemyPrefabs[GlobalState.EnemyIndex]; 
        //set the image of the enemy

        this.GetComponent<Image>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
