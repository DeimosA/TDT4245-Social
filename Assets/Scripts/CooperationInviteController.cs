using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooperationInviteController : MonoBehaviour
{
    public TextMeshProUGUI companyNameText;
    public CoopCardController coopCardController;

    [Header("Debug")]
    public string sendingPlayerCompanyName;
    public ActivityCard card;
    public int choiceIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Init(sendingPlayerCompanyName, card, choiceIndex);
    }

    //FOR TESTING; DELETE
    public void Init()
    {
        Init(sendingPlayerCompanyName, card, choiceIndex);
    }

    //set values
    //Input: sending player string, activity card to be cooperated on, choice index highlighted by sending player
    public void Init(string sendingPlayerCompanyName, ActivityCard activityCard, int choiceIndex)
    {
        companyNameText.text = sendingPlayerCompanyName;

        coopCardController.SetCardData(activityCard);
        coopCardController.HighlightChoice(choiceIndex);
        coopCardController.SetPlayerHandController(GameObject.Find("Player").GetComponent<PlayerHandController>());
    }

    /// <summary>
    /// Remove popup
    /// TODO: Notify sending player?
    /// </summary>
    public void DeclineCard()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Add card to receiving player's play slot
    /// TODO: notify sending player?
    /// </summary>
    public void AcceptCard()
    {
        coopCardController.MoveToPlaySlotWithPriority();
        Destroy(gameObject);
    }

}
