using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionHUDBossFight : MonoBehaviour
{
    private Dictionary<Attributes, int> _potionAttributes;

    [SerializeField]
    private GameObject _potionFluidImage;
    public GameObject PotionFluidImage
    {
        get { return _potionFluidImage; }
        set { _potionFluidImage = value; }
    }
 

    private int _r, _g, _b; 
    void Awake()
    {
    }

    void Start()
    {
        
        //register to the potion update event ,  a chain call
        _potionAttributes = GlobalState.PotionAttributes; 

        //initialize
        _r = _potionAttributes[Attributes.R];
        _g = _potionAttributes[Attributes.G];
        _b = _potionAttributes[Attributes.B];

        UpdatePotionHUD();

    }

    void UpdatePotionHUD()
    { 

        //define local rgb for the color
        int r, g, b;
        //normalize the color by divide the max of RGB , and round it to integer
        int max = Mathf.Max(_r, _g, _b);

        r = Mathf.RoundToInt((float)_r / max * 255);
        g = Mathf.RoundToInt((float)_g / max * 255);
        b = Mathf.RoundToInt((float)_b / max * 255);

        if (max >= 255)
        {
            //update the flask imageUI
            _potionFluidImage.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, 255);
        }
        else
        {
            //set the alpha to be max 
            _potionFluidImage.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, (byte)max);
 
        }
         
      

    }
}
