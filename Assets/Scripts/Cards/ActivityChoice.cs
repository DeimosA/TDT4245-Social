﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ActivityChoice", menuName = "Custom/ActivityChoice", order = 1)]
[System.Serializable]
public class ActivityChoice
{
    //TODO: method for applying choice
    public string title;
    [TextArea]
    public string description;
    public int addToNewsCounter;
    [Tooltip("Reference a card here if this choice should guarantee this card to trigger")]
    public ActivityCard priorityCardToTrigger;
    [Tooltip("Contains all features that must have been reached for choice to appear")]
    public List<FeaturePrerequisiste> featurePrerequisites;
    [Tooltip("Contains all stat prerequisites that must be met for choice to appear")]
    public List<StatPrerequisite> statPrerequisites;
    [Tooltip("Contains all stat changes that will be applied to player's stats upon activation")]
    public List<StatChange> statChanges;

    public ActivityChoice(string title)
    {
        this.title = title;
    }

    //returns whether all prerequisites are met
    public bool ValidateChoice(HashSet<BusinessFeatureTitle> purchasedFeatures, PlayerStatIntDictionary playerStats)
    {
        return (ValidateFeaturePrerequisites(purchasedFeatures)
            && ValidateStatPrerequisites(playerStats));
    }

    //Return whether this card has a priority card to trigger
    public bool HasPriorityCard()
    {
        return (priorityCardToTrigger != null);
    }


    //Input: List of player's unlocked features
    //Checks if featurePrerequisites match player's features
    private bool ValidateFeaturePrerequisites(HashSet<BusinessFeatureTitle> purchasedFeatures)
    {
        //iterate over all feature prerequisites, return false if any does not match input
        foreach (FeaturePrerequisiste prerequisite in featurePrerequisites)
        {
            //if the card requires the feature to be activated, return false if it is not held by player
            if (prerequisite.value == true)
            {
                if (!purchasedFeatures.Contains(prerequisite.feature)) return false;
            }
            else //card requires feature to not be activated, return false if it is held by player
            {
                if (purchasedFeatures.Contains(prerequisite.feature)) return false;
            }
        }
        //no mismatches, card's feature prerequisites have been met
        return true;
    }

    //input: Dictionary representing player's stats
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
}
