using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public ActivityCard cardData;
    public PlayerHandController playerHandController;


    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Text").GetComponent<Text>().text = cardData.description;
    }

    // Update is called once per frame
    void Update()
    {
        
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
