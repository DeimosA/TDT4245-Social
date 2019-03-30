using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CompanyCreationController : MonoBehaviour
{

    private PersistentPlayerData playerData;
    private TextMeshProUGUI testText;
    private bool startable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>();
        testText = GameObject.Find("CompanyNameTest").GetComponent<TextMeshProUGUI>();
        GameObject.Find("CompanyNameInput").GetComponent<TMP_InputField>().ActivateInputField();
    }

    public void SetCompanyName(string companyName)
    {
        playerData.SetCompanyName(companyName);
        testText.SetText(companyName);

        // Game is startable if company name is not empty
        startable = companyName != "" ? true : false;
    }

    public void StartGame()
    {
        if (startable)
        {
            //SceneManager.LoadScene("MainGameUIScene", LoadSceneMode.Single);
        }
    }

}
