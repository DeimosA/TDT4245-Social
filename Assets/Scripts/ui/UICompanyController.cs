﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UICompanyController : MonoBehaviour
{

    
    public GameObject establishCommDialogPrefab;
    public GameObject messageDialogPrefab;

    public TextMeshProUGUI companyNameText;
    public TextMeshProUGUI commButtonText;

    private CompanyModel company;
    private GameObject mainCanvas;
    private GameObject commDialog;


    // Start is called before the first frame update
    void Start()
    {
        //companyNameText = transform.Find("CompanyNameText").GetComponent<TextMeshProUGUI>();
        mainCanvas = GameObject.Find("MainCanvas");
        //commButtonText = transform.Find("CommButtonText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCompanyName(string newCompanyName)
    {
        //if (companyNameText == null)
        //{
        //    companyNameText = transform.Find("CompanyNameText").GetComponent<TextMeshProUGUI>();
        //}
        this.companyNameText.SetText(newCompanyName);
    }

    private void ShowChatDialog()
    {
        commDialog = Instantiate(messageDialogPrefab, mainCanvas.transform, false);
        commDialog.GetComponent<UICommController>().SetCompanyController(this);
    }

    private void SetCommButtonText()
    {
        // Set text on comm button to send message or establish comm
        if (company.IsCommEstablished())
        {
            commButtonText.SetText("Send message");
        }
        else
        {
            commButtonText.SetText("Establish communication");
        }
    }


    public string GetCompanyName()
    {
        return company.companyName;
    }

    public List<string> GetMessageList()
    {
        return company.GetMessages();
    }

    public void EstablishCommunication()
    {
        // Set that communication with this company has been established and show chat
        company.SetCommEstablished();
        SetCommButtonText();
        ShowChatDialog();

        // TODO notify other company
    }

    public void SetCompanyModel(CompanyModel companyModel)
    {
        this.company = companyModel;
        SetCompanyName(company.companyName);
        SetCommButtonText();
    }

    public void HandleCommButtonClick()
    {
        if (! company.IsCommEstablished())
        {
            // Show establish dialog
            GameObject commDialog = Instantiate(establishCommDialogPrefab, mainCanvas.transform, false);
            commDialog.GetComponent<UICommController>().SetCompanyController(this);

            // TODO if request from another company, show answer dialog

        }
        else
        {
            ShowChatDialog();
        }
    }

    public void SendMessageToCompany(string newMessage)
    {
        int receiverNetId = (int)company.networkCompany.netId.Value;
        int senderNetId = (int)company.localCompany.netId.Value;
        company.AddMessage(newMessage, true);
        GameObject.Find("NetworkManager").GetComponent<NetworkManager>().SendInstantMessage(senderNetId, receiverNetId, newMessage, company.companyName);
        //company.localCompany.SendInstantMessage(id, newMessage, company.companyName);
    }

    public void ReceiveMessage(string message)
    {
        company.AddMessage(message);
        if (commDialog)
        {
            commDialog.GetComponent<UICommController>().AddMessageItem(message);
        }
    }

}
