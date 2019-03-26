﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maintains player's hand of cards
/// </summary>
public class PlayerHandController : MonoBehaviour
{
    public CardDeckController deck;
    //public List<GameObject> hand;
    //public List<GameObject> playSlots;

    //Transforms containing card elements in UI
    public Transform handTransform;
    public Transform playSlotsTransform;

    //Prefab to be instantiated when drawing new card from deck
    public GameObject cardPrefab;

    public int maxCardsInHand;
    public int maxCardsInPlaySlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveCardToPlaySlot(GameObject card)
    {
        if (playSlotsTransform.childCount < maxCardsInPlaySlots)
        {
            card.transform.SetParent(playSlotsTransform);
        }
        else
        {
            Debug.Log("Move denied, play slots are full.", this);
        }
    }

    public void MoveCardToHand(GameObject card)
    {
        if (handTransform.childCount < maxCardsInHand)
        {
            card.transform.parent = handTransform;
        }
        else
        {
            Debug.Log("Move denied, Hand is full", this);
        }
    }

    public void FillHand(int currentTurn, CardIntDictionary choiceHistory, PlayerStatIntDictionary playerStats, List<string> playerFeatures)
    {
        //Gets card asset from deck, instantiates new cards with that asset
        //Until hand is full
        while(handTransform.childCount < maxCardsInHand)
        {
            ActivityCard newCard = deck.GetCard(currentTurn, choiceHistory, playerStats, playerFeatures);
            if(newCard == null)
            {
                Debug.Log("No new cards found");
                break;
            }
            GameObject c = Instantiate(cardPrefab, handTransform, false);
            c.GetComponent<CardController>().cardData = newCard;
            c.GetComponent<CardController>().playerHandController = this;
        }
    }
}
