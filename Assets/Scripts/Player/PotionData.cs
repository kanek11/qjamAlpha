using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class PotionData : MonoBehaviour
{
    //impose singleton pattern
    public static PotionData Instance;


    //R,G,B values of the potion.
    //set public to be queried by the boss fight script
    private Dictionary<Attributes, int> _potionAttributes = new Dictionary<Attributes, int>(); 
    public Dictionary<Attributes, int> PotionAttributes
    {
        get { return _potionAttributes; }
    }

   
    private Potion _potion;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("More than one instance of PotionData found, delete this one");
            Destroy(this);
        }
    }

    void Start()
    {

        //register to the potion update event
        _potion = GameObject.Find("Player/PlayerInventory/Potion").GetComponent<Potion>();
        if (_potion == null)
        {
            Debug.Log("Original Potion not found!");
            //initialize as zeros
            _potionAttributes.Add(Attributes.R, 10);
            _potionAttributes.Add(Attributes.G, 10);
            _potionAttributes.Add(Attributes.B, 10);

            Debug.Log("PotionData: Potion initialized without player " + "R: " + _potionAttributes[Attributes.R] + "G: " + _potionAttributes[Attributes.G] + "B: " + _potionAttributes[Attributes.B]);

        }
        else
        {
            _potion.OnPotionUpdate.AddListener(UpdatePotionData);
            _potionAttributes.Add(Attributes.R, _potion.PotionAttributes[Attributes.R]);
            _potionAttributes.Add(Attributes.G, _potion.PotionAttributes[Attributes.G]);
            _potionAttributes.Add(Attributes.B, _potion.PotionAttributes[Attributes.B]);
             
        }

    }

    void UpdatePotionData()
    {
        //update the potion data by copying data ,  not copy reference
        _potionAttributes[Attributes.R] = _potion.PotionAttributes[Attributes.R];
        _potionAttributes[Attributes.G] = _potion.PotionAttributes[Attributes.G];
        _potionAttributes[Attributes.B] = _potion.PotionAttributes[Attributes.B];

    }




}
