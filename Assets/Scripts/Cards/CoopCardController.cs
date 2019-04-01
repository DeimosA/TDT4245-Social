using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CoopCardController : CardController
{
    //disable some stuff if this card is receiving
    public bool receivingCard;

    private Dropdown playerDropdown;
    private Button sendButton;

    private bool sent = false;


    public void SendInvite()
    {
        //TODO: Send the card to the player chosen in playerdropdown

        //TODO: Deactivate choice and player dropdowns, display something else instead..

        //move to play slot, remove a card from playslot if needed..
        MoveToPlaySlot();

        sendButton.interactable = false;
        sent = true;

    }


    public override void OnStart()
    {
        if (!receivingCard)
        {

            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = cardData.description;

            choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<Dropdown>();


            //Temporary way of displaying valid choices
            for (int i = 0; i < cardData.choices.Count; i++)
            {
                if (playerHandController.playerController.ValidateChoice(cardData.choices[i]))
                {
                    choiceDropdown.options.Add(new Dropdown.OptionData(cardData.choices[i].title));
                }
            }

            sendButton = transform.Find("SendButton").GetComponent<Button>();

            //set player dropdown options
            playerDropdown = transform.Find("PlayerDropdown").GetComponent<Dropdown>();

            //TODO: Get player company names and add them here..
            playerDropdown.options.Add(new Dropdown.OptionData("MockPlayer1"));
            playerDropdown.options.Add(new Dropdown.OptionData("MockPlayer2"));
            playerDropdown.options.Add(new Dropdown.OptionData("MockPlayer3"));

        }
        else
        {
            choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<Dropdown>();
        }
    }

    public void SetCardData(ActivityCard activityCard)
    {
        this.cardData = activityCard;
        choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<Dropdown>();
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = cardData.description;
        //Temporary way of displaying valid choices
        for (int i = 0; i < cardData.choices.Count; i++)
        {
            choiceDropdown.options.Add(new Dropdown.OptionData(cardData.choices[i].title));
        }

    }

    public void HighlightChoice(int choiceIndex)
    {
        if(choiceIndex >= choiceDropdown.options.Count)
        {
            Debug.LogError("Tried highlighting choice outside of dropdown range");
            return;
        }
        else
        {
            choiceDropdown.value = choiceIndex;
        }
    }


    public override void SetButtonsInteractable(bool fullPlaySlot, bool fullHand)
    {
        if (sent || receivingCard) return;

        if (fullPlaySlot)
        {
            sendButton.interactable = false;
        }
        else
        {
            sendButton.interactable = true;
        }
    }

    public void MoveToPlaySlotWithPriority()
    {
        playerHandController.ReplaceCardInPlaySlot(gameObject);
    }

    public void SetPlayerHandController(PlayerHandController playerHandController)
    {
        this.playerHandController = playerHandController;
    }
}
