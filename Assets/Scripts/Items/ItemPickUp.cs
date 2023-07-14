using System.Collections;
using System.Collections.Generic;
using UnityEngine; 



//readme:
//this component is to be attached under item
//in response to the player pick up event.



public class ItemPickUp : MonoBehaviour
{
      
    void Start()
    {
        //register the event
        //PlayerPickUp playerPickUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickUp>();
        
        //playerPickUp.OnPlayerPickUp.AddListener(OnItemPickUp);  

    }

    public void OnItemPickUp()
    {
        Debug.Log("ItemPickUp: OnItemPickUp() called.");

        //destroy its children effect
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //disable its collider
        GetComponent<BoxCollider2D>().enabled = false;

    }


 
}
