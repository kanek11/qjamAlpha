using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

//readme:
//this class is used to pick up items in the game.
//since pick up is a interactive behavior from the player, we attach this script to the player.

//the pickup event leaves the responsibility of post-pickup behavior to the item.

//specifications:
//allow to pick up when the player coincide with the item.  
//press enter button to pick up the item.

//OnTriggerEnter2D, set the item to be available.
//OnTriggerExit2D, set the item to be unavailable.

//in Update(), check if the item is available, if so, press enter can pick up the item.

//bug£º
//all instances will have a listener to the event, which is not correct.
//we just refer to the item's public method.

 

public class PlayerPickUp : MonoBehaviour
{
    Inventory inventory;

    //state
    Item itemToPickUp;


    //=====event list
    //register the item pick up event.  
    //public UnityEvent OnPlayerPickUp;

    private void Awake()
    {
        inventory = GetComponentInChildren<Inventory>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.gameObject.CompareTag("Item"))
        {

            itemToPickUp = other.GetComponent<Item>();

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (other.gameObject.CompareTag("Item"))
        {
            itemToPickUp = null;
        }
    }


        void Update()
        {
            //if the item is available, press enter to pick up the item.
            if (itemToPickUp != null && Input.GetKeyDown(KeyCode.Return))
            {
            
                 //caution: make sure add before destroy.
                //add the item to the inventory.
                inventory.AddItem(itemToPickUp);

               // OnPlayerPickUp?.Invoke();
               itemToPickUp.GetComponent<ItemPickUp>().OnItemPickUp(); 

               itemToPickUp = null;

            }
        }
 
}
