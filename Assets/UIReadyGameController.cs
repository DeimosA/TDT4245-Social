using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReadyGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadyButtonCLickHandler()
    {
        List<NetworkPlayer> players = NetworkManager.Instance.getPlayerList();
        foreach (NetworkPlayer player in players)
        {
            if (player.isLocalPlayer)
            {
                float power = 50f;
                player.OnPlayerInput(PlayerAction.SHOOT, power);
            }
        }
    }

}
