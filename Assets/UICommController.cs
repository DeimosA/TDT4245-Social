using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICommController : MonoBehaviour
{

    public GameObject messageItemPrefab;
    public TextMeshProUGUI companyNameText;
    public Transform messageContainer;
    public TMP_InputField messageInput;
    public ScrollRect messageScrollRect;

    private UICompanyController company;
    private string message = "";

    // Start is called before the first frame update
    void Start()
    {
        if (messageInput != null) messageInput.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SendMessageClickHandler();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CancelCommDialog();
        }
    }

    private void FillMessageContainer()
    {
        foreach (string message in company.GetMessageList())
        {
            if (message.StartsWith("You: ")) AddMessagePrefab(message, true);
            else AddMessageItem(message);
        }
    }

    private void AddMessagePrefab(string message, bool ownMessage = false)
    {
        GameObject messageItem = Instantiate(messageItemPrefab, messageContainer, false);
        messageItem.transform.SetParent(messageContainer, false);
        TextMeshProUGUI text = messageItem.GetComponent<TextMeshProUGUI>();

        // Set a different color for our own messages
        if (ownMessage) text.color = new Color(0.1f, 0.1f, 0.5f);
        text.SetText(message);
        messageScrollRect.velocity = new Vector2(0, 1000f);
    }

    public void AddMessageItem(string message, bool ownMessage = false)
    {
        if (ownMessage) AddMessagePrefab("You: " + message, true);
        else AddMessagePrefab(message);
    }


    public void SetCompanyController(UICompanyController cc)
    {
        company = cc;
        if (companyNameText != null)
        {
            companyNameText.SetText(company.GetCompanyName());
            FillMessageContainer();
        }
    }

    public void EstablishComms()
    {
        Destroy(gameObject);
        company.EstablishCommunication();
    }

    public void CancelCommDialog()
    {
        Destroy(gameObject);
        // TODO notify other company if request denied
    }

    public void SendMessageClickHandler()
    {
        message = message.Trim();
        if (message != "")
        {
            company.SendMessageToCompany(message);
            AddMessageItem(message, true);
            // Clear message input
            message = "";
            messageInput.text = "";
            messageInput.ActivateInputField();
        }
    }

    public void UpdateMessage(string message)
    {
        this.message = message;
    }
}
