using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessFeatureViewController : MonoBehaviour
{
    public Transform imageGroupScrollViewContent;
    public Button imageGroupUpgradeButton;
    public Transform securityGroupScrollViewContent;
    public Button securityGroupUpgradeButton;
    public Transform socialGroupScrollViewContent;
    public Button socialGroupUpgradeButton;

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
        imageGroupUpgradeButton.interactable = playerFeatures.CanPurchaseNextItemInGroup(0);
        securityGroupUpgradeButton.interactable = playerFeatures.CanPurchaseNextItemInGroup(1);
        socialGroupUpgradeButton.interactable = playerFeatures.CanPurchaseNextItemInGroup(2);
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
        for (int i = 0; i < imageGroupScrollViewContent.childCount; i++)
        {
            imageGroupScrollViewContent.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.imageGroup[i]);
        }
        for (int i = 0; i < securityGroupScrollViewContent.childCount; i++)
        {
            securityGroupScrollViewContent.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.securityGroup[i]);
        }
        for (int i = 0; i < socialGroupScrollViewContent.childCount; i++)
        {
            socialGroupScrollViewContent.GetChild(i).GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.socialGroup[i]);
        }
    }

    //builds UI for feature groups based on playerfeatures data
    private void BuildGroupLists()
    {



        //build group1
        for (int i = 0; i < playerFeatures.imageGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, imageGroupScrollViewContent, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.imageGroup[i]);
        }

        //build group2
        for (int i = 0; i < playerFeatures.securityGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, securityGroupScrollViewContent, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.securityGroup[i]);
        }

        //build group3
        for (int i = 0; i < playerFeatures.socialGroup.Count; i++)
        {
            GameObject element = Instantiate(featureElementPrefab, socialGroupScrollViewContent, false);
            //set element title, element cost and checkmark
            element.GetComponent<FeatureElementController>().SetFeatureElement(playerFeatures.socialGroup[i]);
        }
    }

}
