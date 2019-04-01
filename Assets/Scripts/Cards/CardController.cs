using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public ActivityCard cardData;
    public PlayerHandController playerHandController;

    private Dropdown dropdown;


    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Text").GetComponent<TextMeshProUGUI>().text = cardData.description;

        dropdown = transform.Find("Dropdown").GetComponent<Dropdown>();

        //Temporary way of displaying valid choices
        for(int i = 0; i < cardData.choices.Count; i++)
        {
            if (playerHandController.playerController.ValidateChoice(cardData.choices[i]))
            {
                dropdown.options.Add(new Dropdown.OptionData(cardData.choices[i].title));
            }
        }
    }

    //Temporary way of getting which choice is currently chosen
    public int GetIndexOfHighlightedChoice()
    {
        return dropdown.value;
    }

    public ActivityChoice GetHighlightedChoice()
    {
        return cardData.GetChoiceByIndex(GetIndexOfHighlightedChoice());
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
