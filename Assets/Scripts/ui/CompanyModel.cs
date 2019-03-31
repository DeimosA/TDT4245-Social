using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyModel
{

    public string companyName;
    public int userCount;
    public int reputation;
    public int cash;

    public List<string> messages;

    private bool commEstablished = false;

    public CompanyModel(string companyName)
    {
        this.companyName = companyName;
        this.userCount = 0;
        this.reputation = 0;
        this.cash = 100;
        messages = new List<string>();

        ///////////
        TestData();
        ///////////
    }

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



}
