using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendMessage : MessageBase
{
	public string messageContent;
    public int senderNetId;
    public int receiverNetId;
    public string companyName;
}

public class SendMessageType
{
    public static short Score = MsgType.Highest + 1;
}


public class NetworkPlayer : NetworkBehaviour
{

    public List<NetworkPlayer> otherPlayers;

	public SendMessage m_Message;

	[SyncVar(hook = "OnTurnChange")]
	public bool isTurn = false;

	[SyncVar(hook = "UpdateTimeDisplay")]
	public float time = 90.0f;

	public PlayerController controller;

	//public NetworkManager nwm;

	private int[] turnData;

	[SyncVar]
	public bool ready = false;

	//[SyncVar]
	public uint playerID;

	[SyncVar]
	public int userbase = 0;

	[SyncVar]
	public int capital = 500;

	[SyncVar]
	public int publicOpinion = 1;

    
    public string companyName = "";

    public UICompanyController uiCompanyController;
    
	//[SyncList(hook = "AddPlayerToList")]
	public List<string> playerList = new List<string>();

	public bool SetupNames;

	bool done;

	//public int instanceID;


	// Use this for initialization
	void Start()		//may need a custom method that runs when game "starts"
	{
		//nwm = GameObject.Find("NetworkManager");
		controller.OnPlayerInput += OnPlayerInput; // what
		//playerID = gameObject.GetComponent<NetworkInstanceId>().Value;
		Time.timeScale = 1.0f;

        playerID = gameObject.GetComponent<NetworkIdentity>().netId.Value;
        
		bool done = false;
	}

	// Update is called once per frame
	[Server]
	void Update()
	{
		if (isTurn)
		{
			time -= Time.deltaTime;
			if (time <= 0)
			{
				TurnEnd();
				//NetworkManager.Instance.AlterTurns();
				// server starts handling new data
			}
		}
	}

	void LateUpdate(){
		if(!isLocalPlayer){
			return;
		}
		if(SetupNames && !done){
			controller.name = GameObject.Find("PlayerData").GetComponent<PersistentPlayerData>().GetCompanyName();
			OnPlayerAdd();
			done = true;
		}
			//CmdAddToPlayersList(controller.name);
		//if(isServer){
		//RpcSpawnPlayers(controller.name);
		//}
	}

	public override void OnStartClient() //this one should maybe be called when a player is ready setting up their business
	{
		DontDestroyOnLoad(this);
		//gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

		base.OnStartClient();
		Debug.Log("Client Network Player start");
		GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
		StartPlayer();
		NetworkManager.Instance.RegisterNetworkPlayer(this);
	}

	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();
		controller.SetupLocalPlayer();
		this.name = "local";
	}

	[Server]
	public void StartPlayer()
	{
		gameObject.GetComponent<Transform>().position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 5f), 0f);
		GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
	}

	public void StartGame()
	{
		TurnStart();
	}


	[Server]
	public void TurnStart()
	{
		isTurn = true;
		time = 90;
		RpcTurnStart();
	}

	[ClientRpc]
	void RpcTurnStart()
	{
		controller.TurnStart();
	}

	[Server]
	public void TurnEnd()
	{
		isTurn = false;
		RpcTurnEnd();
	}

	[ClientRpc]
	void RpcTurnEnd()
	{
		controller.TurnEnd();
	}

	[Command]
	public void CmdSendInstantMessage(int receiverID, string msg){
        // send msg to Receiver
	}

	[ClientRpc]
	void RpcReceiveInstantMessage(int senderID, string msg){
        Debug.Log("received a message from " + senderID + ": " + msg);
	}

	[Command]
	void CmdRequestCooperation(int receiverID, int cardID){
		// send request for cooperating on a certain card to a receiver
		m_Message = new SendMessage();
		m_Message.messageContent = cardID.ToString();
		//m_Message = cardID.ToString();
		NetworkServer.SendToClient(receiverID, 54, m_Message);
	}

	[ClientRpc]
	void RpcReceiveCooperationRequest(int senderID, int cardID){
		Debug.Log("received a request from " + senderID + " to work on card " + cardID);
	}

	[Command]
	void CmdSendTurnData(int[] turnData){
		//
	}

	[ClientRpc]
	void RpcReceiveTurnData(int[] turnData){
		
	}






	[Server]
	public void DistributeTurnChanges(){
		
		//RpcReceiveTurnData();		for every other player, subsequently update them
		// how are other players handled in your client?
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
		NetworkManager.Instance.DeregisterNetworkPlayer(this);
	}

	public void OnTurnChange(bool turn) //
	{
		if (isLocalPlayer)	// && turn, can be used to do different things on turn start/end
		{
			//play turn sound, display some "new turn" effect i guess
			//NetworkManager.Instance.UpdateStatistics(turnData);
		}
	}

	public void UpdateStatistics(int[] newData)
	{
		Debug.Log ("score: "+newData[1]);	//update statistics
		// get UI gameobject, shove new values in their fields
		userbase += newData[1];
		capital += newData[2];
		publicOpinion += newData[3];

		// temporary setup/sketch, newData[0] is some unique identifier
	}

	public void UpdateScore(float score){
		Debug.Log("score: " + score);
	}

	public void OnPlayerAdd(){
		CmdAddToPlayersList(controller.name);
	}


	[Command]
	void CmdAddToPlayersList(string name){
		RpcSpawnPlayers(name);
		//NetworkManager.Instance.AddName(controller.name);
	}


	[ClientRpc]
	void RpcSpawnPlayers(string playerName){		//List<string> ServerPlayers){
		//List<String> playerList = ServerPlayers;
		if(!playerList.Contains(playerName)){
			//playerList.Add(playerName);
			NetworkManager.Instance.AddName(playerName);
		}
	}

	void OnPlayerInput(PlayerAction action, float amount)
	{
		if (action == PlayerAction.SHOOT)
		{
			CmdOnPlayerInput(action, amount);
		}
	}
		
	
	[Command]
	void CmdOnPlayerInput(PlayerAction action, float amount)
	{
		//Shoot bullets

		//Update score
		ready = true;
		NetworkManager.Instance.UpdateScore(amount);

	}
		


	public void UpdateTimeDisplay(float curtime)
	{
		GameObject timerText = GameObject.Find("Timer");
		Text timer = timerText.GetComponent<Text> ();
		timer.text = Mathf.Round(curtime).ToString();
	}

    
}

