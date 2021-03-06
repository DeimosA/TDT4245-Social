﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStat
{
    userbase,
    capital,
    reputation
}

public class PlayerStats : MonoBehaviour
{
    public PlayerStatIntDictionary stats = new PlayerStatIntDictionary()
    {
        {PlayerStat.capital, 0 },
        {PlayerStat.reputation, 0 },
        {PlayerStat.userbase, 0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCapital()
    {
        return stats[PlayerStat.capital];
    }

    public int GetReputation()
    {
        return stats[PlayerStat.reputation];
    }

    public int GetUserbase()
    {
        return stats[PlayerStat.userbase];
    }

    public void AddToStat(StatChange statChange)
    {
        stats[statChange.stat] += statChange.valueChange;
    }
}
