using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonController : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        }
        catch (System.NullReferenceException){
            Debug.Log("Player not found");
        }
    }

    public void OnClick()
    {
        if(playerController == null)
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        playerController.EndTurn();
    }
}
