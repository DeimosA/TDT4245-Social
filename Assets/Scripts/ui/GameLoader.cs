using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLoader : MonoBehaviour
{

    public GameObject companyPrefab;

    private PersistentPlayerData playerData;
    private UICompanyController companyStatus;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // Load player data from company creation
            playerData = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>();
            if (playerData.GetCompanyName() == null)
            {
                // If company creation scene is loaded when running main scene, restart
                // TODO set a test company instead for quicker loading
                this.RestartGame();
                return;
            }
        }
        catch (NullReferenceException)
        {
            // If player data is missing we are probably loading from the wrong place
            this.RestartGame();
            return;
        }
        //GameObject.Find("CompanyNameText").GetComponent<TextMeshProUGUI>().SetText(playerData.GetCompanyName());
        companyStatus = GameObject.Find("CompanyStatusPanel").GetComponent<UICompanyController>();
        companyStatus.SetCompanyModel(new CompanyModel(playerData.GetCompanyName()));


        /* Load game logic scenes */
        //SceneManager.LoadScene("GameLogicScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("CardsLogicScene", LoadSceneMode.Additive);


        /* Instantiate other company info cards */
        int companyCount = 20;
        GameObject companyContainer = GameObject.Find("OtherCompaniesPanel");
        for (int i = 0; i < companyCount; i++)
        {
            CompanyModel company = new CompanyModel("Kompani " + i);
            GameObject newInstance = Instantiate(companyPrefab, companyContainer.transform, false);
            newInstance.transform.SetParent(companyContainer.transform, false);
            newInstance.GetComponent<UICompanyController>().SetCompanyModel(company);
        }

    }

    private void RestartGame()
    {
        SceneManager.LoadScene("CharacterCreationScene", LoadSceneMode.Single);
    }

}
