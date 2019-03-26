using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStat
{
    USERBASE,
    CAPITAL,
    REPUTATION
}

public class PlayerStats : MonoBehaviour
{
    public PlayerStatIntDictionary stats = new PlayerStatIntDictionary()
    {
        {PlayerStat.CAPITAL, 0 },
        {PlayerStat.REPUTATION, 0 },
        {PlayerStat.USERBASE, 0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
