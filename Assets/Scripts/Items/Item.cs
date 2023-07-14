using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//readme:
//to Use() the item is to pick it up and add it to the inventory, access its properties, etc.

public class Item  : MonoBehaviour
{ 
    [SerializeField]  
    public int R_value, G_value, B_value;
    public int R_boost, G_boost, B_boost;   

    void Awake()
    {
        //get it's id for debug information
        int id = this. GetComponent<TilemapPrefab>().Id;
        Debug.Log("Item: " + id + " created.");

        //check for valid values.
        //if any of the values are negative, set them to 0. and report the error.
        if (R_value < 0 || G_value < 0 || B_value < 0)
        {
            Debug.Log("Item: Item values cannot be negative.  Setting to 0.");
            R_value = 0;
            G_value = 0;
            B_value = 0;
        }


        //if any of the boost are negative or 0, set them to 1. and report the error.
        if (R_boost <= 0 || G_boost <= 0 || B_boost <= 0)
        {
            Debug.Log("Item: Item boosts cannot be negative or 0.  Setting to 1.");
            R_boost = 1;
            G_boost = 1;
            B_boost = 1;
        }

    }

}
