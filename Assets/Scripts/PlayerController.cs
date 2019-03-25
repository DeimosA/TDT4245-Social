using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
	//SHOOT,
	//JUMP
}




public class PlayerController : MonoBehaviour
{
	public int playerID;
	// same as SyncVar declared in NetworkPlayer, gotta be synced

	public int[] turnData;

	public delegate void PlayerInputCallback(PlayerAction action, float deg);
	public event PlayerInputCallback OnPlayerInput;
	bool isLocalPlayer = false;

	// Use this for initialization
	void Start()
	{
		// assign unique player ID?
	}

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		//Shoot ();
	}

	public void SetupLocalPlayer()
	{
		//add color to your player
		isLocalPlayer = true;
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

	// methods needed to change turndata array, these happen based on what cards the player plays during their turn. For example playing a card which gives them x more money will modify the corresponding
	// money index in the array by x.

	// these are probably being written by someone else right now and will be merged later.

	/*
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
	*/
}