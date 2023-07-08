using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //fornow: just follow the player
    public GameObject player; 
    
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    { 

        player = GameObject.Find("Player");
        _target = player.transform;
        if (player == null)
        {
            Debug.LogError("CameraController: player is not found.");
        }
         

    }
 

    // Update is called once per frame
    void Update()
    {

        //follow the player, don't change the z position
        //Debug.Log("Updated, called , is target null?" + (_target == null));
        if(_target != null)
        this.transform.position = new Vector3(_target.position.x,_target.position.y, this.transform.position.z);

    }

   


}
