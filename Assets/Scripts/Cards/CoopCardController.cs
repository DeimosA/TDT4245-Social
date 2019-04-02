using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CoopCardController : CardController
{
    //disable some stuff if this card is receiving
    public bool receivingCard;

    private TMP_Dropdown playerDropdown;
    private Button sendButton;

    private bool sent = false;


    public void SendInvite()
    {
        //TODO: Send the card to the player chosen in playerdropdown


        //Deactivate card elements
        playerDropdown.interactable = false;
        choiceDropdown.interactable = false;

        //move to play slot
        MoveToPlaySlotWithPriority();

        sendButton.interactable = false;
        sendButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Invite sent";
        sent = true;

    }


    public override void OnStart()
    {
        if (!receivingCard)
        {

            //set type text
            transform.Find("TypeText").GetComponent<TextMeshProUGUI>().text = cardData.cardCategory.ToString();

            //set description text
            transform.Find("DescriptionScrollView/Viewport/Content/DescriptionText").GetComponent<TextMeshProUGUI>().text = cardData.description;


            choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<TMP_Dropdown>();


            //Temporary way of displaying valid choices
            for (int i = 0; i < cardData.choices.Count; i++)
            {
                if (playerHandController.playerController.ValidateChoice(cardData.choices[i]))
                {
                    choiceDropdown.options.Add(new TMP_Dropdown.OptionData(cardData.choices[i].title));
                }
            }


            //force label to update
            choiceDropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = choiceDropdown.options[0].text;

            sendButton = transform.Find("SendButton").GetComponent<Button>();

            //set player dropdown options
            playerDropdown = transform.Find("PlayerDropdown").GetComponent<TMP_Dropdown>();

            //TODO: Get player company names and add them here..
            playerDropdown.options.Add(new TMP_Dropdown.OptionData("MockPlayer1"));
            playerDropdown.options.Add(new TMP_Dropdown.OptionData("MockPlayer2"));
            playerDropdown.options.Add(new TMP_Dropdown.OptionData("MockPlayer3"));

        }
        else
        {
            choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<TMP_Dropdown>();
            choiceDropdown.interactable = false;
        }
    }

    public void SetCardData(ActivityCard activityCard)
    {
        this.cardData = activityCard;
        choiceDropdown = transform.Find("ChoiceDropdown").GetComponent<TMP_Dropdown>();
        //set type text
        transform.Find("TypeText").GetComponent<TextMeshProUGUI>().text = cardData.cardCategory.ToString();

        //set description text
        transform.Find("DescriptionScrollView/Viewport/Content/DescriptionText").GetComponent<TextMeshProUGUI>().text = cardData.description;

        //Temporary way of displaying valid choices
        for (int i = 0; i < cardData.choices.Count; i++)
        {
            choiceDropdown.options.Add(new TMP_Dropdown.OptionData(cardData.choices[i].title));
        }

        //force label to update
        choiceDropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = choiceDropdown.options[0].text;

    }

    //Highlight choice by choice index
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

    //Highlight choice matching title of input choice
    public void HighlightChoice(ActivityChoice choice)
    {
        for(int i = 0; i < choiceDropdown.options.Count; i++)
        {
            if(choice.title == choiceDropdown.options[i].text)
            {
                choiceDropdown.value = i;
                return;
            }
        }

        Debug.Log("Choice not found in choicedropdown options");
        
    }


    public override void SetButtonsInteractable(bool fullPlaySlot, bool fullHand, bool cardInHand)
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

        //Remove any duplicates in hand here...
        playerHandController.CheckForDuplicatesInHand(cardData);
    }

    public void SetPlayerHandController(PlayerHandController playerHandController)
    {
        this.playerHandController = playerHandController;
    }
}
