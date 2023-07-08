using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//readme:
//the manager scripts are persistent through the scenes.
//player manager is responsible for the player prefab, and player doesn't exists in the title.
//so a more robust approach is to link the prefab in the inspector.  and initialize the player as the setup;


[Serializable]
public class PlayerManager  
{

     public Vector2 SpawnPoint;

     private PlayerMovement _playerMovement;

     public GameObject PlayerInstance;
     

     public void SetupPlayer()
     {  
        _playerMovement = PlayerInstance.GetComponent<PlayerMovement>();
        PlayerInstance.name = "Player";  
    }

     public void ResetPlayer()
     {
        PlayerInstance.transform.position = SpawnPoint;
     }

    public void EnablePlayerControl()
    {
        _playerMovement.enabled = true;
    }

    //disable the player movement
     public void DisablePlayerControl()
     {
        _playerMovement.enabled = false;
     }

}
