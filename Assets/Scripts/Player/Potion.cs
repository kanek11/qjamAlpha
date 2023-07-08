using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Potion : MonoBehaviour
{
    //R,G,B values of the potion.
    private Dictionary<Attributes, int> _potionAttributes = new Dictionary<Attributes, int>() ;
    public Dictionary<Attributes, int> PotionAttributes { get { return _potionAttributes; } set { _potionAttributes = value; } }

    public UnityEvent OnPotionUpdate;

    void Start()
    {
       //initialize as zeros
       _potionAttributes.Add(Attributes.R, 10);
       _potionAttributes.Add(Attributes.G, 10);
       _potionAttributes.Add(Attributes.B, 10);

    }


    public void Use(Item item)
    {
        //add the item's value to the potion.
        _potionAttributes[Attributes.R] += item.R_value; 
        _potionAttributes[Attributes.G] += item.G_value;
        _potionAttributes[Attributes.B] += item.B_value;

        //multiply the potion's value by the item's boost.
        _potionAttributes[Attributes.R] *= item.R_boost;
        _potionAttributes[Attributes.G] *= item.G_boost;
        _potionAttributes[Attributes.B] *= item.B_boost;

        Debug.Log("Potion updated " + "R: " + _potionAttributes[Attributes.R] + "G: " + _potionAttributes[Attributes.G] + "B: " + _potionAttributes[Attributes.B]);

        //update the potion.
        OnPotionUpdate?.Invoke();


    }


}
