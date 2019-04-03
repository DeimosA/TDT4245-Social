using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerAction
{
	SHOOT,
	//JUMP
}




public class PlayerController : MonoBehaviour
{
	public int playerID;
	// same as SyncVar declared in NetworkPlayer, gotta be synced

	public int[] turnData;
	public int userbase;
	public int capital;
	public int publicOpinion;
	public string name = "testing";

	private GameObject sp;

	//public GameObject nwm;

	public delegate void PlayerInputCallback(PlayerAction action, float deg);
	public event PlayerInputCallback OnPlayerInput;

	bool isLocalPlayer = false;

    public PlayerStats playerStats;
    public PlayerFeatures playerFeatures;
    public CardIntDictionary choiceHistory;
    public int currentTurn;
    public PlayerHandController playerHandController;
    public CardDeckController deck;
    [Header("Newsfeed stuff")]
    public int newsCounter = 0;
    public PlayerNewsFeedController playerNewsFeedController;

    [Header("Prefabs")]
    public GameObject cardViewOverlayPrefab;

    private CardIntDictionary cardsPlayedLastTurn;
    private GameObject cardViewOverlay;
	//bool ready = false;

	public List<string> playerList;

    NetworkPlayer localNetworkPlayer;

	// Use this for initialization
	void Start()
	{
        // TODO figure out if local player
        List<NetworkPlayer> networkPlayers = NetworkManager.Instance.getPlayerList();
        foreach (NetworkPlayer player in networkPlayers)
        {
            if (player.isLocalPlayer)
            {
                player.controller = this;
                localNetworkPlayer = player;
            }
        }

		sp = GameObject.Find("Sphere");
		DontDestroyOnLoad(this);

		List<string> playerList = new List<string>();
		//GameObject.Find("Sphere").SetActive(false);

		
		//name = gameobject.PersistentPlayerData.getCompanyName();
        choiceHistory = new CardIntDictionary();
        cardsPlayedLastTurn = new CardIntDictionary();
		// assign unique player ID?
	}


    // Update is called once per frame
    void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		//if(Input.GetKeyDown(KeyCode.W)){
		//	sp.SetActive(true);
		//}

