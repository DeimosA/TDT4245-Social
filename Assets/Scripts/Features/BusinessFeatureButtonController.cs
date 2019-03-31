using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BusinessFeatureButtonController : MonoBehaviour
{
    public BusinessFeature feature;
    [Header("Prefabs")]
    public Button purchasableButton;
    public Button isPurchasedButton;
    // Start is called before the first frame update
    void Start()
    {
        //set button text
        transform.Find("Text").GetComponent<Text>().text = feature.title.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sets button status based on player features and stats
    //Sets status in children if this is purchased
    public void SetButtonStatus(BusinessFeatureTitleBusinessFeatureDictionary purchasedFeatures, PlayerStatIntDictionary playerStats)
    {
        if (purchasedFeatures.ContainsKey(feature.title))
        {
            //set status of this to purchased
            //Destroy(GetComponent<Button>());
            Debug.Log(this + " is purchased");
            Button b = GetComponent<Button>();
            CopyButton(b, isPurchasedButton);

            //call setstatus in all children
            for (int i = 0; i < transform.childCount; i++)
            {
                //skip child if it is the button text object..
                if(transform.GetChild(i).name == "Text") { continue; }
                transform.GetChild(i).GetComponent<BusinessFeatureButtonController>().SetButtonStatus(purchasedFeatures, playerStats);
            }
        }
        else if (feature.CanBePurchased(playerStats))
        {
            //set status of this to purchasable
            //Destroy(GetComponent<Button>());
            Debug.Log(this + " can be purchased");
            Button b = GetComponent<Button>();
            CopyButton(b, purchasableButton);
        }
    }

    public void CopyButton(Button thisButton, Button otherButton)
    {
        thisButton.colors = otherButton.colors;
        thisButton.interactable = otherButton.interactable;
    }

    public void HandleClick()
    {
        PlayerFeatures player = GameObject.Find("Player").GetComponent<PlayerFeatures>();
        player.AddFeature(feature);
        CopyButton(GetComponent<Button>(), isPurchasedButton);
    }


}
