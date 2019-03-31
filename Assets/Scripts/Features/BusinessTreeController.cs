using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls business feature tree
/// All tree nodes must be placed as child of this transform
/// Builds tree on open based on player's current features and stats
/// </summary>
public class BusinessTreeController : MonoBehaviour
{
    public List<GameObject> childNodes;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SetTreeButtons();
    }

    //sets button in nodes based on players current stats and features
    //Assumes that the tree is organized in the scene hierarchy
    //i.e. a node Y is a child of node X if feature X is a prerequisite to feature Y
    public void SetTreeButtons()
    {
        for(int i = 0; i < childNodes.Count; i++)
        {
            BusinessFeatureButtonController featureButton = transform.GetChild(i).GetComponent<BusinessFeatureButtonController>();
            featureButton.SetButtonStatus(playerController.playerFeatures.purchasedFeatures, playerController.playerStats.stats);
        }
    }

    public void CloseTree()
    {
        Destroy(gameObject);
    }


}
