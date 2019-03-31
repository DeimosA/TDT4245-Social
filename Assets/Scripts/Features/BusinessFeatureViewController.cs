using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessFeatureViewController : MonoBehaviour
{
    public Transform imageGroupUI;
    public Transform securityGroupUI;
    public Transform socialGroupUI;

    [Header("Prefabs")]
    public GameObject featureElementPrefab;

    private PlayerFeatures playerFeatures;


    // Start is called before the first frame update
    void Start()
    {
        playerFeatures = GameObject.Find("Player").GetComponent<PlayerFeatures>();
        BuildGroupLists();

        //set status of upgrade buttons
        SetUpgradeButtonsInteractable();


    }

    public void CloseView()
    {
        Destroy(gameObject);
    }

    private void SetUpgradeButtonsInteractable()
    {
        imageGroupUI.Find("UpgradeButton").GetComponent<Button>().interactable = playerFeatures.CanPurchaseNextItemInGroup(0);
        securityGroupUI.Find("UpgradeButton").GetComponent<Button>().interactable = playerFeatures.CanPurchaseNextItemInGroup(1);
        socialGroupUI.Find("UpgradeButton").GetComponent<Button>().interactable = playerFeatures.CanPurchaseNextItemInGroup(2);
    }

    public void BuyFeature(int groupIndex)
    {
        playerFeatures.UpgradeGroup(groupIndex);
        //update all lists (inefficient, but no time to fix)
        UpdateGroupLists();
        SetUpgradeButtonsInteractable();
    }

    private void UpdateGroupLists()
    {
        for (int i = 2; i < imageGroupUI.childCount; i++)
        {
            imageGroupUI.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.imageGroup[i-2]);
        }
        for (int i = 2; i < securityGroupUI.childCount; i++)
        {
            securityGroupUI.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.securityGroup[i-2]);
        }
        for (int i = 2; i < socialGroupUI.childCount; i++)
        {
            socialGroupUI.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.socialGroup[i-2]);
        }
    }

    //builds UI for feature groups based on playerfeatures data
    private void BuildGroupLists()
    {



        //build group1
        for (int i = 0; i < playerFeatures.imageGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, imageGroupUI, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.imageGroup[i]);
        }

        //build group2
        for (int i = 0; i < playerFeatures.securityGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, securityGroupUI, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.securityGroup[i]);
        }

        //build group3
        for (int i = 0; i < playerFeatures.socialGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, socialGroupUI, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.socialGroup[i]);
        }
    }

}
