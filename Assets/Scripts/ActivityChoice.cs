using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivityChoice", menuName = "Custom/ActivityChoice", order = 1)]
public class ActivityChoice : ScriptableObject
{
    [TextArea]
    public string description;
    [Tooltip("Contains all features that must have been reached for choice to appear")]
    public List<string> featurePrerequisites;
    [Tooltip("Contains all stat prerequisites that must be met for choice to appear")]
    public List<StatPrerequisite> statPrerequisites;
    [Tooltip("Contains all stat changes that will be applied to player's stats upon activation")]
    public List<StatChange> statChanges;
}
