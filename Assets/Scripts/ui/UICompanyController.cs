using System.Collections;
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
        GameObject commDialog = Instantiate(messageDialogPrefab, mainCanvas.transform, false);
        commDialog.GetComponent<UICommController>().SetCompanyController(this);
    }

    private void SetCommButtonText()
    {
        // TODO set text on comm button to send message or establish comm
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
        return company.messages;
    }

    public void EstablishCommunication()
    {
        // Set that communication with this company has been established and show chat
        company.SetCommEstablished();
        SetCommButtonText();
        ShowChatDialog();
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
        }
        else
        {
            ShowChatDialog();
        }
    }

    public void SendMessageToCompany(string newMessage)
    {
        company.messages.Add("You: " + newMessage);
        // TODO send message
    }

}
