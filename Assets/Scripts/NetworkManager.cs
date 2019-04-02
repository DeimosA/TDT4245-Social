using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class NetworkManager : UnityEngine.Networking.NetworkManager
{
	public event Action<bool, MatchInfo> matchCreated;

	public event Action<bool, MatchInfo> matchJoined;

	private Action<bool, MatchInfo> NextMatchCreatedCallback;

	List<NetworkPlayer> players;

	public static NetworkManager Instance;

	private bool started = false;

	List<string> playerNames;

	List<List<int>> playerStats;

	int iActivePlayer = 0;
	public int ActivePlayer
	{
		get
		{
			return iActivePlayer;
		}
	}

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
	}
	// Use this for initialization
	void Start()
	{
		//Instance = this;
		players = new List<NetworkPlayer>();
		playerNames = new List<string>();

		playerStats = new List<List<int>>();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Update is called once per frame
	void Update()
	{
		
		//foreach (NetworkPlayer player in players){
		//	player.controller.test();
		//}
		if (players.Count > 0)
		{
			CheckPlayersReady ();
		}
	}

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler(SendMessageType.Score, ServerInstantMessageHandler);
        NetworkServer.RegisterHandler(ConnectMessageType.Score, ServerConnectMessageHandler);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        client.RegisterHandler(SendMessageType.Score, ReceiveInstantMessage);
    }

    private Dictionary<int, int> connIdLookup = new Dictionary<int, int>();

    public void ServerInstantMessageHandler(NetworkMessage networkMessage)
    {
        int cId = networkMessage.conn.connectionId;
        SendMessage message = networkMessage.ReadMessage<SendMessage>();
        Debug.Log("ServerSide " + message.receiverNetId + " " + message.messageContent);
        NetworkServer.SendToClient(connIdLookup[message.receiverNetId], SendMessageType.Score, message);
    }

    public void ServerConnectMessageHandler(NetworkMessage networkMessage)
    {
        int connectionId = networkMessage.conn.connectionId;
        SendMessage message = networkMessage.ReadMessage<SendMessage>();
        connIdLookup.Add(message.senderNetId, connectionId);

        Debug.Log("Client connected: " + message.senderNetId + " " + message.companyName);
        //NetworkServer.SendToClient(message.receiverNetId - 1, SendMessageType.Score, message);
    }

    public void SendConnectMessage(int senderNetId, string companyName)
    {
        // send msg to server
        SendMessage message = new SendMessage();
        message.senderNetId = senderNetId;
        message.receiverNetId = -1;
        message.messageContent = "";
        message.companyName = companyName;
        client.Send(ConnectMessageType.Score, message);
    }

    public void SendInstantMessage(int senderNetId, int receiverNetId, string msg, string companyName)
    {
        // send msg to Receiver
        SendMessage message = new SendMessage();
        message.senderNetId = senderNetId;
        message.receiverNetId = receiverNetId;
        message.messageContent = msg;
        message.companyName = companyName;
        Debug.Log("send: " + receiverNetId + " " + msg);
        //NetworkServer.SendToAll(SendMessageType.Score, message);
        client.Send(SendMessageType.Score, message);
    }

    void ReceiveInstantMessage(NetworkMessage networkMessage)
    {
        SendMessage message = networkMessage.ReadMessage<SendMessage>();
        Debug.Log("receive " + message.senderNetId + " " + message.messageContent);
        //Debug.Log(message.messageContent);
        foreach (NetworkPlayer player in players)
        {
            if (player.netId.Value == message.senderNetId)
            {
                //Debug.Log("value match");
                player.uiCompanyController.ReceiveMessage(message.messageContent);
            }
        }
    }

    bool CheckPlayersReady()
	{
		bool playersReady = true;
		foreach (var player in players)
		{
			playersReady &= player.ready;
			// add a bool "ready" which is set after a player is done setting up their business
		}

		if (playersReady && !started)
		{
			//players[iActivePlayer].StartGame();			rewrite, this is when game starts
			foreach (NetworkPlayer player in players){
				player.StartGame();
				started = true;
				playerStats.Add(new List<int>());
				//player.SetupNames();
				//player.controller.test();
			}
			SceneManager.LoadScene("CharacterCreationScene", LoadSceneMode.Single);
		}

		return playersReady;
	}

	public void ReTurn()		// terrible naming
	{
		Debug.Log ("turn::"+iActivePlayer);		//
		foreach(NetworkPlayer player in players){
			player.TurnStart();

		}
		//players[iActivePlayer].TurnStart();
	}
	/*
	public void AlterTurns()
	{
		Debug.Log ("turn::"+iActivePlayer);

		players[iActivePlayer].TurnEnd();
		iActivePlayer = (iActivePlayer + 1) % players.Count;
		players[iActivePlayer].TurnStart();
	}
	*/
	// Doesn't work as intended for our project, goes on a rotating basis and has to be rewritten (but how)
	public void UpdateScore(float score)
	{

		players [ActivePlayer].UpdateScore (score);
		//endres til update statistics
	}

	public void UpdateStatistics(int[] newData){
		players[ActivePlayer].UpdateStatistics(newData);
	}

	public void RegisterNetworkPlayer(NetworkPlayer player)
	{
		if (players.Count <= 4)
		{
			players.Add(player);
			player.numberInList = players.IndexOf(player);
		}
	}

	public void DeregisterNetworkPlayer(NetworkPlayer player)
	{
		players.Remove(player);
	}

	public void CreateOrJoin(string gameName, Action<bool, MatchInfo> onCreate)
	{
		StartMatchMaker();
		NextMatchCreatedCallback = onCreate;
		matchMaker.ListMatches(0, 10, "turnbasedgame", true, 0, 0, OnMatchList);
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if(scene.name == "MainGameUIScene")		// rename
		{
			NetworkServer.SpawnObjects();
		}
	}

	public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		Debug.Log("Matches:" + matches.Count);
		if (success && matches.Count > 0)
		{
			matchMaker.JoinMatch(matches[0].networkId, string.Empty, string.Empty, string.Empty, 0, 0, OnMatchJoined);
		}
		else
		{
			CreateMatch("turnbasedgame");
		}
	}

	public void CreateMatch(string matchName)
	{
		matchMaker.CreateMatch(matchName, 2, true, string.Empty, string.Empty, string.Empty, 0, 0, OnMatchCreate);
	}

	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchCreate(success, extendedInfo, matchInfo);
		Debug.Log("OnMatchCreate"+matchInfo.networkId);

		// Fire callback
		if (NextMatchCreatedCallback != null)
		{
			NextMatchCreatedCallback(success, matchInfo);
			NextMatchCreatedCallback = null;
		}

		// Fire event
		if (matchCreated != null)
		{
			matchCreated(success, matchInfo);
		}
	}

	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchJoined(success, extendedInfo, matchInfo);
		Debug.Log("OnMatchJoined"+matchInfo.networkId);

		// Fire callback
		if (NextMatchCreatedCallback != null)
		{
			NextMatchCreatedCallback(success, matchInfo);
			NextMatchCreatedCallback = null;
		}

		// Fire event
		if (matchJoined != null)
		{
			matchJoined(success, matchInfo);
		}
	}

    public List<NetworkPlayer> getPlayerList()
    {
        return this.players;
    }

	public void AddName(string name){
		if(!playerNames.Contains(name) && name != null){
			playerNames.Add(name);
			foreach(var player in players){
				player.playerList.Add(name);
			}
		}		
	}

	public void UpdateValues(List<int> newValues){
		foreach(var player in players){
			//if(playerStats[player.NumberInList] == null){
			//	playerStats.Insert(player.NumberInList, newValues);
			//}else{
				playerStats[player.numberInList] = newValues;
			//}
		}
	}

}

// possible a lot of these methods shouldn't be touched at all.