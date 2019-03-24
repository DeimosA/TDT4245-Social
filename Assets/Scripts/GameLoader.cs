using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLoader : MonoBehaviour
{

    private PersistentPlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerData = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>();
            if (playerData.GetCompanyName() == null) this.RestartGame();
            GameObject.Find("CompanyNameText").GetComponent<TextMeshProUGUI>().SetText(playerData.GetCompanyName());

        } catch (NullReferenceException)
        {
            // If player data is missing we are probably loading from the wrong place
            this.RestartGame();
            return;
        }

    }

    private void RestartGame()
    {
        SceneManager.LoadScene("CharacterCreationScene", LoadSceneMode.Single);
    }

}
