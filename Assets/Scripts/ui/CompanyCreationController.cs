using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CompanyCreationController : MonoBehaviour
{

    public int maxNameLength;

    private PersistentPlayerData playerData;
    private TextMeshProUGUI nameFeedback;
    private bool startable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>();
        nameFeedback = GameObject.Find("CompanyNameFeedback").GetComponent<TextMeshProUGUI>();
        GameObject.Find("CompanyNameInput").GetComponent<TMP_InputField>().ActivateInputField();
    }

    void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            this.StartGame();
        }
    }

    public void SetCompanyName(string companyName)
    {
        companyName = companyName.Trim();

        if (companyName.Length <= 0)
        {
            nameFeedback.SetText("* Required");
            startable = false;
        }
        else if (companyName.Length > maxNameLength)
        {
            nameFeedback.SetText("* Must be less than " + maxNameLength + " characters");
            startable = false;
        }
        else
        {
            nameFeedback.SetText("* OK");
            playerData.SetCompanyName(companyName);
            startable = true;
        }

    }

    public void StartGame()
    {
        if (startable)
        {
            NetworkPlayer localPlayer = GameObject.Find("local").GetComponent<NetworkPlayer>();
            localPlayer.SetupNames = true;

            NetworkManager.Instance.SendConnectMessage((int)localPlayer.netId.Value, playerData.GetCompanyName());

            SceneManager.LoadScene("MainGameUIScene", LoadSceneMode.Single);
        }
    }

}
