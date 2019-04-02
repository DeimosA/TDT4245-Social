using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using TMPro;

public class GameLoader : MonoBehaviour
{

    public GameObject companyPrefab;

    private PersistentPlayerData playerData;
    private UICompanyController companyStatus;
    private NetworkManager networkManager;
    private List<NetworkPlayer> networkPlayers;


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
                this.RestartGame();
                return;
            }

            // Find network stuff
            networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkPlayers = networkManager.getPlayerList();
            Debug.Log("Player count: " + networkPlayers.Count);
            //foreach (NetworkPlayer player in networkPlayers) {
            //    Debug.Log(player +
            //        " isClient: " + player.gameObject.GetComponent<NetworkIdentity>().isClient +
            //        " " + player.gameObject.GetComponent<NetworkIdentity>().netId.ToString()
            //    );
            //}
            foreach (NetworkPlayer player in networkPlayers)
            {
                Debug.Log(player +
                    " isClient: " + player.isClient +
                    " isServer: " + player.isServer +
                    " isLocalPlayer: " + player.isLocalPlayer +
                    " " + player.gameObject.GetComponent<NetworkIdentity>().netId.ToString() +
                    " " + player.netId.ToString()
                );
            }

        }
        catch (NullReferenceException)
        {
            // If player data is missing we are probably loading from the wrong place
            this.RestartGame();
            return;
        }

        /* Load game logic scenes */
        //SceneManager.LoadScene("GameLogicScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("CardsLogicScene", LoadSceneMode.Additive);


        /* Instantiate other company info cards */
        GameObject companyContainer = GameObject.Find("OtherCompaniesPanel");
        NetworkPlayer ownPlayer = null;

        for (int i = 0; i < networkPlayers.Count; i++)
        {
            if (networkPlayers[i].isLocalPlayer)
            {
                ownPlayer = networkPlayers[i];
            }
        }

        for (int i = 0; i < networkPlayers.Count; i++)
        {
            if (! networkPlayers[i].isLocalPlayer)
            {
                networkPlayers[i].companyName = "Kompani " + i;
                CompanyModel company = new CompanyModel(networkPlayers[i]);
                company.localCompany = ownPlayer;
                GameObject newInstance = Instantiate(companyPrefab, companyContainer.transform, false);
                newInstance.transform.SetParent(companyContainer.transform, false);
                UICompanyController cc = newInstance.GetComponent<UICompanyController>();
                networkPlayers[i].uiCompanyController = cc;
                cc.SetCompanyModel(company);
            }
        }

        /* Put info in own company info panel */
        companyStatus = GameObject.Find("CompanyStatusPanel").GetComponent<UICompanyController>();
        ownPlayer.companyName = playerData.GetCompanyName();
        ownPlayer.uiCompanyController = companyStatus;
        CompanyModel localCompany = new CompanyModel(ownPlayer);
        localCompany.localCompany = ownPlayer;
        companyStatus.SetCompanyModel(localCompany);


        //// TEST DATA ////////
        //TestData();
        ///////////////////////

    }

    private void TestData()
    {
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

    /**
     * For reloading game in editor
     */
    private void RestartGame()
    {
        // Destroy everything
        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);
        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);

        // Load network manager
        SceneManager.LoadScene("GameLogicScene", LoadSceneMode.Single);
    }

}
