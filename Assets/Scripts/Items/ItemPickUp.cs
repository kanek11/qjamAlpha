using System.Collections;
using System.Collections.Generic;
using UnityEngine; 



//readme:
//this component is to be attached under item
//in response to the player pick up event.

//bug history:
//don't use Event , that will cause all instance be triggered by one event.



public class ItemPickUp : MonoBehaviour
{
    private AudioSource _itemGetAudio;
      
    void Start()
    { 
        _itemGetAudio = GetComponent<AudioSource>();
        if (_itemGetAudio == null)
        {
            Debug.Log("No AudioSource found on " + gameObject.name);
        }
    }

    public void OnItemPickUp()
    {
        Debug.Log("ItemPickUp: OnItemPickUp() called.");
        //play sound
        _itemGetAudio.Play();

        //destroy its children effect
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //disable its collider
        GetComponent<BoxCollider2D>().enabled = false;

    }


 
}
