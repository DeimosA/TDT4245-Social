using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerFeatures playerFeatures;
    public CardIntDictionary choiceHistory;
    public int currentTurn;
    public PlayerHandController playerHandController;
    public CardDeckController deck;

    // Start is called before the first frame update
    void Start()
    {
        choiceHistory = new CardIntDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Commit all cards in player's play slots
    public void EndTurn()
    {
        List<GameObject> cardsInPlaySlots = playerHandController.GetCardsInPlaySlots();
        for(int i = 0; i < cardsInPlaySlots.Count; i++)
        {
            CardController cardController = cardsInPlaySlots[i].GetComponent<CardController>();
            choiceHistory.Add(cardController.cardData, cardController.GetIndexOfHighlightedChoice());

            //apply stat changes
            ActivityChoice choice = cardController.GetHighlightedChoice();
            for(int j = 0; j < choice.statChanges.Count; j++)
            {
                playerStats.AddToStat(choice.statChanges[j]);
            }

            //Add any potential priority cards to queue
            if (choice.HasPriorityCard())
            {
                deck.AddToQueue(choice.priorityCardToTrigger);
            }

            Destroy(cardsInPlaySlots[i]);
        }
    }

    public void FillHand()
    {
        playerHandController.FillHand(currentTurn, choiceHistory, playerStats.stats, playerFeatures.features);
    }

    public bool ValidateChoice(ActivityChoice choice)
    {
        return choice.ValidateChoice(playerFeatures.features, playerStats.stats);
    }
}
