using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardController : MonoBehaviour
{
    public ActivityCard cardData;
    public PlayerHandController playerHandController;

    protected Dropdown choiceDropdown;


    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    public abstract void OnStart();

    //set buttons interactable based on whether playslot or hand is full
    public abstract void SetButtonsInteractable(bool fullPlaySlots, bool fullHand);

    //Temporary way of getting which choice is currently chosen
    public int GetIndexOfHighlightedChoice()
    {
        return choiceDropdown.value;
    }

    public string GetTitleOfHighlightedChoice()
    {
        Debug.Log("Choice title: " + choiceDropdown.options[choiceDropdown.value].text);
        return choiceDropdown.options[choiceDropdown.value].text;
    }

    public ActivityChoice GetHighlightedChoice()
    {
        return cardData.GetChoice(GetTitleOfHighlightedChoice());
    }

    public void MoveToPlaySlot()
    {
        playerHandController.MoveCardToPlaySlot(gameObject);
    }

    public void MoveToHand()
    {
        playerHandController.MoveCardToHand(gameObject);
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }


}
