using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivityCard", menuName = "Custom/ActivityCard", order = 1)]
public class ActivityCard : ScriptableObject
{
    [TextArea]
    public string description;
    public List<ActivityChoice> choices;
    [Tooltip("Game must have reached this turn for this card to be added to a deck")]
    public int minimumTurn;
    [Tooltip("Contains all features that must have been reached for card to be added to deck")]
    public List<string> featurePrerequisites;
    [Tooltip("Contains stat prerequisites that must be met for card to be added to deck")]
    public List<StatPrerequisite> statPrerequisites;
    [Tooltip("Contains any Event/Choice pairs that must have been met for card to be added to deck")]
    public CardChoiceDictionary choicePrerequisites;

}
