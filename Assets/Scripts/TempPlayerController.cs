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

    // Start is called before the first frame update
    void Start()
    {
        choiceHistory = new CardIntDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillHand()
    {
        playerHandController.FillHand(currentTurn, choiceHistory, playerStats.stats, playerFeatures.features);
    }
}
