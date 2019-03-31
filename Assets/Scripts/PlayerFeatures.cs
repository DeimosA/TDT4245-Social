using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeatures : MonoBehaviour
{
    public BusinessFeatureTitleBusinessFeatureDictionary purchasedFeatures;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddFeature(BusinessFeature feature)
    {
        purchasedFeatures.Add(feature.title, feature);
    }

    public List<StatChange> GetStatChanges()
    {
        List<StatChange> results = new List<StatChange>();
        foreach(BusinessFeature feature in purchasedFeatures.Values)
        {
            for(int i = 0; i < feature.statChangesPerTurn.Count; i++)
            {
                results.Add(feature.statChangesPerTurn[i]);
            }
        }

        return results;
    }
}
