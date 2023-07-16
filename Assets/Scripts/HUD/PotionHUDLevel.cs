using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI; 

public class PotionHUDLevel : MonoBehaviour
{  
    private Potion _potion; 

    [SerializeField]
    private GameObject  _potionFluidImage;
    public GameObject PotionFluidImage
    {
        get { return _potionFluidImage; }
        set { _potionFluidImage = value; }
    }

    [SerializeField]
    private GameObject  _potionAttributesText;
    public GameObject PotionAttributesText { 
        get { return _potionAttributesText; }
        set { _potionAttributesText = value; }
    }

    private int R, G, B;


    void Awake()
    { 
    }

    void Start()
    {

        //register to the potion update event ,  a chain call
        _potion = GameObject.Find("Player/PlayerInventory/Potion").GetComponent<Potion>(); 
        if (_potion == null)
        {
            Debug.Log("Original Potion not found!");
        } 
       
        _potion.OnPotionUpdate.AddListener(UpdatePotionHUD); 

  
        if(_potionFluidImage == null || _potionAttributesText == null)
        {
            Debug.Log("PotionHUD: PotionFlaskImage or PotionAttributesText not found!");
        }


        //initialize
        R = _potion.PotionAttributes[Attributes.R];
        G = _potion.PotionAttributes[Attributes.G];
        B = _potion.PotionAttributes[Attributes.B]; 

        UpdatePotionHUD();
         
    }

    void UpdatePotionHUD()
    {
        //retrieve the potion data
        R = _potion.PotionAttributes[Attributes.R];
        G = _potion.PotionAttributes[Attributes.G];
        B = _potion.PotionAttributes[Attributes.B]; 

        //define local rgb for the color
        int r, g, b;
        //normalize the color by divide the max of RGB , and round it to integer
        int max = Mathf.Max(R, G, B); 

        r = Mathf.RoundToInt((float)R / max * 255);
        g = Mathf.RoundToInt((float)G / max * 255);
        b = Mathf.RoundToInt((float)B / max * 255);

        if (max >= 510)
        {
            //update the flask imageUI
            _potionFluidImage.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, 255);
        
        }
        else
        {
            //set the alpha to be max 
            _potionFluidImage.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, (byte)(max/2));


        }

         
        //update the texts
        _potionAttributesText.transform.Find("PotionRText").GetComponent<TextMeshProUGUI>().text = R.ToString();
        _potionAttributesText.transform.Find("PotionGText").GetComponent<TextMeshProUGUI>().text = G.ToString();
        _potionAttributesText.transform.Find("PotionBText").GetComponent<TextMeshProUGUI>().text = B.ToString();


    }




}

