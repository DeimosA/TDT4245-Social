using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the card deck
/// Maintains queue of cards to be given to player
/// </summary>
public class CardDeckController : MonoBehaviour
{
    public Queue<ActivityCard> priorityCards;
    [Tooltip("Path of folder containing cards to build deck from (relative to Resources)")]
    public string deckFolderPath = "ActivityCards/FinalCards";
    private List<ActivityCard> deck = new List<ActivityCard>();

    // Start is called before the first frame update
    void Start()
    {
        priorityCards = new Queue<ActivityCard>();

        //build deck from folder
        var cardObjects = Resources.LoadAll(deckFolderPath, typeof(ActivityCard));
        foreach(Object o in cardObjects)
        {
            deck.Add((ActivityCard)o);
        }
    }

    //Public methods
    public void AddToQueue(ActivityCard card)
    {
        priorityCards.Enqueue(card);
    }

    //Gets valid card based on Player's stats, features and history
    public ActivityCard GetCard(int currentTurn, CardIntDictionary choiceHistory, PlayerStatIntDictionary playerStats, HashSet<BusinessFeatureTitle> purchasedFeatures)
    {
        ActivityCard result = null;

        //Check priority queue first
        if(priorityCards.Count > 0)
        {
            var queueCard = priorityCards.Dequeue();
            if(queueCard.ValidateCard(currentTurn, purchasedFeatures, playerStats, choiceHistory)){
                result = queueCard;
            }
        }
        else //otherwise find random valid card from list of all cards
        {
            //super duper excellent algorithm for getting random card..
            HashSet<int> used = new HashSet<int>();
            while (result == null && used.Count < deck.Count)
            {
                int index = Random.Range(0, deck.Count);
                if (!used.Contains(index))
                {
                    used.Add(index);
                    if (deck[index].ValidateCard(currentTurn, purchasedFeatures, playerStats, choiceHistory))
                    {
                        result = deck[index];
                        deck.RemoveAt(index);
                    }
                }
            }
            //for(int i = 0; i < deck.Count; i++)
            //{

            //}
        }
        if(result != null)
        {
            return result;
        }
        else
        {
            //No cards returned..
            Debug.Log("No valid cards were found", this);
            return null;
        }
    }

    //Private methods
}
