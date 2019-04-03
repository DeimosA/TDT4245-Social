using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeatureElementController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI cost;
    public Image checkmarkImage;

    public void SetFeatureElement(BusinessFeature feature)
    {
        title.text = feature.title.ToString();
        cost.text = "Cost: " + feature.cost.ToString();
        checkmarkImage.enabled = feature.purchased;
    }
}
