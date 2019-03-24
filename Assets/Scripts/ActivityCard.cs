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
    public StringBoolDictionary featurePrerequisites;
    [Tooltip("Contains stat prerequisites that must be met for card to be added to deck")]
    public List<StatPrerequisite> statPrerequisites;
    [Tooltip("Contains any Event/Choice pairs that must have been met for card to be added to deck")]
    public CardChoiceDictionary choicePrerequisites;

    //returns whether all prerequisites are met
    public bool ValidateCard(int currentTurn, List<string> features, PlayerStatIntDictionary playerStats, CardChoiceDictionary choices)
    {
        return (ValidateTurn(currentTurn) && ValidateFeaturePrerequisites(features)
            && ValidateStatPrerequisites(playerStats) && ValidateChoicePrerequisites(choices));
    }

    //check if turn requirement has been met
    private bool ValidateTurn(int currentTurn)
    {
        return (currentTurn >= minimumTurn);
    }

    //Input: List of player's unlocked features
    //Checks if featurePrerequisites match player's features
    private bool ValidateFeaturePrerequisites(List<string> features)
    {
        //iterate over all feature prerequisites, return false if any does not match input
        foreach(string key in featurePrerequisites.Keys)
        {
            //if the card requires the feature to be activated, return false if it is not held by player
            if(featurePrerequisites[key] == true)
            {
                if (!features.Contains(key)) return false;
            }
            else //card requires feature to not be activated, return false if it is held by player
            {
                if (features.Contains(key)) return false;
            }
        }
        //no mismatches, card's feature prerequisites have been met
        return true;
    }

    private bool ValidateStatPrerequisites(PlayerStatIntDictionary playerStats)
    {
        //iterate over every prerequisite, check if it matches what is in input dict
        foreach (StatPrerequisite statPrerequisite in statPrerequisites)
        {
            if (statPrerequisite.statMustBeEqualOrGreater)
            {
                if (playerStats[statPrerequisite.stat] < statPrerequisite.targetValue) return false;
            }
            else
            {
                if (playerStats[statPrerequisite.stat] >= statPrerequisite.targetValue) return false;
            }
        }

        //no mismatches found, card's statprerequisites are met
        return true;
    }

    //input: dictionary containing all Card+Choice pairs player has played currently
    private bool ValidateChoicePrerequisites(CardChoiceDictionary playerCardChoiceHistory)
    {
        //iterate over every prerequisite, validate that player has played the card and made the required choice
        foreach(ActivityCard key in choicePrerequisites.Keys)
        {
            //check if player has played card
            if (playerCardChoiceHistory.ContainsKey(key))
            {
                //check if player has made the required choice
                if (!(playerCardChoiceHistory[key].Equals(choicePrerequisites[key])))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //No mismatches found, card's prerequisites are met
        return true;
    }
}
