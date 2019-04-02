using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewsFeedItem", menuName = "Custom/NewsFeedItem", order = 1)]
public class NewsFeedItem : ScriptableObject
{
    public string header;
    public string content;

    //Prerequisites:
    //- newscounter must be >= newsCounterminimum
    //- any choiceprerequisites must be met
    public int newsCounterMinimum;
    [Tooltip("Contains any Event/Choice pairs that must have been met for newsfeeditem to be added to fired")]
    public List<ChoicePrerequisite> choicePrerequisites = new List<ChoicePrerequisite>();


    public bool ValidateItem(int playerNewsCounter, CardIntDictionary playerChoiceHistory)
    {
        return (playerNewsCounter >= newsCounterMinimum && ValidateChoicePrerequisites(playerChoiceHistory));
    }

    //input: dictionary containing all Card+Choice pairs player has played currently
    private bool ValidateChoicePrerequisites(CardIntDictionary playerChoiceHistory)
    {
        //iterate over every prerequisite, validate that player has played the card and made the required choice
        foreach (ChoicePrerequisite prerequisite in choicePrerequisites)
        {
            //check if player has played card
            if (playerChoiceHistory.ContainsKey(prerequisite.card))
            {
                //check if player has made the required choice
                if (playerChoiceHistory[prerequisite.card] != prerequisite.choiceIndex)
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
