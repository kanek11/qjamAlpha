using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class PotionData : MonoBehaviour
{
    //impose singleton pattern
    public static PotionData Instance;


    //R,G,B values of the potion.
    private Dictionary<Attributes, int> _potionAttributes = new Dictionary<Attributes, int>();
    public Dictionary<Attributes, int> PotionAttributes { get { return _potionAttributes; } set { _potionAttributes = value; } }
   
    private Potion Potion;

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
        Potion = GameObject.Find("Player").transform.Find("PlayerInventory").transform.Find("Potion").GetComponent<Potion>();
        if(Potion == null)
        {
            Debug.Log("Original Potion not found!");
        }
        else
        Potion.OnPotionUpdate.AddListener(UpdatePotionData);

        //initialize as zeros
        _potionAttributes.Add(Attributes.R, 10);
        _potionAttributes.Add(Attributes.G, 10);
        _potionAttributes.Add(Attributes.B, 10);

        Debug.Log("PotionData: Potion initialized " + "R: " + _potionAttributes[Attributes.R] + "G: " + _potionAttributes[Attributes.G] + "B: " + _potionAttributes[Attributes.B]);
         

    }

    void UpdatePotionData()
    {
        //update the potion data by copying data ,  not copy reference
        _potionAttributes[Attributes.R] = Potion.PotionAttributes[Attributes.R];
        _potionAttributes[Attributes.G] = Potion.PotionAttributes[Attributes.G];
        _potionAttributes[Attributes.B] = Potion.PotionAttributes[Attributes.B];
    }




}
