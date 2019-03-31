using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeatures : MonoBehaviour
{
    public List<BusinessFeature> imageGroup;
    public List<BusinessFeature> securityGroup;
    public List<BusinessFeature> socialGroup;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();   
    }

    //buys the highest available element in group
    public void UpgradeGroup(int groupIndex)
    {
        switch (groupIndex)
        {
            case 0:
                GetNextItemInGroup(imageGroup).SetPurchased(true);
                break;
            case 1:
                GetNextItemInGroup(securityGroup).SetPurchased(true);
                break;
            case 2:
                GetNextItemInGroup(socialGroup).SetPurchased(true);
                break;
            default:
                Debug.LogError(string.Format("{0} did not match any group index", groupIndex), this);
                break;
        }
    }


    //returns stat changes of all purchased features
    public List<StatChange> GetStatChanges()
    {
        List<StatChange> results = new List<StatChange>();
        results.AddRange(GetStatChangesInFeatureGroup(imageGroup));
        results.AddRange(GetStatChangesInFeatureGroup(securityGroup));
        results.AddRange(GetStatChangesInFeatureGroup(socialGroup));
        return results;
    }

    public HashSet<BusinessFeatureTitle> GetPurchasedFeatureTitlesAsHashSet()
    {
        HashSet <BusinessFeatureTitle> results = new HashSet<BusinessFeatureTitle>();
        results.UnionWith(GetPurchasedFeatureTitlesInGroup(imageGroup));
        results.UnionWith(GetPurchasedFeatureTitlesInGroup(securityGroup));
        results.UnionWith(GetPurchasedFeatureTitlesInGroup(socialGroup));
        return results;
    }

    public bool CanPurchaseNextItemInGroup(int groupIndex)
    {

        switch (groupIndex)
        {
            case 0:
                {
                    BusinessFeature nextItem = GetNextItemInGroup(imageGroup);
                    if (nextItem == null) return false;
                    else return nextItem.CanBePurchased(playerStats.stats);
                }
            case 1:
                {
                    BusinessFeature nextItem = GetNextItemInGroup(securityGroup);
                    if (nextItem == null) return false;
                    else return nextItem.CanBePurchased(playerStats.stats);
                }
            case 2:
                {
                    BusinessFeature nextItem = GetNextItemInGroup(socialGroup);
                    if (nextItem == null) return false;
                    else return nextItem.CanBePurchased(playerStats.stats);
                }
            default:
                Debug.LogError(string.Format("{0} did not match any group index", groupIndex), this);
                return false;
        }
    }

    //gets next non-purchased item
    private BusinessFeature GetNextItemInGroup(List<BusinessFeature> featureGroup)
    {
        for(int i = 0; i < featureGroup.Count; i++)
        {
            if (featureGroup[i].purchased) continue;
            else
            {
                return featureGroup[i];
            }
        }
        Debug.Log("All items in group purchased");
        return null;
    }

    private List<StatChange> GetStatChangesInFeatureGroup(List<BusinessFeature> featureGroup)
    {
        List<StatChange> results = new List<StatChange>();
        for (int featureGroupIndex = 0; featureGroupIndex < featureGroup.Count; featureGroupIndex++)
        {
            if (featureGroup[featureGroupIndex].purchased)
            {
                for (int statChangesIndex = 0; statChangesIndex < featureGroup[featureGroupIndex].statChangesPerTurn.Count; statChangesIndex++)
                {
                    results.Add(featureGroup[featureGroupIndex].statChangesPerTurn[statChangesIndex]);
                }
            }
        }

        return results;
    }

    private HashSet<BusinessFeatureTitle> GetPurchasedFeatureTitlesInGroup(List<BusinessFeature> featureGroup)
    {
        HashSet<BusinessFeatureTitle> results = new HashSet<BusinessFeatureTitle>();
        for (int featureGroupIndex = 0; featureGroupIndex < featureGroup.Count; featureGroupIndex++)
        {
            if (featureGroup[featureGroupIndex].purchased)
            {
                results.Add(featureGroup[featureGroupIndex].title);
            }
        }
        return results;
    }

}
