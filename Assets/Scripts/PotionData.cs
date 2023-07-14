using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

//readme: this script is used to store the potion data to global state.

public class PotionData : MonoBehaviour
{ 
    private Potion _potion;

    void Awake()
    {  
    }

    void Start()
    {

        //register to the potion update event
        _potion = GameObject.Find("Player/PlayerInventory/Potion").GetComponent<Potion>();
        if (_potion == null)
        {
            Debug.Log("Original Potion not found!");
            //initialize 
            GlobalState.PotionAttributes.Add(Attributes.R, 10);
            GlobalState.PotionAttributes.Add(Attributes.G, 10);
            GlobalState.PotionAttributes.Add(Attributes.B, 10);

            Debug.Log("PotionData: Potion initialized without _playerTransform " + 
                "R: " + GlobalState.PotionAttributes[Attributes.R] + 
                "G: " + GlobalState.PotionAttributes[Attributes.G] + 
                "B: " + GlobalState.PotionAttributes[Attributes.B]);

        }
        else
        {
            _potion.OnPotionUpdate.AddListener(UpdatePotionData);
            GlobalState.PotionAttributes.Add(Attributes.R, _potion.PotionAttributes[Attributes.R]);
            GlobalState.PotionAttributes.Add(Attributes.G, _potion.PotionAttributes[Attributes.G]);
            GlobalState.PotionAttributes.Add(Attributes.B, _potion.PotionAttributes[Attributes.B]);
             
        }

    }

    void UpdatePotionData()
    {
        //update the potion data by copying data ,  not copy reference
        GlobalState.PotionAttributes[Attributes.R] = _potion.PotionAttributes[Attributes.R];
        GlobalState.PotionAttributes[Attributes.G] = _potion.PotionAttributes[Attributes.G];
        GlobalState.PotionAttributes[Attributes.B] = _potion.PotionAttributes[Attributes.B];

    }


}
