using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BusinessFeatureTitle
{
    FeatureA,
    FeatureB,
    FeatureC,
    FeatureD,
    FeatureE,
    FeatureF,
    FeatureG,
    FeatureH
}

[System.Serializable]
public class BusinessFeature
{
    public BusinessFeatureTitle title;
    public int cost;
    public bool purchased;
    public List<StatChange> statChangesPerTurn;


    public bool CanBePurchased(PlayerStatIntDictionary playerStats)
    {
        return (playerStats[PlayerStat.capital] >= cost);
    }

    public void SetPurchased(bool purchased)
    {
        this.purchased = purchased;
    }


}
