using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndividualCardController : CardController
{
    private Button moveButton;

    public override void OnStart()
    {
        moveButton = transform.Find("MoveButton").GetComponent<Button>();


        //set type text
        transform.Find("TypeText").GetComponent<TextMeshProUGUI>().text = cardData.cardCategory.ToString();

        //set description text
        transform.Find("DescriptionScrollView/Viewport/Content/DescriptionText").GetComponent<TextMeshProUGUI>().text = cardData.description;

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

    public override void SetButtonsInteractable(bool fullPlaySlot, bool fullHand, bool cardInHand)
    {
        if (cardInHand)
        {
            if (fullPlaySlot)
            {
                moveButton.interactable = false;
            }
            else
            {
                moveButton.interactable = true;
            }
        }
        else
        {
            if (fullHand)
            {
                moveButton.interactable = false;
            }
            else
            {
                moveButton.interactable = true;
            }
        }
    }

}