		// call whatever methods are needed for inputs
		//Shoot ();
	}

	public void SetupLocalPlayer()
	{
		//add color to your player
		//isLocalPlayer = true;
		//instansiate values for each player here as they join
			// think this might be buggy, having issues with isLocalPlayer for non-host.
	}

	public void TurnStart()
	{
		if (isLocalPlayer)
		{
			//receive other player's turn data from server and update values
			/*
			for (player in playerList){
				update their statistics with data retrieved from server
			}
			*/

			// update newsfeed
			// newsfeed probably a gameobject, should have methods to do this
			// newsfeed<String> += new messages or something
			// newsfeed.update(message) 

			//spawn or enable player
			turnData = new int[4];
			turnData[0] = this.playerID;
		}
	}

	public void TurnEnd()
	{
		if (isLocalPlayer)
		{
			// disable actions/player input

			// send turn data to server

		}
	}
	public void test(List<string> names){
		foreach (string name in names){
			Debug.Log(name);
		}

	/*
	public void test(){
		Debug.Log("???????");
	}
*/
	}
	public void testtwo(string name, bool a){
		Debug.Log(name);
		Debug.Log(a);
		//DebugConsole.Log(name);
	}

	// methods needed to change turndata array, these happen based on what cards the player plays during their turn. For example playing a card which gives them x more money will modify the corresponding
	// money index in the array by x.

	// these are probably being written by someone else right now and will be merged later.


    //
    public void OnTurnStart()
    {
        //Display new turn text
        StartCoroutine(DisplayNewTurnText());

        //commit cards played last turn
        if(cardsPlayedLastTurn.Count != 0)
        {
            foreach(ActivityCard card in cardsPlayedLastTurn.Keys)
            {
                //Apply stat changes
                ActivityChoice choice = card.GetChoice(cardsPlayedLastTurn[card]);
                for (int j = 0; j < choice.statChanges.Count; j++)
                {
                    playerStats.AddToStat(choice.statChanges[j]);
                }

                //Apply news counter changes
                newsCounter += choice.addToNewsCounter;

                //Add any potential priority cards to queue
                if (choice.HasPriorityCard())
                {
                    Debug.Log(deck.priorityCards);
                    deck.AddToQueue(choice.priorityCardToTrigger);
                }

                //commit to history
                try
                {
                    choiceHistory.Add(card, cardsPlayedLastTurn[card]);
                }
                catch (System.ArgumentException)
                {
                    Debug.Log("Uh oh card already in choiceHistory");
                }
            }

            //clear cards played last turn
            cardsPlayedLastTurn.Clear();
        }

        //get stat changes based on features
        List<StatChange> featureStatChanges = playerFeatures.GetStatChanges();
        for(int i = 0; i < featureStatChanges.Count; i++)
        {
            playerStats.AddToStat(featureStatChanges[i]);
        }

        //update newsfeed
        playerNewsFeedController.OnTurnStart();


        //fill hand
        FillHand();
    }

    //add all cards in playslots to cardsPlayedLastTurn
    public void EndTurn()
    {
        List<GameObject> cardsInPlaySlots = playerHandController.GetCardsInPlaySlots();
        for (int i = 0; i < cardsInPlaySlots.Count; i++)
        {
            CardController cardController = cardsInPlaySlots[i].GetComponent<CardController>();
            try
            {
                cardsPlayedLastTurn.Add(cardController.cardData, cardController.GetIndexOfHighlightedChoice());
            }
            catch (System.ArgumentException)
            {
                Debug.Log("Uh oh card already in cardsplayedlastturn");
            }

            ////apply stat changes
            //ActivityChoice choice = cardController.GetHighlightedChoice();
            //for (int j = 0; j < choice.statChanges.Count; j++)
            //{
            //    playerStats.AddToStat(choice.statChanges[j]);
            //}

            ////Add any potential priority cards to queue
            //if (choice.HasPriorityCard())
            //{
            //    deck.AddToQueue(choice.priorityCardToTrigger);
            //}

            Destroy(cardsInPlaySlots[i]);
        }

        //Display card view overlay
        DisplayCardViewOverlay("Waiting for other players");

        localNetworkPlayer.EndTurnEarly();
    }

    public void FillHand()
    {
        playerHandController.FillHand(currentTurn, choiceHistory, playerStats.stats, playerFeatures.GetPurchasedFeatureTitlesAsHashSet());
    }

    public bool ValidateChoice(ActivityChoice choice)
    {
        return choice.ValidateChoice(playerFeatures.GetPurchasedFeatureTitlesAsHashSet(), playerStats.stats);
    }


    private IEnumerator DisplayNewTurnText()
    {
        DisplayCardViewOverlay("New turn starting");
        yield return new WaitForSecondsRealtime(2f);
        Destroy(cardViewOverlay);
    }

    private void DisplayCardViewOverlay(string text)
    {
        if (cardViewOverlay != null)
        {
            Destroy(cardViewOverlay);
        }
        cardViewOverlay = Instantiate(cardViewOverlayPrefab, GameObject.Find("MainCanvas").transform, false);
        cardViewOverlay.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    // methods needed to change turndata array, these happen based on what cards the player plays during their turn. For example playing a card which gives them x more money will modify the corresponding
    // money index in the array by x.

    // these are probably being written by someone else right now and will be merged later.


	public void Shoot()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		float power = 50f;

			if (Input.GetKeyDown(KeyCode.S))
			{
				OnPlayerInput(PlayerAction.SHOOT, power);
		}
		if(Input.GetKeyDown(KeyCode.W)){
			Debug.Log("??");


		}

	}	

}