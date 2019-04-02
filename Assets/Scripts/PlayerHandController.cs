using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maintains player's hand of cards
/// </summary>
public class PlayerHandController : MonoBehaviour
{
    public PlayerController playerController;
    public CardDeckController deck;
    //public List<GameObject> hand;
    //public List<GameObject> playSlots;

    //Transforms containing card elements in UI
    private Transform handTransform;
    private Transform playSlotsTransform;

    //Prefab to be instantiated when drawing new card from deck
    [Header("Prefabs")]
    public GameObject individualCardPrefab;
    public GameObject coopCardPrefab;
    public GameObject coopRequestPrefab;

    public int maxCardsInHand;
    public int maxCardsInPlaySlots;

    // Start is called before the first frame update
    void Start()
    {
        handTransform = GameObject.Find("UIPlayerHand").transform;
        playSlotsTransform = GameObject.Find("UIPlaySlots").transform;
    }

    public List<GameObject> GetCardsInPlaySlots()
    {
        List<GameObject> result = new List<GameObject>();
        for(int i = 0; i < playSlotsTransform.childCount; i++)
        {
            result.Add(playSlotsTransform.GetChild(i).gameObject);
        }
        return result;
    }

    public void MoveCardToPlaySlot(GameObject card)
    {
        if (playSlotsTransform.childCount < maxCardsInPlaySlots)
        {
            card.transform.SetParent(playSlotsTransform);
            //update interactable status in all cards
            SetInteractableInCards();
        }
        else
        {
            Debug.Log("Move denied, play slots are full.", this);
        }
    }

    //move card to play slot, replace first card if play slot is full
    public void ReplaceCardInPlaySlot(GameObject card)
    {
        if(playSlotsTransform.childCount >= maxCardsInPlaySlots)
        {
            MoveCardToHand(playSlotsTransform.GetChild(0).gameObject);
        }
        MoveCardToPlaySlot(card);
    }

    public void MoveCardToHand(GameObject card)
    {
        if (handTransform.childCount < maxCardsInHand)
        {
            card.transform.SetParent(handTransform);
            //update interactable status in all cards
            SetInteractableInCards();
        }
        else
        {
            Debug.Log("Move denied, Hand is full", this);
        }
    }

    public void FillHand(int currentTurn, CardIntDictionary choiceHistory, PlayerStatIntDictionary playerStats, HashSet<BusinessFeatureTitle> purchasedFeatures)
    {
        //Gets card asset from deck, instantiates new cards with that asset
        //Until hand is full
        while(handTransform.childCount < maxCardsInHand)
        {
            ActivityCard newCard = deck.GetCard(currentTurn, choiceHistory, playerStats, purchasedFeatures);
            if(newCard == null)
            {
                Debug.Log("No new cards found");
                break;
            }

            //spawn coop or individual card

            GameObject c = newCard.cooperative ? Instantiate(coopCardPrefab, handTransform, false) : Instantiate(individualCardPrefab, handTransform, false);
            c.GetComponent<CardController>().cardData = newCard;
            c.GetComponent<CardController>().playerHandController = this;
        }

        //Set interactable status in all cards
        SetInteractableInCards();
    }

    //set interactable status in all cards present
    public void SetInteractableInCards()
    {
        bool fullHand = handTransform.childCount >= maxCardsInHand;
        bool fullPlaySlots = playSlotsTransform.childCount >= maxCardsInPlaySlots;

        for(int i = 0; i < handTransform.childCount; i++)
        {
            try
            {
                handTransform.GetChild(i).GetComponent<CardController>().SetButtonsInteractable(fullPlaySlots, fullHand);
            }
            catch (System.NullReferenceException){
                Debug.Log("No cardcontroller found", this);
            }
        }

        for (int i = 0; i < playSlotsTransform.childCount; i++)
        {
            try
            {
                playSlotsTransform.GetChild(i).GetComponent<CardController>().SetButtonsInteractable(fullPlaySlots, fullHand);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("No cardcontroller found", this);
            }
        }
    }

    //TODO: init with proper data
    public void ReceiveCoopRequest()
    {
        GameObject g = Instantiate(coopRequestPrefab, GameObject.Find("MainCanvas").transform, false);
        g.GetComponent<CooperationInviteController>().Init();
    }
}
