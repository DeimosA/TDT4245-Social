using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyModel
{

    public string companyName;
    public int userCount;
    public int reputation;
    public int cash;

    private List<string> messages;

    public NetworkPlayer networkCompany;
    public NetworkPlayer localCompany;

    private bool commEstablished = false;


    /* CONSTRUCTORS */

    public CompanyModel(string companyName)
    {
        this.companyName = companyName;
        this.userCount = 0;
        this.reputation = 0;
        this.cash = 100;
        messages = new List<string>();

        ///////////
        //TestData();
        ///////////
    }

    public CompanyModel(NetworkPlayer networkPlayer)
    {
        networkCompany = networkPlayer;
        this.companyName = networkCompany.companyName;
        this.userCount = networkCompany.userbase;
        this.reputation = networkCompany.publicOpinion;
        this.cash = networkCompany.capital;
        messages = new List<string>();

        ///////////
        //TestData();
        ///////////
    }


    /* PRIVATE METHODS */

    private void TestData()
    {
        messages.Add("Hei!");
        messages.Add("Det var en gang en lang fortelling om etellerannet. Siden det ikke er plass til å skrive så mye så slutter vi her tenker jeg");
        messages.Add("You: Hei!");
    }

    public bool IsCommEstablished()
    {
        return commEstablished;
    }

    public void SetCommEstablished()
    {
        commEstablished = true;
        // TODO probably transmit some message about this to other company
    }

    public void AddMessage(string message, bool ownMessage = false)
    {
        if (ownMessage)
        {
            messages.Add("You: " + message);
        }
        else
        {
            messages.Add(message);
        }

    }

    public List<string> GetMessages()
    {
        return messages;
    }



}
