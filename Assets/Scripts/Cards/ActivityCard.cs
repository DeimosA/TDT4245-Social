using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoicePrerequisite
{
    public ActivityCard card;
    public int choiceIndex;

}

[System.Serializable]
public class FeaturePrerequisiste
{
    public BusinessFeatureTitle feature;
    public bool value;
}

public enum CardCategory
{
    Default,
    BusinessDeal,
    Lobby,
    PressRelease,
    Philanthropy
}

[CreateAssetMenu(fileName = "ActivityCard", menuName = "Custom/ActivityCard", order = 1)]
public class ActivityCard : ScriptableObject
{
    public bool cooperative;
    public CardCategory cardCategory = CardCategory.Default;
    public string description;
    public List<ActivityChoice> choices = new List<ActivityChoice>();
    [Tooltip("Game must have reached this turn for this card to be added to a deck")]
    public int minimumTurn;
    [Tooltip("Contains all features that must have been reached for card to be added to deck")]
    public List<FeaturePrerequisiste> featurePrerequisites = new List<FeaturePrerequisiste>();
    [Tooltip("Contains stat prerequisites that must be met for card to be added to deck")]
    public List<StatPrerequisite> statPrerequisites = new List<StatPrerequisite>();
    [Tooltip("Contains any Event/Choice pairs that must have been met for card to be added to deck")]
    public List<ChoicePrerequisite> choicePrerequisites = new List<ChoicePrerequisite>();

    //returns whether all prerequisites are met
    public bool ValidateCard(int currentTurn, HashSet<BusinessFeatureTitle> purchasedFeatures, PlayerStatIntDictionary playerStats, CardIntDictionary choiceHistory)
    {
        //not valid if player has played card before
        if (choiceHistory.ContainsKey(this))
        {
            return false;
        }
        //otherwise check all prerequisites
        return (ValidateTurn(currentTurn) && ValidateFeaturePrerequisites(purchasedFeatures)
            && ValidateStatPrerequisites(playerStats) && ValidateChoicePrerequisites(choiceHistory));
    }

    //check if turn requirement has been met
    private bool ValidateTurn(int currentTurn)
    {
        return (currentTurn >= minimumTurn);
    }

    //Input: List of player's unlocked features
    //Checks if featurePrerequisites match player's features
    private bool ValidateFeaturePrerequisites(HashSet<BusinessFeatureTitle> purchasedFeatures)
    {
        //iterate over all feature prerequisites, return false if any does not match input
        foreach(FeaturePrerequisiste prerequisite in featurePrerequisites)
        {
            //if the card requires the feature to be activated, return false if it is not held by player
            if(prerequisite.value == true)
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
    private bool ValidateChoicePrerequisites(CardIntDictionary playerCardChoiceHistory)
    {
        //iterate over every prerequisite, validate that player has played the card and made the required choice
        foreach(ChoicePrerequisite prerequisite in choicePrerequisites)
        {
            //check if player has played card
            if (playerCardChoiceHistory.ContainsKey(prerequisite.card))
            {
                //check if player has made the required choice
                if (playerCardChoiceHistory[prerequisite.card] != prerequisite.choiceIndex)
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

    public int GetNumberOfChoices()
    {
        return choices.Count;
    }

    public int GetChoiceIndex(ActivityChoice c)
    {
        return choices.IndexOf(c);
    }

    public ActivityChoice GetChoice(int index)
    {
        return choices[index];
    }

    public ActivityChoice GetChoice(string title)
    {
        for(int i = 0; i < choices.Count; i++)
        {
            if(choices[i].title == title)
            {
                return choices[i];
            }
        }
        return null;
    }

    public string GetChoiceTitleByIndex(int index)
    {
        return choices[index].title;
    }

    public string[] GetChoiceTitlesArray()
    {
        string[] result = new string[choices.Count];

        for(int i = 0; i < choices.Count; i++)
        {
            result[i] = GetChoiceTitleByIndex(i);
        }

        return result;
    }
}
