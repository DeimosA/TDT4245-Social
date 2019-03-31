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
}
