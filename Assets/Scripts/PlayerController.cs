using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public string name;

	private GameObject sp;

	//public GameObject nwm;

	public delegate void PlayerInputCallback(PlayerAction action, float deg);
	public event PlayerInputCallback OnPlayerInput;
	bool isLocalPlayer = false;

	//bool ready = false;

	// Use this for initialization
	void Start()
	{
		sp = GameObject.Find("Sphere");

			GameObject.Find("Sphere").SetActive(false);
		
		//name = gameobject.PersistentPlayerData.getCompanyName();
		// assign unique player ID?
	}

	// Update is called once per frame
	void Update()
	{

		//Debug.Log(score);
		//Debug.Log(nwm);
		//Debug.Log("c");
		if (!isLocalPlayer)
		{
			return;
		}
		if(Input.GetKeyDown(KeyCode.W)){
			sp.SetActive(true);
		}

		// call whatever methods are needed for inputs
		//Shoot ();
	}

	public void SetupLocalPlayer()
	{
		//add color to your player
		isLocalPlayer = true;
		Debug.Log("test");
		//instansiate values for each player here I thinK??
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

	public void test(){
		Debug.Log("test?");
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

		if (Application.platform == RuntimePlatform.Android)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				OnPlayerInput(PlayerAction.SHOOT, power);
			}
		}
		else
		{
			if (Input.GetMouseButtonDown (0))
			{
				OnPlayerInput(PlayerAction.SHOOT, power);
			}
		}
	}	

}