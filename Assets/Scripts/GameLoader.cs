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


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerData = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>();
            if (playerData.GetCompanyName() == null)
            {
                this.RestartGame();
                return;
            }
            GameObject.Find("CompanyNameText").GetComponent<TextMeshProUGUI>().SetText(playerData.GetCompanyName());

        }
        catch (NullReferenceException)
        {
            // If player data is missing we are probably loading from the wrong place
            this.RestartGame();
            return;
        }

        int companyCount = 5;
        GameObject companyContainer = GameObject.Find("OtherCompaniesPanel");
        for (int i = 0; i < companyCount; i++)
        {
            GameObject newInstance = Instantiate(companyPrefab, companyContainer.transform, false);
            newInstance.transform.SetParent(companyContainer.transform, false);
            newInstance.GetComponent<OtherCompanyController>().SetCompanyName("Kompani " + i);
        }

    }

    private void RestartGame()
    {
        SceneManager.LoadScene("CharacterCreationScene", LoadSceneMode.Single);
    }

}
