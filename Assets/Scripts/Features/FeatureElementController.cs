using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureElementController : MonoBehaviour
{
    public Text title;
    public Text cost;
    public Image checkmarkImage;

    public void SetFeatureElement(BusinessFeature feature)
    {
        title.text = feature.title.ToString();
        cost.text = "Cost: " + feature.cost.ToString();
        checkmarkImage.enabled = feature.purchased;
    }
}
