using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndividualCardController : CardController
{
    private Button moveToPlaySlotButton;
    private Button moveToHandButton;

    public override void OnStart()
    {
        moveToPlaySlotButton = transform.Find("MoveToPlaySlotButton").GetComponent<Button>();
        moveToHandButton = transform.Find("MoveToHandButton").GetComponent<Button>();

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
    }

    public override void SetButtonsInteractable(bool fullPlaySlot, bool fullHand)
    {
        if (fullPlaySlot)
        {
            moveToPlaySlotButton.interactable = false;
        }
        else
        {
            moveToPlaySlotButton.interactable = true;
        }

        if (fullHand)
        {
            moveToHandButton.interactable = false;
        }
        else
        {
            moveToHandButton.interactable = true;
        }
    }

}
