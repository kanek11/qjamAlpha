using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//readme:
//this is the inventory class that should be attached to the player children "PlayerInventory".

//every inventory object should be the child of inventory.


//the inventory system of this game is simple,  won't hold a list, but hold one "potion" .
//whenever an item is picked up,  take effect immediately.


//for consistency,  we still use the "AddItem" £¬ ¡°RemoveItem¡± methods.

//as for the behavior, is modifiable. 



public class Inventory : MonoBehaviour
{

    Potion potion;

    private void Awake()
    {
        //find potion in its children.
        potion = GetComponentInChildren<Potion>(); 

    }
     
    public void AddItem(Item item)
    {
        potion.Use(item);
        
    }





   
}
